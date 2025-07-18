using ECommerce.Entity.Client.Home;
using ECommerce.Repository.Client.Home;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Client.Home
{
    public class HomeRedisBusiness : CommonBusiness, IHomeRepository
    {
        public HomeRedisBusiness(IConfiguration config) : base(config)
        {
        }

        public Task<List<CategoryEntity>> SelectCategoryList(bool isRedisHome)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlockEntity>> SelectForBlock(bool isRedisHome)
        {
            throw new NotImplementedException();
        }

        public Task<BlockGridEntity> SelectForBlockList(bool isRedisHome)
        {
            throw new NotImplementedException();
        }

        public Task<List<BlockEntity>> SelectForSllider(bool isRedisHome)
        {
            throw new NotImplementedException();
        }
    }
}
