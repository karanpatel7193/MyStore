using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entity.Client.Cart
{
    public interface ICartRepository
    {
        public Task<int> Insert(CartEntity cartEntity);
        public Task<int> InsertBulk(CartMainEntity cartMainEntity);
        public Task<int> Update(CartEntity cartEntity);
        public Task Delete(CartParameterEntity cartParameterEntity);
        public Task<CartGridEntity> SelectForGrid(CartParameterEntity cartParameterEntity);
        public Task MapGridEntity(int resultSet, CartGridEntity CartGridEntity, IDataReader reader);

    }

}
