namespace ECommerce.Entity.Admin.Master
{
    public class ProductPropertyMainEntity
    {
        public int Id { get; set; } = 0;
        public int PropertyId { get; set; } = 0;

    }
    public class ProductPropertyEntity : ProductPropertyMainEntity
    {
        public string PropertyName { get; set; } = string.Empty;
        public int ProductId { get; set; } = 0;
        public string Unit {  get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
    public class VariantProductPropertyEntity
    {
        public int VariantPropertyId { get; set; } = 0;
    }

    public class ProductPropertyGridEntity
    {
        public List<ProductPropertyEntity> ProductProperties { get; set; } = new List<ProductPropertyEntity>();
        public List<VariantProductPropertyEntity> VariantPropertyIds { get; set; } = new List<VariantProductPropertyEntity>();
    }

    public class ProductPropertyAddEntity
    {
        public List<ProductPropertyEntity> Properties { get; set; } = new List<ProductPropertyEntity>();
    }
  
    public class ProductPropertyEditEntity : ProductPropertyAddEntity
    {
        public ProductPropertyEntity ProductProperty { get; set; } = new ProductPropertyEntity();
    }
  
    public class ProductPropertyListEntity : ProductPropertyGridEntity
    {
    }
    public class ProductPropertyParameterEntity
    {
        public int Id { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public int CategoryId { get; set; } = 0;
        public List<ProductPropertyEntity> ProductProperties { get; set; } = new List<ProductPropertyEntity>();

    }
}
