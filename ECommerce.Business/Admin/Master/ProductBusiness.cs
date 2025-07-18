using AdvancedADO;
using CommonLibrary;
using DocumentDBClient;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2019.Word.Cid;
using ECommerce.Entity.Account;
using ECommerce.Entity.Admin.Master;
using ECommerce.Repository.Admin.Master;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Data;

namespace ECommerce.Business.Admin.Master
{
    //public class ProductBusiness : CommonBusiness, IProductRepositoroy
    public class ProductBusiness : CommonBusiness, IProductRepositoroy

    {
        ISql sql;
        IDocument<ProductMongoEntity> document;

        public ProductBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
            document = CreateDocumentInstance<ProductMongoEntity>("product");
        }

        public async Task<int> Insert(ProductEntity productEntity)
        {
            sql.AddParameter("Name", productEntity.Name);

            if (productEntity.CategoryId != 0)
                sql.AddParameter("CategoryId", productEntity.CategoryId);

            if (productEntity.Description != string.Empty)
                sql.AddParameter("Description", productEntity.Description);

            if (productEntity.LongDescription != string.Empty)
                sql.AddParameter("LongDescription", productEntity.LongDescription);

            sql.AddParameter("AllowReturn", productEntity.AllowReturn );

            if (productEntity.ReturnPolicy != string.Empty)
                sql.AddParameter("ReturnPolicy", productEntity.ReturnPolicy);

            if (productEntity.ProductVariantIds != string.Empty)
                sql.AddParameter("ProductVariantIds", productEntity.ProductVariantIds);

            sql.AddParameter("IsExpiry", productEntity.IsExpiry);
            sql.AddParameter("CreatedBy", productEntity.CreatedBy);
            sql.AddParameter("CreatedOn", DbType.DateTime, ParameterDirection.Input, productEntity.CreatedOn);
            sql.AddParameter("OpeningQty", productEntity.OpeningQty);
            sql.AddParameter("BuyQty", productEntity.BuyQty);
            sql.AddParameter("LockQty", productEntity.LockQty);
            sql.AddParameter("OrderQty", productEntity.OrderQty);
            sql.AddParameter("SellQty", productEntity.SellQty);
            sql.AddParameter("ClosingQty", productEntity.ClosingQty);
            sql.AddParameter("CostPrice", productEntity.CostPrice);
            sql.AddParameter("SellPrice", productEntity.SellPrice);
            sql.AddParameter("DiscountPercentage", productEntity.DiscountPercentage);
            sql.AddParameter("DiscountAmount", productEntity.DiscountAmount);
            sql.AddParameter("FinalSellPrice", productEntity.FinalSellPrice);
            sql.AddParameter("SKU", productEntity.SKU);
            sql.AddParameter("UPC", productEntity.UPC);
            sql.AddParameter("ParentProductId", productEntity.ParentProductId);
            sql.AddParameter("LastUpdatedBy", productEntity.LastUpdatedBy);
            sql.AddParameter("LastUpdatedOn", DbType.DateTime, ParameterDirection.Input, productEntity.LastUpdatedOn);

            if (productEntity.ProductMedias != null && productEntity.ProductMedias.Count > 0)
                sql.AddParameter("ProductMediaXML", productEntity.ProductMedias.ToXML());

            if (productEntity.VariantCombinations != null && productEntity.VariantCombinations.Count > 0)
                sql.AddParameter("VariantCombinationXML", productEntity.VariantCombinations.ToXML());

            productEntity.Properties = new List<dynamic>();
            productEntity.Id = MyConvert.ToInt(await sql.ExecuteScalarAsync("Product_Insert", CommandType.StoredProcedure));

            var variantCombinations = new List<ProductVariantEntity>();
            if (productEntity.ParentProductId == 0)
            {
                List<ProductVariantEntity> variantCombinationsDatabase = await SelectVariantCombinations(productEntity.Id);
                variantCombinations = (from db in variantCombinationsDatabase
                                       join api in productEntity.VariantCombinations
                                       on new { db.VariantPropertyId, db.VariantPropertyValue } equals new { api.VariantPropertyId, api.VariantPropertyValue }
                                       select new ProductVariantEntity { Index = api.Index, Id = db.Id }).ToList();
            }

            if (productEntity.Id > 0)
            {
                await document.InsertAsync(productEntity.ToMongoEntity());
            }

            //foreach (var variant in productEntity.Variants)
            //{
            //    variant.ParentProductId = productEntity.Id;
            //    variant.ProductVariantIds = string.Join(',', variantCombinations.Where(it => variant.ProductVariantIndexs.Contains(it.Index)).Select(it => it.Id).ToList());
            //    Console.WriteLine($"Saving Variant ID: {variant.Id}, ProductVariantIds: {variant.ProductVariantIds}");

            //    await Insert(variant);
            //}
            foreach (var variant in productEntity.Variants)
            {
                variant.ParentProductId = productEntity.Id;

                // Only assign ProductVariantIds if there are index matches
                if (variant.ProductVariantIndexs != null && variant.ProductVariantIndexs.Count > 0)
                {
                    var matchedVariantIds = variantCombinations
                        .Where(it => variant.ProductVariantIndexs.Contains(it.Index))
                        .Select(it => it.Id)
                        .ToList();

                    if (matchedVariantIds.Count > 0)
                    {
                        variant.ProductVariantIds = string.Join(',', matchedVariantIds);
                    }
                }

                Console.WriteLine($"Saving Variant ID: {variant.Id}, ProductVariantIds: {variant.ProductVariantIds}");

                await Insert(variant);
            }

            return productEntity.Id;
        }

