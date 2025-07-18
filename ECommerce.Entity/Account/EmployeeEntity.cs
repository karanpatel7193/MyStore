using ECommerce.Entity.Admin.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entity.Account
{

    /// <summary>
    /// This class having main entities of table Employee
    /// Created By :: Rekansh Patel
    /// Created On :: 06/18/2025
    /// </summary>
    public class EmployeeMainEntity
    {
        #region Constructor
        /// <summary>
        /// This construction is set properties default value based on its data type in table.
        /// </summary>
        public EmployeeMainEntity()
        {
            SetDefaulValue();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Get & Set Id
        /// </summary>
        public int Id { get; set; }


        #endregion

        #region Private Methods
        /// <summary>
        /// This function is set properties default value based on its data type in table.
        /// </summary>
        private void SetDefaulValue()
        {
            Id = 0;

        }
        #endregion
    }

    /// <summary>
    /// This class having entities of table Employee
    /// Created By :: Rekansh Patel
    /// Created On :: 06/18/2025
    /// </summary>
    public class EmployeeEntity : EmployeeMainEntity
    {
        #region Constructor
        /// <summary>
        /// This construction is set properties default value based on its data type in table.
        /// </summary>
        public EmployeeEntity()
        {
            SetDefaulValue();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Get & Set First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Get & Set Middle Name
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Get & Set Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Get & Set Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Get & Set Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Get & Set Phone Number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Get & Set DOB
        /// </summary>
        public DateTime DOB { get; set; } 

        /// <summary>
        /// Get & Set Date Of Join
        /// </summary>
        public DateTime DateOfJoin { get; set; }

        /// <summary>
        /// Get & Set Education
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// Get & Set City Id
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Get & Set State Id
        /// </summary>
        public int StateId { get; set; }

        /// <summary>
        /// Get & Set Country Id
        /// </summary>
        public int CountryId { get; set; }


        #endregion

        #region Private Methods
        /// <summary>
        /// This function is set properties default value based on its data type in table.
        /// </summary>
        private void SetDefaulValue()
        {
            FirstName = string.Empty;
            MiddleName = string.Empty;
            LastName = string.Empty;
            Gender = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            DOB = DateTime.MinValue;
            DateOfJoin = DateTime.MinValue;
            Education = string.Empty;
            CityId = 0;
            StateId = 0;
            CountryId = 0;

        }
        #endregion
    }

    public class EmployeeAddEntity
    {
        public List<CountryMainEntity> Countrys { get; set; } = new List<CountryMainEntity>();
        public List<StateMainEntity> States { get; set; } = new List<StateMainEntity>();
        public List<CityMainEntity> Citys { get; set; } = new List<CityMainEntity>();

    }

    public class EmployeeEditEntity : EmployeeAddEntity
    {
        public EmployeeEntity Employee { get; set; } = new EmployeeEntity();
    }

    public class EmployeeGridEntity
    {
        public List<EmployeeEntity> Employees { get; set; } = new List<EmployeeEntity>();
        public int TotalRecords { get; set; }
    }

    public class EmployeeListEntity : EmployeeGridEntity
    {
        public List<CountryMainEntity> Countrys { get; set; } = new List<CountryMainEntity>();
        public List<StateMainEntity> States { get; set; } = new List<StateMainEntity>();
        public List<CityMainEntity> Citys { get; set; } = new List<CityMainEntity>();

    }

    public class EmployeeParameterEntity : PagingSortingEntity
    {
        #region Constructor
        /// <summary>
        /// This construction is set properties default value based on its data type in table.
        /// </summary>
        public EmployeeParameterEntity()
        {
            SetDefaulValue();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Get & Set State Id
        /// </summary>
        public int StateId { get; set; }

        /// <summary>
        /// Get & Set Country Id
        /// </summary>
        public int CountryId { get; set; }

        public int Id { get; set; }

        #endregion

        #region Private Methods
        /// <summary>
        /// This function is set properties default value based on its data type in table.
        /// </summary>
        private void SetDefaulValue()
        {
            StateId = 0;
            CountryId = 0;

        }
        #endregion
    }
}
