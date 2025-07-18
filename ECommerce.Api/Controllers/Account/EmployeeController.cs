using CommonLibrary;
using ECommerce.Api.Common;
using ECommerce.Entity.Account;
using ECommerce.Repository;
using ECommerce.Repository.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Account
{
    /// <summary>
    /// This API use for employee related operation like list, insert, update, delete employee from database etc.
    /// </summary>
    [Route("account/employee/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase, IPageController<EmployeeEntity, EmployeeParameterEntity, int>
    {
        //services.AddScoped<IEmployeeRepository, EmployeeBusiness>();  //Move this line in Startup.cs file
        IEmployeeRepository employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        #region Interface public methods
        /// <summary>
        /// Get all columns information for particular employee record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getRecord/{id:int}", Name = "account.employee.employee.record")]
        [AuthorizeAPI(pageName: "Employee", pageAccess: PageAccessValues.View)]
        public async Task<Response> GetForRecord(int id)
        {
            Response response;
            try
            {
                response = new Response(await employeeRepository.SelectForRecord(id));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        /// <summary>
        /// Get main columns informations for bind employee LOV
        /// </summary>
        /// <param name="employeeParameterEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getLovValue", Name = "account.employee.employee.lovValue")]
        [AuthorizeAPI(pageName: "Employee", pageAccess: PageAccessValues.IgnoreAuthorization)]
        public async Task<Response> GetForLOV(EmployeeParameterEntity employeeParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await employeeRepository.SelectForLOV(employeeParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        /// <summary>
        /// Get employee's page all LOV data when employee page open in add mode.
        /// </summary>
        /// <param name="employeeParameterEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAddMode", Name = "account.employee.employee.addMode")]
        [AuthorizeAPI(pageName: "Employee", pageAccess: PageAccessValues.Insert)]
        public async Task<Response> GetForAdd(EmployeeParameterEntity employeeParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await employeeRepository.SelectForAdd(employeeParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        /// <summary>
        /// Get employee's page all LOV data and all columns information for edit record when employee page open in edit mode.
        /// </summary>
        /// <param name="employeeParameterEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getEditMode", Name = "account.employee.employee.editMode")]
        [AuthorizeAPI(pageName: "Employee", pageAccess: PageAccessValues.Update)]
        public async Task<Response> GetForEdit(EmployeeParameterEntity employeeParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await employeeRepository.SelectForEdit(employeeParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        /// <summary>
        /// Get employee list for bind grid.
        /// </summary>
        /// <param name="employeeParameterEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getGridData", Name = "account.employee.employee.gridData")]
        [AuthorizeAPI(pageName: "Employee", pageAccess: PageAccessValues.View)]
        public async Task<Response> GetForGrid(EmployeeParameterEntity employeeParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await employeeRepository.SelectForGrid(employeeParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        /// <summary>
        /// Get employee's page all LOV data and grid result when employee page open in list mode.
        /// </summary>
        /// <param name="employeeParameterEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getListMode", Name = "account.employee.employee.listMode")]
        [AuthorizeAPI(pageName: "Employee", pageAccess: PageAccessValues.View)]
        public async Task<Response> GetForList(EmployeeParameterEntity employeeParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await employeeRepository.SelectForList(employeeParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        /// <summary>
        /// Insert record in employee table.
        /// </summary>
        /// <param name="employeeEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insert", Name = "account.employee.employee.insert")]
        [AuthorizeAPI(pageName: "Employee", pageAccess: PageAccessValues.Insert)]
        public async Task<Response> Insert(EmployeeEntity employeeEntity)
        {
            Response response;
            try
            {
                response = new Response(await employeeRepository.Insert(employeeEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        /// <summary>
        /// Update record in employee table.
        /// </summary>
        /// <param name="employeeEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update", Name = "account.employee.employee.update")]
        [AuthorizeAPI(pageName: "Employee", pageAccess: PageAccessValues.Update)]
        public async Task<Response> Update(EmployeeEntity employeeEntity)
        {
            Response response;
            try
            {
                response = new Response(await employeeRepository.Update(employeeEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        /// <summary>
        /// Delete record from employee table.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete/{id:int}", Name = "account.employee.employee.delete")]
        [AuthorizeAPI(pageName: "Employee", pageAccess: PageAccessValues.Delete)]
        public async Task<Response> Delete(int id)
        {
            Response response;
            try
            {
                await employeeRepository.Delete(id);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
        #endregion
    }



}