        private async Task<List<ProductVariantEntity>> SelectVariantCombinations(int productId)
        {
            sql.AddParameter("ProductId", productId);
            return await sql.ExecuteListAsync<ProductVariantEntity>("ProductVariant_SelectForGrid", CommandType.StoredProcedure);
        }

        public async Task<int> Update(ProductEntity productEntity)
        {
            sql.AddParameter("Id", productEntity.Id);
            sql.AddParameter("Name", productEntity.Name);

            if (productEntity.CategoryId != 0)
                sql.AddParameter("CategoryId", productEntity.CategoryId);

            if (productEntity.Description != string.Empty)
                sql.AddParameter("Description", productEntity.Description);

            if (productEntity.LongDescription != string.Empty)
                sql.AddParameter("LongDescription", productEntity.LongDescription);

            if (productEntity.ProductVariantIds != string.Empty)
                sql.AddParameter("ProductVariantIds", productEntity.ProductVariantIds);

            if (productEntity.ReturnPolicy != string.Empty)
                sql.AddParameter("ReturnPolicy", productEntity.ReturnPolicy);

            sql.AddParameter("AllowReturn", productEntity.AllowReturn );

            sql.AddParameter("IsExpiry", productEntity.IsExpiry);

            sql.AddParameter("LastUpdatedOn", DbType.DateTime, ParameterDirection.Input, productEntity.LastUpdatedOn);

            sql.AddParameter("OpeningQty", productEntity.OpeningQty);

            sql.AddParameter("BuyQty", productEntity.BuyQty);

            sql.AddParameter("LockQty", productEntity.LockQty);

            sql.AddParameter("OrderQty", productEntity.OrderQty);

            sql.AddParameter("SellQty", productEntity.SellQty);

            sql.AddParameter("ClosingQty", productEntity.ClosingQty);

            sql.AddParameter("CostPrice", productEntity.CostPrice);

            sql.AddParameter("SellPrice", productEntity.SellPrice);

            sql.AddParameter("DiscountPercentage", productEntity.DiscountPercentage);

            sql.AddParameter("DiscountAmount", productEntity.DiscountAmount);

            sql.AddParameter("FinalSellPrice", productEntity.FinalSellPrice);
            sql.AddParameter("SKU", productEntity.SKU);
            sql.AddParameter("UPC", productEntity.UPC);
            sql.AddParameter("CreatedBy", productEntity.CreatedBy);
            sql.AddParameter("LastUpdatedBy", productEntity.LastUpdatedBy);

            if (productEntity.ProductMedias != null && productEntity.ProductMedias.Count > 0)
                sql.AddParameter("ProductMediaXML", productEntity.ProductMedias.ToXML());

            if (productEntity.VariantCombinations != null && productEntity.VariantCombinations.Count > 0)
                sql.AddParameter("VariantCombinationXML", productEntity.VariantCombinations.ToXML());

            var result = await sql.ExecuteScalarAsync("Product_Update", CommandType.StoredProcedure);

            await document.UpdateAsync(productEntity.ToMongoEntity());

            var variantCombinations = new List<ProductVariantEntity>();
            if (productEntity.ParentProductId == 0)
            {
                List<ProductVariantEntity> variantCombinationsDatabase = await SelectVariantCombinations(productEntity.Id);
                variantCombinations = (from db in variantCombinationsDatabase
                                          join api in productEntity.VariantCombinations
                                          on new { db.VariantPropertyId, db.VariantPropertyValue } equals new { api.VariantPropertyId, api.VariantPropertyValue }
                                          select new ProductVariantEntity { Index= api.Index, Id = db.Id }).ToList();
            }

            //foreach (var variant in productEntity.Variants)
            //{
            //    variant.ParentProductId = productEntity.Id;
            //    variant.ProductVariantIds = string.Join(',', variantCombinations.Where(it => variant.ProductVariantIndexs.Contains(it.Index)).Select(it => it.Id).ToList());

            //    Console.WriteLine($"Saving Variant ID: {variant.Id}, ProductVariantIds: {variant.ProductVariantIds}");

            //    if (variant.Id == 0)
            //        await Insert(variant);
            //    else
            //        await Update(variant);
            //}
            foreach (var variant in productEntity.Variants)
            {
                variant.ParentProductId = productEntity.Id;

                if (variant.ProductVariantIndexs != null && variant.ProductVariantIndexs.Count > 0)
                {
                    var matchedVariantIds = variantCombinations
                        .Where(it => variant.ProductVariantIndexs.Contains(it.Index))
                        .Select(it => it.Id)
                        .ToList();

                    if (matchedVariantIds.Count > 0)
                    {
                        variant.ProductVariantIds = string.Join(',', matchedVariantIds);
                    }
                }

                Console.WriteLine($"Saving Variant ID: {variant.Id}, ProductVariantIds: {variant.ProductVariantIds}");

                if (variant.Id == 0)
                    await Insert(variant);
                else
                    await Update(variant);
            }



            ProductParameterEntity productParameterEntity = new ProductParameterEntity();
            productParameterEntity.Id = productEntity.Id;
            await Sync(productParameterEntity);

            return MyConvert.ToInt(result);
        }

