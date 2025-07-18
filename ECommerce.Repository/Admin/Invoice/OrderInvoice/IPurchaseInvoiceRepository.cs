using ECommerce.Entity.Admin.Order.OrderInvoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Admin.Invoice.OrderInvoice.Invoice
{
    public interface IPurchaseInvoiceRepository : IBusiness<PurchaseInvoiceEntity, PurchaseInvoiceMainEntity, PurchaseInvoiceAddEntity, PurchaseInvoiceEditEntity, PurchaseInvoiceGridEntity, PurchaseInvoiceListEntity, PurchaseInvoiceParameterEntity, int>
    {
    }
}
