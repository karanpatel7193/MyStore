using CommonLibrary;
using ECommerce.Entity.Account;
using ECommerce.Entity.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ECommerce.Entity.Admin.Master
{
    public class CategoryMainEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
    }

    public class CategoryEntity
    {
        public FileEntity File { get; set; } = null;
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public int ParentId { get; set; } = 0;
        public string ParentName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsVisible { get; set; } = false;

        public class CategoryGridEntity
        {
            public List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();
            public int TotalRecords { get; set; }
        }
        public class CategoryListEntity : CategoryGridEntity
        {
        }

        public class CategoryAddEntity
        {
            public List<CategoryMainEntity> ParentCategories { get; set; } = new List<CategoryMainEntity>();

        }
        public class CategoryEditEntity : CategoryAddEntity
        {
            public CategoryEntity Category { get; set; } = new CategoryEntity();
        }

        public class CategoryParemeterEntity : PagingSortingEntity
        {
            public int Id { get; set; } = 0;
            public string Name { set; get; } = string.Empty;
            public int ParentId { get; set; } = 0;
        }

        public class CategoryImageResultEntity
        {
            public string Status { get; set; }
            public string ImageSrc { get; set; }
            public string ErrorMessage { get; set; }
        }

        public class CategoryPropertyValueEntity
        {
            public int CategoryId { get; set; } = 0;
            public int PropertyId { get; set; } = 0;
            public string Value { get; set; } = string.Empty;
            public string Unit { get; set; } = String.Empty;
        }
    }
}