        public async Task Delete(int id)
        {
            sql.AddParameter("Id", id);
            await sql.ExecuteNonQueryAsync("Product_Delete", CommandType.StoredProcedure);
            await document.DeleteAsync(id.ToString());
        }

        public async Task<ProductGridEntity> SelectForGrid(ProductParameterEntity productParameterEntity)
        {
            if (productParameterEntity.Name != string.Empty)
                sql.AddParameter("Name", productParameterEntity.Name);

            if (productParameterEntity.CategoryId != 0)
                sql.AddParameter("CategoryId", productParameterEntity.CategoryId);

            if (productParameterEntity.Description != string.Empty)
                sql.AddParameter("Description", productParameterEntity.Description);

            sql.AddParameter("SortExpression", productParameterEntity.SortExpression);
            sql.AddParameter("SortDirection", productParameterEntity.SortDirection);
            sql.AddParameter("PageIndex", productParameterEntity.PageIndex);
            sql.AddParameter("PageSize", productParameterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<ProductGridEntity>("Product_SelectForGrid", CommandType.StoredProcedure, 2, MapGridEntity);
        }

        public async Task MapGridEntity(int resultSet, ProductGridEntity productGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    productGridEntity.Products.Add(await sql.MapDataAsync<ProductEntity>(reader));
                    break;
                case 1:
                    productGridEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }
       
        public async Task<ProductEntity> SelectForRecord(int Id)
        {
            sql.AddParameter("Id", Id);
            return await sql.ExecuteRecordAsync<ProductEntity>("Product_SelectForRecord", CommandType.StoredProcedure);
        }
        public async Task<List<ProductMainEntity>> SelectForLOV(ProductParameterEntity productParameterEntity)
        {
            return await sql.ExecuteListAsync<ProductMainEntity>("Product_SelectForLOV", CommandType.StoredProcedure);
        }

        public async Task<ProductAddEntity> SelectForAdd(ProductParameterEntity productParameterEntity)
        {
            return await sql.ExecuteResultSetAsync<ProductAddEntity>("Product_SelectForAdd", CommandType.StoredProcedure, 1, MapAddEntity);
        }

        public async Task MapAddEntity(int resultSet, ProductAddEntity productAddEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    productAddEntity.Categories.Add(await sql.MapDataAsync<CategoryMainEntity>(reader));
                    break;

            }
        }

