using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Admin.Order.OrderInvoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Admin.Invoice.OrderInvoice
{
    public interface IPurchaseOrderRepository : IBusiness<PurchaseOrderEntity, PurchaseOrderMainEntity, PurchaseOrderAddEntity, PurchaseOrderEditEntity, PurchaseOrderGridEntity, PurchaseOrderListEntity, PurchaseOrderParameterEntity, int>
    {
    }
}
