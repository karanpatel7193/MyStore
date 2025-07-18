namespace ECommerce.Entity.Admin.Master.CategoryProperty
{
    public class CategoryPropertyMainEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;

    }
    public class CategoryPropertyEntity : CategoryPropertyMainEntity
    {
        public int CategoryId { get; set; }= 0;
        public int PropertyId { get; set; } = 0;
        public string PropertyName { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;

    }

    public class CategoryPropertyGridEntity
    {
       public List<CategoryPropertyEntity> CategoryPropertys { get; set; } = new List<CategoryPropertyEntity>();
       public int TotalRecords { get; set; }

    }
    public class CategoryPropertyAddEntity
    {
        public List<PropertyMainEntity> Properties { get; set; } = new List<PropertyMainEntity>();
    }
    public class CategoryPropertyEditEntity : CategoryPropertyAddEntity
    {
        public CategoryPropertyEntity CategoryProperty { get; set; } = new  CategoryPropertyEntity();
    }

    public class CategoryPropertyListEntity : CategoryPropertyGridEntity
    {
        public List<CategoryPropertyEntity> CategoryProperties { get; set; } = new List<CategoryPropertyEntity>();
    }

    public class CategoryPropertyParameterEntity : PagingSortingEntity
    {
        public int CategoryId { get; set; } = 0;
        public int PropertyId { get; set; } = 0;
        public string Unit { get; set; } = string.Empty;
    }

}
