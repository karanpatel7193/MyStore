using ECommerce.Entity.Admin.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Admin.Master
{
    public interface ICustomerRepository : IBusinessPartial<CustomerEntity, CustomerMainEntity, CustomerGridEntity, CustomerParameterEntity, int>
    {
    }
}
