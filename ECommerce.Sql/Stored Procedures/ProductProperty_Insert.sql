CREATE PROCEDURE [dbo].[ProductProperty_Insert]
    @ProductId            INT,
    @ProductPropertyXML   XML   
AS
BEGIN
    DECLARE @ProductPropertyTable TABLE 
    (
        [Id]            INT,
        [PropertyId]    INT,
        [Value]         VARCHAR(500),
        [Unit]          VARCHAR(50),
        [IsInitial]     BIT
    )

    IF @ProductPropertyXML IS NOT NULL
    BEGIN
        DECLARE @DocHandle INT;
        EXEC sp_xml_preparedocument @DocHandle OUTPUT, @ProductPropertyXML;
        INSERT INTO @ProductPropertyTable([Id], [PropertyId], [Value], [Unit], [IsInitial])
        SELECT 
            PPXML.[Id],
            PPXML.[PropertyId],
            PPXML.[Value],
            PPXML.[Unit],
            1
        FROM OPENXML(@DocHandle, '/ArrayOfProductPropertyEntity/ProductPropertyEntity', 2) 
            WITH (
                [Id]            INT,
                [PropertyId]   INT,
                [Value]        VARCHAR(500),
                [Unit]         VARCHAR(50)
            ) PPXML
		LEFT JOIN	dbo.[ProductVariant] PV 
			ON		PPXML.[PropertyId] = PV.VariantPropertyId
			AND		PV.ProductId = @ProductId
		WHERE		PV.VariantPropertyId IS NULL
			
        -- Clean up XML handle
        EXEC sp_xml_removedocument @DocHandle;
    END

    DECLARE @Products TABLE 
    (
        ProductId           INT, 
        RowIndex            INT,
        ProductVariantIds   VARCHAR(300)
    )

    INSERT INTO @Products (ProductId, RowIndex, ProductVariantIds)
    SELECT      @ProductId, 0, NULL
    UNION ALL 
    SELECT      P.Id, 
                ROW_NUMBER() OVER(ORDER BY P.Id ASC) AS RowIndex, 
                P.ProductVariantIds
    FROM        dbo.[Product] P
    WHERE       P.ParentProductId = @ProductId

    DECLARE     @RowIndex           INT = 0
    DECLARE     @TotalRow           INT = 0
    DECLARE     @LoopProjectId      INT
    DECLARE     @ProductVariantIds  VARCHAR(300)

    SELECT      @TotalRow = MAX(P.RowIndex)
    FROM        @Products P

    BEGIN TRANSACTION ProductPropertyInsert
    BEGIN TRY
        WHILE (@RowIndex <= @TotalRow)
        BEGIN
            SELECT  @LoopProjectId = P.ProductId, @ProductVariantIds = P.ProductVariantIds
            FROM    @Products P
            WHERE   P.RowIndex = @RowIndex

        IF @RowIndex > 0
            BEGIN
                -- Clear child property (not parent)
                DELETE 
                FROM        @ProductPropertyTable
                WHERE       [IsInitial] = 0;

                -- Build from ProductVariant
                INSERT INTO @ProductPropertyTable([Id], [PropertyId], [Value], [Unit], [IsInitial])
                SELECT      ISNULL(PP.Id, 0) AS Id,
                            PV.VariantPropertyId, 
                            (SELECT VPV.[value] FROM dbo.fnSplit(PV.VariantPropertyValue, ' ') VPV WHERE VPV.idx = 1),
                            ISNULL((SELECT VPV.[value] FROM dbo.fnSplit(PV.VariantPropertyValue, ' ') VPV WHERE VPV.idx = 2), ''),
                            0
                FROM        dbo.fnSplit(@ProductVariantIds, ',') AS PVS
                INNER JOIN  dbo.ProductVariant AS PV 
                      ON    PV.Id = PVS.[value]
                      AND   PV.ProductId = @ProductId
                LEFT JOIN   dbo.ProductProperty PP
                      ON    PP.PropertyId = PV.VariantPropertyId
                      AND   PP.ProductId = @LoopProjectId -- only child
                WHERE       PP.Id IS NULL -- only insert if property doesn't exist

            END

            -- Delete properties not in the incoming XML (and have no value)
            DELETE FROM [dbo].[ProductProperty]
            WHERE [ProductId] = @LoopProjectId
                AND [PropertyId] NOT IN (
                    SELECT  [PropertyId] 
                    FROM    @ProductPropertyTable
                    WHERE   [Value] != ''
                );

            -- Update existing ProductProperty entries for parent product
            UPDATE PP
            SET 
                PP.[PropertyId] = PPXML.[PropertyId],
                PP.[Value] = PPXML.[Value],
                PP.[Unit] = PPXML.[Unit]
            FROM [dbo].[ProductProperty] PP
            INNER JOIN @ProductPropertyTable PPXML
                ON PP.[PropertyId] = PPXML.[PropertyId] AND PP.[ProductId] = @LoopProjectId;

            -- Insert new properties for parent product
            INSERT INTO [dbo].[ProductProperty] ([ProductId], [PropertyId], [Value], [Unit])
            SELECT 
						@LoopProjectId,
						PPXML.[PropertyId],
						PPXML.[Value],
						PPXML.[Unit]
            FROM		@ProductPropertyTable PPXML
            WHERE		[Value] != ''
				AND		PPXML.[PropertyId] NOT IN (
					SELECT  [PropertyId] 
                    FROM    [dbo].[ProductProperty] PP
					WHERE	PP.[ProductId] = @LoopProjectId
				)
			ORDER BY	PPXML.IsInitial 

            SET @RowIndex = @RowIndex + 1
        END

        IF @@TRANCOUNT > 0
            COMMIT TRANSACTION ProductPropertyInsert;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION ProductPropertyInsert;

        IF @DocHandle IS NOT NULL
            EXEC sp_xml_removedocument @DocHandle;

        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT, @ErrorNumber INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), 
               @ErrorState = ERROR_STATE(), @ErrorNumber = ERROR_NUMBER();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState, @ErrorNumber);
    END CATCH
END;