        public async Task<ProductEditEntity> SelectForEdit(ProductParameterEntity productParameterEntity)
        {
            if (productParameterEntity.Id != 0)
                sql.AddParameter("Id", productParameterEntity.Id);

            return await sql.ExecuteResultSetAsync<ProductEditEntity>("Product_SelectForEdit", CommandType.StoredProcedure, 5, MapEditEntity);
        }

        public async Task MapEditEntity(int resultSet, ProductEditEntity productEditEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    productEditEntity.Product = await sql.MapDataAsync<ProductEntity>(reader);
                    break;
                case 1:
                    productEditEntity.Product.ProductMedias.Add(await sql.MapDataAsync<ProductMediaEntity>(reader));
                    break;
                case 2:
                    productEditEntity.Categories.Add(await sql.MapDataAsync<CategoryMainEntity>(reader));
                    break;
                case 3:
                    productEditEntity.VariantCombinations.Add(await sql.MapDataAsync<ProductVariantEntity>(reader));
                    break;
                case 4:
                    productEditEntity.Variants.Add(await sql.MapDataAsync<ProductEntity>(reader));
                    break;

            }
        }

        public async Task<ProductListEntity> SelectForList(ProductParameterEntity productParameterEntity)
        {
            if (productParameterEntity.Name != string.Empty)
                sql.AddParameter("Name", productParameterEntity.Name);

            if (productParameterEntity.CategoryId != 0)
                sql.AddParameter("CategoryId", productParameterEntity.CategoryId);

            if (productParameterEntity.Description != string.Empty)
                sql.AddParameter("Description", productParameterEntity.Description);

            sql.AddParameter("SortExpression", productParameterEntity.SortExpression);
            sql.AddParameter("SortDirection", productParameterEntity.SortDirection);
            sql.AddParameter("PageIndex", productParameterEntity.PageIndex);
            sql.AddParameter("PageSize", productParameterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<ProductListEntity>("Product_SelectForList", CommandType.StoredProcedure, 3, MapListEntity);
        }
                            
        public async Task MapListEntity(int resultSet, ProductListEntity productListEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    productListEntity.Categories.Add(await sql.MapDataAsync<CategoryMainEntity>(reader));
                    break;
                case 1:
                    productListEntity.Products.Add(await sql.MapDataAsync<ProductEntity>(reader));
                    break;
                case 2:
                    productListEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }
        public ProductEntity MapData(IDataReader reader)
        {
            ProductEntity productEntity = new ProductEntity();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Id":
                        productEntity.Id = MyConvert.ToInt(reader["Id"]);
                        break;
                    case "Name":
                        productEntity.Name = MyConvert.ToString(reader["Name"]);
                        break;
                    case "Description":
                        productEntity.Description = MyConvert.ToString(reader["Description"]);
                        break;
                    case "LongDescription":
                        productEntity.LongDescription = MyConvert.ToString(reader["LongDescription"]);
                        break;
                    case "CategoryId":
                        productEntity.CategoryId = MyConvert.ToInt(reader["CategoryId"]);
                        break;
                    case "CategoryName":
                        productEntity.CategoryName = MyConvert.ToString(reader["CategoryName"]);
                        break;
                    case "AllowReturn":
                        productEntity.AllowReturn = MyConvert.ToBoolean(reader["AllowReturn"]);
                        break;
                    case "ReturnPolicy":
                        productEntity.ReturnPolicy = MyConvert.ToString(reader["ReturnPolicy"]);
                        break;
                    case "IsExpiry":
                        productEntity.IsExpiry = MyConvert.ToBoolean(reader["IsExpiry"]);
                        break;
                    case "CreatedBy":
                        productEntity.CreatedBy = MyConvert.ToLong(reader["CreatedBy"]);
                        break;
                    case "CreatedOn":
                        productEntity.CreatedOn = MyConvert.ToDateTime(reader["CreatedOn"]);
                        break;
                    case "LastUpdatedBy":
                        productEntity.LastUpdatedBy = MyConvert.ToInt(reader["LastUpdatedBy"]);
                        break;
                    case "LastUpdatedOn":
                        productEntity.LastUpdatedOn = MyConvert.ToDateTime(reader["LastUpdatedOn"]);
                        break;
                    case "OpeningQty":
                        productEntity.OpeningQty = MyConvert.ToInt(reader["OpeningQty"]);
                        break;
                    case "BuyQty":
                        productEntity.BuyQty = MyConvert.ToInt(reader["BuyQty"]);
                        break;
                    case "LockQty":
                        productEntity.LockQty = MyConvert.ToInt(reader["LockQty"]);
                        break;
                    case "OrderQty":
                        productEntity.OrderQty = MyConvert.ToInt(reader["OrderQty"]);
                        break;
                    case "SellQty":
                        productEntity.SellQty = MyConvert.ToInt(reader["SellQty"]);
                        break;
                    case "ClosingQty":
                        productEntity.ClosingQty = MyConvert.ToInt(reader["ClosingQty"]);
                        break;
                    case "CostPrice":
                        productEntity.CostPrice = MyConvert.ToDouble(reader["CostPrice"]);
                        break;
                    case "SellPrice":
                        productEntity.SellPrice = MyConvert.ToDouble(reader["SellPrice"]);
                        break;
                    case "DiscountPercentage":
                        productEntity.DiscountPercentage = MyConvert.ToDouble(reader["DiscountPercentage"]);
                        break;
                    case "DiscountAmount":
                        productEntity.DiscountAmount = MyConvert.ToDouble(reader["DiscountAmount"]);
                        break;
                    case "FinalSellPrice":
                        productEntity.FinalSellPrice = MyConvert.ToDouble(reader["FinalSellPrice"]);
                        break;
                    case "MediaId":
                        ProductMediaEntity mediaEntity = new ProductMediaEntity
                        {
                            Id = MyConvert.ToInt(reader["MediaId"]),
                            ProductId = MyConvert.ToInt(reader["ProductId"]),
                            Type = MyConvert.ToInt(reader["Type"]),
                            Url = MyConvert.ToString(reader["Url"]),
                            ThumbUrl = MyConvert.ToString(reader["ThumbUrl"]),
                            Description = MyConvert.ToString(reader["Description"])
                        };
                        productEntity.ProductMedias.Add(mediaEntity);
                        break;
                }
            }
            return productEntity;
        }

        public async Task<ProductPropertyGridEntity> SelectForPropertyGrid(ProductPropertyParameterEntity productPropertyParameterEntity)
        {
            sql.AddParameter("ProductId", productPropertyParameterEntity.ProductId);
            sql.AddParameter("CategoryId", productPropertyParameterEntity.CategoryId);
            return await sql.ExecuteResultSetAsync<ProductPropertyGridEntity>("ProductProperty_SelectForGrid", CommandType.StoredProcedure, 2, MapPropertyGridEntity);
        }

        public async Task MapPropertyGridEntity(int resultSet, ProductPropertyGridEntity productPropertyGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    productPropertyGridEntity.ProductProperties.Add(await sql.MapDataAsync<ProductPropertyEntity>(reader));
                    break;
                case 1:
                    productPropertyGridEntity.VariantPropertyIds.Add(await sql.MapDataAsync<VariantProductPropertyEntity>(reader));
                    break;
            }
        }

        public async Task InsertProperty(ProductPropertyParameterEntity productPropertyParameterEntity)
        {
            sql.AddParameter("ProductId", productPropertyParameterEntity.ProductId);

            if (productPropertyParameterEntity.ProductProperties != null)
            {
                sql.AddParameter("ProductPropertyXML", productPropertyParameterEntity.ProductProperties.ToXML());
            }

            await sql.ExecuteScalarAsync("ProductProperty_Insert", CommandType.StoredProcedure);

            var productEntity = await document.GetByIdAsync(productPropertyParameterEntity.ProductId.ToString());
            productEntity ??= new ProductMongoEntity();

            var filteredProperties = productPropertyParameterEntity.ProductProperties
               .ToDictionary(
                   p => p.PropertyName,
                   p => p.Unit == "None" ? p.Value : p.Value + " " + p.Unit
               );

            productEntity.Properties = filteredProperties;

            await document.UpdateAsync(productEntity);
        }

        public async Task Sync(ProductParameterEntity productParamterEntity)
        {
            ProductEntity product = new ProductEntity();

            sql.AddParameter("Id", productParamterEntity.Id);
            product =  await sql.ExecuteRecordAsync<ProductEntity>("Product_SelectForSync", CommandType.StoredProcedure);

            var productPropertyParameterEntity = new ProductPropertyParameterEntity
            {
                ProductId = product.Id,
                CategoryId = product.CategoryId
            };

            var productProperties = await SelectForPropertyGrid(productPropertyParameterEntity);

            if (productProperties?.ProductProperties != null)
            {
                var filteredProperties = productProperties.ProductProperties
                    .ToDictionary(
                        p => p.PropertyName,
                        p => p.Unit == "None" ? p.Value : $"{p.Value} {p.Unit}"
                    );

                product.Properties = filteredProperties;
            }

            await document.UpdateAsync(product.ToMongoEntity());
        }

        public async Task SyncAll()
        {
            List<ProductEntity> products = new List<ProductEntity>();

            products = await sql.ExecuteListAsync<ProductEntity>("Product_SelectForSyncAll", CommandType.StoredProcedure);

            foreach (var productEntity in products)
            {
                var productPropertyParameterEntity = new ProductPropertyParameterEntity
                {
                    ProductId = productEntity.Id,
                    CategoryId = productEntity.CategoryId
                };

                var productProperties = await SelectForPropertyGrid(productPropertyParameterEntity);

                if (productProperties?.ProductProperties != null)
                {
                    var filteredProperties = productProperties.ProductProperties
                        .ToDictionary(
                            p => p.PropertyName,
                            p => p.Unit == "None" ? p.Value : $"{p.Value} {p.Unit}"
                        );

                    productEntity.Properties = filteredProperties;
                }

                document.UpdateAsync(productEntity.ToMongoEntity());
            }
        }
    }
}