using ECommerce.Business.Client.Search;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Repository.Client.Search
{
    public static class SearchProductFactory
    {
        public static ISearchProductRepository GetInstance(bool isMongoUsed, IConfiguration config)
        {
            if (isMongoUsed)
                return new SearchProductMongoBusiness(config);  
            else
                return new SearchProductSqlBusiness(config);   
        }
    }
}
