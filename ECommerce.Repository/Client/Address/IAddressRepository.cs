using ECommerce.Entity.Client.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Client.Address
{
    public interface IAddressRepository
    {
        public Task<AddressEntity> SelectForRecord(int Id);
        public Task<int> Insert(AddressEntity addressEntity);
        public Task<int> Update(AddressEntity addressEntity);
        public Task Delete(AddressParameterEntity addressParameterEntity);
        public Task<AddressGridEntity> SelectForGrid(AddressParameterEntity addressParameterEntity);

    }
}
