using ECommerce.Entity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Account
{
    /// <summary>
    /// This interface having crud operation methods declarations of table Employee
    /// Created By :: Rekansh Patel
    /// Created On :: 06/18/2025
    /// </summary>
    public interface IEmployeeRepository : IBusiness<EmployeeEntity, EmployeeMainEntity, EmployeeAddEntity, EmployeeEditEntity, EmployeeGridEntity, EmployeeListEntity, EmployeeParameterEntity, int>
    {

    }

}
