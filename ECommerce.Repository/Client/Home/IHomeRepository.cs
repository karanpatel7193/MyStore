using ECommerce.Entity.Client.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Client.Home
{
    public interface IHomeRepository
    {
        public Task<List<CategoryEntity>> SelectCategoryList(bool isRedisHome);
        public Task<List<BlockEntity>> SelectForBlock(bool isRedisHome);
        public Task<List<BlockEntity>> SelectForSllider(bool isRedisHome);
        public Task<BlockGridEntity> SelectForBlockList(bool isRedisHome);
    }
}
