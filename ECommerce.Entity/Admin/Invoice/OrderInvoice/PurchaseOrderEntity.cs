using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Admin.Vendor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entity.Admin.Order.OrderInvoice
{
    public class PurchaseOrderMainEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;

    }
    public class PurchaseOrderItemEntity 
    {
        public int Id { get; set; } = 0;

        public int PurchaseOrderId { get; set; } = 0;

        public int ProductId { get; set; } = 0;

        public string ProductName { get; set; } = string.Empty;

        public int Quantity { get; set; } = 0;

        public double Price { get; set; } = 0;

        public double Amount { get; set; } = 0;

        public double DiscountPercentage { get; set; } = 0;

        public double DiscountedAmount { get; set; } = 0;

        public double Tax { get; set; } = 0;

        public double FinalAmount { get; set; } = 0;

        public DateTime ExpiryDate { get; set; }
    }
    public class PurchaseOrderEntity : PurchaseOrderMainEntity
    {
        public int OrderNumber { get; set; } = 0;
        public long CreatedBy { get; set; } = 0;
        public DateTime CreatedOn { get; set; }
        public long LastUpdatedBy { get; set; } = 0;
        public DateTime LastUpdatedOn { get; set; } = DateTime.UtcNow;
        public int VendorId { get; set; } = 0;
        public string VendorName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double TotalQuantity { get; set; } = 0;
        public double TotalAmount { get; set; } = 0;
        public double TotalDiscount { get; set; } = 0;
        public double TotalTax { get; set; } = 0;
        public double TotalFinalAmount { get; set; } = 0;

        public List<PurchaseOrderItemEntity> PurchaseOrderItem { get; set; } = new List<PurchaseOrderItemEntity>();
    
    }

    public class PurchaseOrderGridEntity
    {
        public List<PurchaseOrderEntity> OrderInvoices { get; set; } = new List<PurchaseOrderEntity>();
        public int TotalRecords { get; set; }
    }

    public class PurchaseOrderAddEntity
    {
        public List<VendorMainEntity> Vendors { get; set; } = new List<VendorMainEntity>();
        public List<ProductMainEntity> Products { get; set; } = new List<ProductMainEntity>();

    }

    public class PurchaseOrderEditEntity : PurchaseOrderAddEntity
    {
        public PurchaseOrderEntity PurchaseOrder { get; set; } = new PurchaseOrderEntity();

    }

    public class PurchaseOrderListEntity : PurchaseOrderGridEntity
    {
        public List<VendorMainEntity> Vendors { get; set; } = new List<VendorMainEntity>();
        public List<ProductMainEntity> Products { get; set; } = new List<ProductMainEntity>();

    }


    public class PurchaseOrderParameterEntity : PagingSortingEntity
    {
        public int Id { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public int VendorId { get; set; } = 0;
        public double TotalQuantity { get; set; } = 0;
        public double TotalAmount { get; set; } = 0;
        public double TotalDiscount { get; set; } = 0;
        public double TotalTax { get; set; } = 0;
        public double TotalFinalAmount { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public string VendorName { get; set; } = string.Empty;


    }
}
