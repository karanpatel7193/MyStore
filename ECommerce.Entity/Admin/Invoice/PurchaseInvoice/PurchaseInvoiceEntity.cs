using ECommerce.Entity.Admin.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entity.Admin.Purchase.PurchaseInvoice
{
    public class PurchaseInvoiceMainEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;

    }

    public class PurchaseInvoiceEntity : PurchaseInvoiceMainEntity
    {
        public int InvoiceNumber { get; set; } = 0;
        public long CreatedBy { get; set; } = 0;
        public DateTime CreatedOn { get; set; }
        public long LastUpdatedBy { get; set; } = 0;
        public DateTime LastUpdatedOn { get; set; } = DateTime.UtcNow;
        public int VendorId { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public double TotalQuantity { get; set; } = 0;
        public double TotalAmount { get; set; } = 0;
        public double TotalDiscount { get; set; } = 0;
        public double TotalTax { get; set; } = 0;
        public double TotalFinalAmount { get; set; } = 0;

    }

    public class PurchaseInvoiceGridEntity
    {
        public List<PurchaseInvoiceEntity> PurchaseInvoices { get; set; } = new List<PurchaseInvoiceEntity>();
        public int TotalRecords { get; set; }
    }

    public class PurchaseInvoiceAddEntity
    {
        public List<CategoryMainEntity> Vendors { get; set; } = new List<CategoryMainEntity>();
    }

    public class PurchaseInvoiceEditEntity
    {
        public PurchaseInvoiceEntity PurchaseInvoices { get; set; } = new PurchaseInvoiceEntity();

    }

    public class PurchaseInvoiceParameterEntity : PagingSortingEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }


}
