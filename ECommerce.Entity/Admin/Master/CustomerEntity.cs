namespace ECommerce.Entity.Admin.Master
{
    public class CustomerMainEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
    }

    public class CustomerEntity : CustomerMainEntity
    {
        public int TotalBuy { get; set; } = 0;
        public string TotalInvoices { get; set; } = string.Empty;
        public int Status { get; set; } = 0;
        public long UserId { get; set; } = 0;
        public string UserName { get; set; } = string.Empty ;
    }

    public class CustomerGridEntity
    {
        public List<CustomerEntity> Customers { get; set; } = new List<CustomerEntity>();
        public int TotalRecords { get; set; }
    }

    public class CustomerParameterEntity : PagingSortingEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Status { get; set; } = 0;
        public long UserId { get; set; } = 0;
    }
}
