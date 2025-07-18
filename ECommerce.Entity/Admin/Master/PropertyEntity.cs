using ECommerce.Entity.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entity.Admin.Master
{
    public class PropertyMainEntity
    {
        public int Id { set; get; } = 0;
        public string Name { get; set; } = string.Empty;

    }

    public class PropertyEntity : PropertyMainEntity
    {
        public string Description { get; set; } = string.Empty;

    }

    public class PropertyGridEntity
    {
        public List<PropertyEntity> Properties { get; set; } = new List<PropertyEntity>();
        public int TotalRecords { get; set; }
    }

    public class PropertyParameterEntity : PagingSortingEntity
    {
        #region Constructor
        /// <summary>
        /// This construction is set properties default value based on its data type in table.
        /// </summary>
        public PropertyParameterEntity()
        {
            SetDefaulValue();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Get & Set Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get & Set Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get & Set Is Public
        /// </summary>

        #endregion

        #region Private Methods
        /// <summary>
        /// This function is set properties default value based on its data type in table.
        /// </summary>
        private void SetDefaulValue()
        {
            Id = 0;
            Name = string.Empty;

        }
        #endregion
    }
}
























//public class CategoryPropertyEntity
//{
//    public int Id { set; get; } = 0;
//    public string Value { get; set; } = string.Empty;
//    public string Unit { get; set; } = string.Empty;
//    public int CategoryId { set; get; } = 0;
//    public int CategoryPropertyId { set; get; } = 0;
//}
//public class ProductPropertyEntity
//{
//    public int Id { get; set; } = 0;
//    public int ProductId { get; set; } = 0;
//    public int PropertyId { get; set; } = 0;
//    public int CategoryPropertyId { get; set; } = 0;
//}
