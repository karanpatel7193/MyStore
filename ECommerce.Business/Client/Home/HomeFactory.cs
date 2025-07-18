using ECommerce.Repository.Client.Home;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Business.Client.Home
{
    public static class HomeFactory
    {
        public static IHomeRepository GetInstance(bool isRedisHome, IConfiguration config)
        {
            if (isRedisHome)
                return new HomeRedisBusiness(config);
            else
                return new HomeSqlBusiness(config);
        }
    }
}
