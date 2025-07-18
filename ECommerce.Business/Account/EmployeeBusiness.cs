using AdvancedADO;
using CommonLibrary;
using ECommerce.Entity.Account;
using ECommerce.Entity.Admin.Globalization;
using ECommerce.Repository.Account;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Account
{
    /// <summary>
	/// This class having crud operation function of table Employee
	/// Created By :: Rekansh Patel
	/// Created On :: 06/18/2025
	/// </summary>
	public class EmployeeBusiness : CommonBusiness, IEmployeeRepository
    {
        ISql sql;

        #region Constructor
        /// <summary>
        /// This construction is set properties default value based on its data type in table.
        /// </summary>
        public EmployeeBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }
        #endregion

        #region Public Default Methods
        /// <summary>
        /// This function return map reader table field to Entity of Employee.
        /// </summary>
        /// <returns>Entity</returns>
        public EmployeeEntity MapData(IDataReader reader)
        {
            EmployeeEntity employeeEntity = new EmployeeEntity();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Id":
                        employeeEntity.Id = MyConvert.ToInt(reader["Id"]);
                        break;
                    case "FirstName":
                        employeeEntity.FirstName = MyConvert.ToString(reader["FirstName"]);
                        break;
                    case "MiddleName":
                        employeeEntity.MiddleName = MyConvert.ToString(reader["MiddleName"]);
                        break;
                    case "LastName":
                        employeeEntity.LastName = MyConvert.ToString(reader["LastName"]);
                        break;
                    case "Gender":
                        employeeEntity.Gender = MyConvert.ToString(reader["Gender"]);
                        break;
                    case "Email":
                        employeeEntity.Email = MyConvert.ToString(reader["Email"]);
                        break;
                    case "PhoneNumber":
                        employeeEntity.PhoneNumber = MyConvert.ToString(reader["PhoneNumber"]);
                        break;
                    case "DOB":
                        employeeEntity.DOB = MyConvert.ToDateTime(reader["DOB"]);
                        break;
                    case "DateOfJoin":
                        employeeEntity.DateOfJoin = MyConvert.ToDateTime(reader["DateOfJoin"]);
                        break;
                    case "Education":
                        employeeEntity.Education = MyConvert.ToString(reader["Education"]);
                        break;
                    case "CityId":
                        employeeEntity.CityId = MyConvert.ToInt(reader["CityId"]);
                        break;
                    case "StateId":
                        employeeEntity.StateId = MyConvert.ToInt(reader["StateId"]);
                        break;
                    case "CountryId":
                        employeeEntity.CountryId = MyConvert.ToInt(reader["CountryId"]);
                        break;

                }
            }
            return employeeEntity;
        }

        /// <summary>
        /// This function return all columns values for particular Employee record
        /// </summary>
        /// <param name="Id">Particular Record</param>
        /// <returns>Entity</returns>
        public async Task<EmployeeEntity> SelectForRecord(int id)
        {
            sql.AddParameter("Id", id);
            return await sql.ExecuteRecordAsync<EmployeeEntity>("Employee_SelectForRecord", CommandType.StoredProcedure);
        }

        /// <summary>
        /// This function returns main informations for bind Employee LOV
        /// </summary>
        /// <param name="employeeEntity">Filter criteria by Entity</param>
        /// <returns>Main Entitys</returns>
		public async Task<List<EmployeeMainEntity>> SelectForLOV(EmployeeParameterEntity employeeParameterEntity)
        {
            if (employeeParameterEntity.StateId != 0)
                sql.AddParameter("StateId", employeeParameterEntity.StateId);
            if (employeeParameterEntity.CountryId != 0)
                sql.AddParameter("CountryId", employeeParameterEntity.CountryId);

            return await sql.ExecuteListAsync<EmployeeMainEntity>("Employee_SelectForLOV", CommandType.StoredProcedure);
        }

        /// <summary>
        /// This function return all LOVs data for Employee page add mode
        /// </summary>
        /// <param name="employeeParameterEntity">Parameter</param>
        /// <returns>Add modes all LOVs data</returns>
        public async Task<EmployeeAddEntity> SelectForAdd(EmployeeParameterEntity employeeParameterEntity)
        {
            if (employeeParameterEntity.StateId != 0)
                sql.AddParameter("StateId", employeeParameterEntity.StateId);
            if (employeeParameterEntity.CountryId != 0)
                sql.AddParameter("CountryId", employeeParameterEntity.CountryId);

            return await sql.ExecuteResultSetAsync<EmployeeAddEntity>("Employee_SelectForAdd", CommandType.StoredProcedure, 3, MapAddEntity);
        }

        /// <summary>
        /// This function map data for Employee page add mode LOVs
        /// </summary>
        /// <param name="resultSet">Result Set No</param>
        /// <param name="employeeAddEntity">Add mode Entity for fill data</param>
        /// <param name="reader">Database reader</param>
        public async Task MapAddEntity(int resultSet, EmployeeAddEntity employeeAddEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    employeeAddEntity.Countrys.Add(await sql.MapDataAsync<CountryMainEntity>(reader));
                    break;
                case 1:
                    employeeAddEntity.States.Add(await sql.MapDataAsync<StateMainEntity>(reader));
                    break;
                case 2:
                    employeeAddEntity.Citys.Add(await sql.MapDataAsync<CityMainEntity>(reader));
                    break;

            }
        }

        /// <summary>
        /// This function return all LOVs data and edit record information for Employee page edit mode
        /// </summary>
        /// <param name="employeeParameterEntity">Parameter</param>
        /// <returns>Edit modes all LOVs data and edit record information</returns>
        public async Task<EmployeeEditEntity> SelectForEdit(EmployeeParameterEntity employeeParameterEntity)
        {
            sql.AddParameter("Id", employeeParameterEntity.Id);
            if (employeeParameterEntity.StateId != 0)
                sql.AddParameter("StateId", employeeParameterEntity.StateId);
            if (employeeParameterEntity.CountryId != 0)
                sql.AddParameter("CountryId", employeeParameterEntity.CountryId);

            return await sql.ExecuteResultSetAsync<EmployeeEditEntity>("Employee_SelectForEdit", CommandType.StoredProcedure, 4, MapEditEntity);
        }

        /// <summary>
        /// This function map data for Employee page edit mode LOVs and edit record information
        /// </summary>
        /// <param name="resultSet">Result Set No</param>
        /// <param name="employeeEditEntity">Edit mode Entity for fill data and edit record information</param>
        /// <param name="reader">Database reader</param>
        public async Task MapEditEntity(int resultSet, EmployeeEditEntity employeeEditEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    employeeEditEntity.Employee = await sql.MapDataAsync<EmployeeEntity>(reader);
                    break;
                case 1:
                    employeeEditEntity.Countrys.Add(await sql.MapDataAsync<CountryMainEntity>(reader));
                    break;
                case 2:
                    employeeEditEntity.States.Add(await sql.MapDataAsync<StateMainEntity>(reader));
                    break;
                case 3:
                    employeeEditEntity.Citys.Add(await sql.MapDataAsync<CityMainEntity>(reader));
                    break;

            }
        }

        /// <summary>
        /// This function returns Employee list page grid data.
        /// </summary>
        /// <param name="employeeParameterEntity">Filter paramters</param>
        /// <returns>Employee grid data</returns>
        public async Task<EmployeeGridEntity> SelectForGrid(EmployeeParameterEntity employeeParameterEntity)
        {
            if (employeeParameterEntity.StateId != 0)
                sql.AddParameter("StateId", employeeParameterEntity.StateId);
            if (employeeParameterEntity.CountryId != 0)
                sql.AddParameter("CountryId", employeeParameterEntity.CountryId);

            sql.AddParameter("SortExpression", employeeParameterEntity.SortExpression);
            sql.AddParameter("SortDirection", employeeParameterEntity.SortDirection);
            sql.AddParameter("PageIndex", employeeParameterEntity.PageIndex);
            sql.AddParameter("PageSize", employeeParameterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<EmployeeGridEntity>("Employee_SelectForGrid", CommandType.StoredProcedure, 2, MapGridEntity);
        }

        /// <summary>
        /// This function map data for Employee grid data
        /// </summary>
        /// <param name="resultSet">Result Set No</param>
        /// <param name="employeeGridEntity">Employee grid data</param>
        /// <param name="reader">Database reader</param>
        public async Task MapGridEntity(int resultSet, EmployeeGridEntity employeeGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    employeeGridEntity.Employees.Add(await sql.MapDataAsync<EmployeeEntity>(reader));
                    break;
                case 1:
                    employeeGridEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }

        /// <summary>
        /// This function returns Employee list page grid data and LOV data
        /// </summary>
        /// <param name="employeeParameterEntity">Filter paramters</param>
        /// <returns>Employee grid data and LOV data</returns>
        public async Task<EmployeeListEntity> SelectForList(EmployeeParameterEntity employeeParameterEntity)
        {
            if (employeeParameterEntity.StateId != 0)
                sql.AddParameter("StateId", employeeParameterEntity.StateId);
            if (employeeParameterEntity.CountryId != 0)
                sql.AddParameter("CountryId", employeeParameterEntity.CountryId);

            sql.AddParameter("SortExpression", employeeParameterEntity.SortExpression);
            sql.AddParameter("SortDirection", employeeParameterEntity.SortDirection);
            sql.AddParameter("PageIndex", employeeParameterEntity.PageIndex);
            sql.AddParameter("PageSize", employeeParameterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<EmployeeListEntity>("Employee_SelectForList", CommandType.StoredProcedure, 5, MapListEntity);
        }

        /// <summary>
        /// This function map data for Employee list page grid data and LOV data
        /// </summary>
        /// <param name="resultSet">Result Set No</param>
        /// <param name="employeeListEntity">Employee list page grid data and LOV data</param>
        /// <param name="reader">Database reader</param>
        public async Task MapListEntity(int resultSet, EmployeeListEntity employeeListEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    employeeListEntity.Countrys.Add(await sql.MapDataAsync<CountryMainEntity>(reader));
                    break;
                case 1:
                    employeeListEntity.States.Add(await sql.MapDataAsync<StateMainEntity>(reader));
                    break;
                case 2:
                    employeeListEntity.Citys.Add(await sql.MapDataAsync<CityMainEntity>(reader));
                    break;

                case 3:
                    employeeListEntity.Employees.Add(await sql.MapDataAsync<EmployeeEntity>(reader));
                    break;
                case 4:
                    employeeListEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }

        /// <summary>
        /// This function insert record in Employee table.
        /// </summary>
        /// <returns>Identity / AlreadyExist = 0</returns>
        public async Task<int> Insert(EmployeeEntity employeeEntity)
        {
            sql.AddParameter("FirstName", employeeEntity.FirstName);
            if (employeeEntity.MiddleName != string.Empty)
                sql.AddParameter("MiddleName", employeeEntity.MiddleName);
            sql.AddParameter("LastName", employeeEntity.LastName);
            sql.AddParameter("Gender", employeeEntity.Gender);
            sql.AddParameter("Email", employeeEntity.Email);
            if (employeeEntity.PhoneNumber != string.Empty)
                sql.AddParameter("PhoneNumber", employeeEntity.PhoneNumber);
            sql.AddParameter("DOB", DbType.Date, ParameterDirection.Input, employeeEntity.DOB);
            sql.AddParameter("DateOfJoin", DbType.Date, ParameterDirection.Input, employeeEntity.DateOfJoin);
            if (employeeEntity.Education != string.Empty)
                sql.AddParameter("Education", employeeEntity.Education);
            if (employeeEntity.CityId != 0)
                sql.AddParameter("CityId", employeeEntity.CityId);
            if (employeeEntity.StateId != 0)
                sql.AddParameter("StateId", employeeEntity.StateId);
            if (employeeEntity.CountryId != 0)
                sql.AddParameter("CountryId", employeeEntity.CountryId);
            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Employee_Insert", CommandType.StoredProcedure));
        }

        /// <summary>
        /// This function update record in Employee table.
        /// </summary>
        /// <returns>PrimaryKey Field Value / AlreadyExist = 0</returns>
        public async Task<int> Update(EmployeeEntity employeeEntity)
        {
            sql.AddParameter("Id", employeeEntity.Id);
            sql.AddParameter("FirstName", employeeEntity.FirstName);
            if (employeeEntity.MiddleName != string.Empty)
                sql.AddParameter("MiddleName", employeeEntity.MiddleName);
            sql.AddParameter("LastName", employeeEntity.LastName);
            sql.AddParameter("Gender", employeeEntity.Gender);
            sql.AddParameter("Email", employeeEntity.Email);
            if (employeeEntity.PhoneNumber != string.Empty)
                sql.AddParameter("PhoneNumber", employeeEntity.PhoneNumber);
            sql.AddParameter("DOB", DbType.Date, ParameterDirection.Input, employeeEntity.DOB);
            sql.AddParameter("DateOfJoin", DbType.Date, ParameterDirection.Input, employeeEntity.DateOfJoin);
            if (employeeEntity.Education != string.Empty)
                sql.AddParameter("Education", employeeEntity.Education);
            if (employeeEntity.CityId != 0)
                sql.AddParameter("CityId", employeeEntity.CityId);
            if (employeeEntity.StateId != 0)
                sql.AddParameter("StateId", employeeEntity.StateId);
            if (employeeEntity.CountryId != 0)
                sql.AddParameter("CountryId", employeeEntity.CountryId);
            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Employee_Update", CommandType.StoredProcedure));
        }

        /// <summary>
        /// This function delete record from Employee table.
        /// </summary>
        public async Task Delete(int id)
        {
            sql.AddParameter("Id", id);
            await sql.ExecuteNonQueryAsync("Employee_Delete", CommandType.StoredProcedure);
        }
        #endregion
    }


}
