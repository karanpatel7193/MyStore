using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Admin.Vendor;
using System;
using System.Collections.Generic;

namespace ECommerce.Entity.Admin.Order.OrderInvoice
{
    public class PurchaseInvoiceMainEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
    }

    public class PurchaseInvoiceItemEntity
    {
        public int Id { get; set; } = 0;
        public int PurchaseInvoiceId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
        public double Price { get; set; } = 0;
        public double Amount { get; set; } = 0;
        public double DiscountPercentage { get; set; } = 0;
        public double DiscountedAmount { get; set; } = 0;
        public double Tax { get; set; } = 0;
        public double FinalAmount { get; set; } = 0;
        public DateTime? ExpiryDate { get; set; }
        public bool IsExpiry { get; set; } = false;

    }

    public class PurchaseInvoiceEntity : PurchaseInvoiceMainEntity
    {
        public int InvoiceNumber { get; set; } = 0;
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

        public List<PurchaseInvoiceItemEntity> PurchaseInvoiceItems { get; set; } = new List<PurchaseInvoiceItemEntity>();
    }

    public class PurchaseInvoiceGridEntity
    {
        public List<PurchaseInvoiceEntity> Invoices { get; set; } = new List<PurchaseInvoiceEntity>();
        public int TotalRecords { get; set; }
    }

    public class PurchaseInvoiceAddEntity
    {
        public List<VendorMainEntity> Vendors { get; set; } = new List<VendorMainEntity>();
        public List<ProductMainEntity> Products { get; set; } = new List<ProductMainEntity>();
    }

    public class PurchaseInvoiceEditEntity : PurchaseInvoiceAddEntity
    {
        public PurchaseInvoiceEntity PurchaseInvoice { get; set; } = new PurchaseInvoiceEntity();
    }

    public class PurchaseInvoiceListEntity : PurchaseInvoiceGridEntity
    {
        public List<VendorMainEntity> Vendors { get; set; } = new List<VendorMainEntity>();
        public List<ProductMainEntity> Products { get; set; } = new List<ProductMainEntity>();
    }

    public class PurchaseInvoiceParameterEntity : PagingSortingEntity
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
        public string ProductName { get; set; } = string.Empty;
    }
}
