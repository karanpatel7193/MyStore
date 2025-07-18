using ECommerce.Entity.Account;
using ECommerce.Entity.Admin.Globalization;

namespace ECommerce.Entity.Admin.Vendor
{
    public class VendorMainEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
    }

    public class VendorEntity : VendorMainEntity
    {
        public string Email { get; set; } = string.Empty;
        public long Phone { get; set; }  
        public string Address { get; set; } = string.Empty;
        public int StateId { get; set; } = 0;
        public int CountryId { get; set; } = 0;
        public string PostalCode { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string IFSCCode { get; set; } = string.Empty;
        public string ContactPersonName { get; set; } = string.Empty;
        public string ContactPersonPhone { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.MinValue;
        public string TaxNumber { get; set; } = string.Empty; 
        public decimal TotalOutstanding { get; set; } = 0;  
        public decimal TotalPaid { get; set; } = 0; 
        public decimal TotalInvoices { get; set; } = 0;  
        public bool Status { get; set; }
    }


    public class VendorAddEntity
    {
        public List<CountryMainEntity> Countries { get; set; } = new List<CountryMainEntity>();
    }

    public class VendorEditEntity : VendorAddEntity
    {
        public VendorEntity Vendor { get; set; } = new VendorEntity();
        public List<StateMainEntity> States { get; set; } = new List<StateMainEntity>();
    }

    public class VendorGridEntity
    {
        public List<VendorEntity> Vendors { get; set; } = new List<VendorEntity>();
        public int TotalRecords { get; set; }
    }

    public class VendorListEntity : VendorGridEntity
    {
        public List<CountryMainEntity> Countries { get; set; } = new List<CountryMainEntity>();
        public List<StateMainEntity> States { get; set; } = new List<StateMainEntity>();
    }

    public class VendorParameterEntity : PagingSortingEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; 
        public long Phone { get; set; } = 0; 
        public string PostalCode { get; set; } = string.Empty; 
        public short CountryId { get; set; } = 0; 
        public int StateId { get; set; } = 0; 
    }


}
