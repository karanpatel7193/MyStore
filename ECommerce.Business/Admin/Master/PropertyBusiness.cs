using AdvancedADO;
using CommonLibrary;
using ECommerce.Entity.Admin.Master;
using ECommerce.Repository;
using ECommerce.Repository.Admin.Master;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Admin.Master
{
    public class PropertyBusiness : CommonBusiness, IPropertyRepository
    {
        ISql sql;

        #region Constructor
        /// <summary>
        /// This construction is set properties default value based on its data type in table.
        /// </summary>
        public PropertyBusiness(IConfiguration configuration) : base(configuration)
        {
            sql = CreateSqlInstance();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This function return map reader table field to Entity of Property.
        /// </summary>
        /// <returns>Entity</returns>
        public PropertyEntity MapData(IDataReader reader)
        {
            PropertyEntity propertyEntity = new PropertyEntity();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Id":
                        propertyEntity.Id = MyConvert.ToInt(reader["Id"]);
                        break;
                    case "Name":
                        propertyEntity.Name = MyConvert.ToString(reader["Name"]);
                        break;
                    case "Description":
                        propertyEntity.Description = MyConvert.ToString(reader["Description"]);
                        break;
                }
            }
            return propertyEntity;
        }

        /// <summary>
        /// This function return all columns values for perticular Property record
        /// </summary>
        /// <param name="Id">Perticular Record</param>
        /// <returns>Entity</returns>
        public async Task<PropertyEntity> SelectForRecord(int Id)
        {
            sql.AddParameter("Id", Id);
            return await sql.ExecuteRecordAsync<PropertyEntity>("Property_SelectForRecord", CommandType.StoredProcedure);
        }

        /// <summary>
        /// This function returns main informations for bind Property LOV
        /// </summary>
        /// <param name="PropertyParameterEntity">Filter criteria by Entity</param>
        /// <returns>Main Entitys</returns>
		public async Task<List<PropertyMainEntity>> SelectForLOV(PropertyParameterEntity PropertyParameterEntity)
        {
            return await sql.ExecuteListAsync<PropertyMainEntity>("Property_SelectForLOV", CommandType.StoredProcedure);
        }

        /// <summary>
        /// This function returns Property list page grid data.
        /// </summary>
        /// <param name="PropertyParameterEntity">Filter paramters</param>
        /// <returns>Property grid data</returns>
        public async Task<PropertyGridEntity> SelectForGrid(PropertyParameterEntity PropertyParameterEntity)
        {
            PropertyGridEntity PropertyGridEntity = new PropertyGridEntity();
            if (PropertyParameterEntity.Name != string.Empty)
                sql.AddParameter("Name", PropertyParameterEntity.Name);

            sql.AddParameter("SortExpression", PropertyParameterEntity.SortExpression);
            sql.AddParameter("SortDirection", PropertyParameterEntity.SortDirection);
            sql.AddParameter("PageIndex", PropertyParameterEntity.PageIndex);
            sql.AddParameter("PageSize", PropertyParameterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<PropertyGridEntity>("Property_SelectForGrid", CommandType.StoredProcedure, 2, MapGridEntity);
        }

        /// <summary>
        /// This function map data for Property grid data
        /// </summary>
        /// <param name="resultSet">Result Set No</param>
        /// <param name="PropertyGridEntity">Property grid data</param>
        /// <param name="reader">Database reader</param>
        public async Task MapGridEntity(int resultSet, PropertyGridEntity PropertyGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    PropertyGridEntity.Properties.Add(await sql.MapDataAsync<PropertyEntity>(reader));
                    break;
                case 1:
                    PropertyGridEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }

        /// <summary>
        /// This function insert record in Property table.
        /// </summary>
        /// <returns>Identity / AlreadyExist = 0</returns>
        public async Task<int> Insert(PropertyEntity propertyEntity)
        {
            sql.AddParameter("Name", propertyEntity.Name);
            sql.AddParameter("Description", propertyEntity.Description);
            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Property_Insert", CommandType.StoredProcedure));
        }

        /// <summary>
        /// This function update record in Property table.
        /// </summary>
        /// <returns>PrimaryKey Field Value / AlreadyExist = 0</returns>
        public async Task<int> Update(PropertyEntity propertyEntity)
        {
            sql.AddParameter("Id", propertyEntity.Id);
            sql.AddParameter("Name", propertyEntity.Name);
            sql.AddParameter("Description", propertyEntity.Description);
            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Property_Update", CommandType.StoredProcedure));
        }

        /// <summary>
        /// This function delete record from Property table.
        /// </summary>
        public async Task Delete(int Id)
        {
            sql.AddParameter("Id", Id);
            await sql.ExecuteNonQueryAsync("Property_Delete", CommandType.StoredProcedure);
        }

        /// <summary>
        /// This function returns main informations for bind Property LOV
        /// </summary>
        /// <param name="PropertyParameterEntity">Filter criteria by Entity</param>
        /// <returns>Main Entitys</returns>
		public List<PropertyEntity> SelectList()
        {
            return sql.ExecuteList<PropertyEntity>("Property_Select", CommandType.StoredProcedure);
        }

        #endregion
    }

}
