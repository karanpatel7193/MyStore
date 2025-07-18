using DocumentDBClient;
using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Client.Search;
using ECommerce.Repository.Client.Search;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ECommerce.Business.Client.Search
{
    public class SearchProductMongoBusiness : CommonBusiness, ISearchProductRepository
    {
        private readonly IDocument<SearchProductMongoEntity> document;

        public SearchProductMongoBusiness(IConfiguration config) : base(config)
        {
            document = CreateDocumentInstance<SearchProductMongoEntity>("DefaultMongoDB", "product");
        }

        public async Task<List<SearchPropertyEntity>> SelectForSearch(SearchProductParameterEntity searchProductParameterEntity)
        {
            var propertyConditions = new List<Condition>();
            foreach (var item in searchProductParameterEntity.SearchProperties)
            {
                propertyConditions.Add(new Condition("Properties._v." + item.PropertyName, FieldType.String, CompareOperator.In, item.Values));
            }
            Filter finalParameter = new Filter(GroupOperator.AND, new List<Condition>
            {
                new Condition("CategoryId", FieldType.Number, CompareOperator.Equal, searchProductParameterEntity.CategoryId),
                new Condition(GroupOperator.AND, propertyConditions)
            });

            List<SearchProductMongoEntity> products = await document.GetAllAsync(finalParameter, new Sort("CategoryId", DocumentDBClient.SortDirection.Descending));
            List<SearchPropertyEntity> result = new List<SearchPropertyEntity>();
            for (int i = 0; i < products.Count; i++) 
            {
                result.Add(new SearchPropertyEntity { 
                    Id = products[i].Id,
                    Name = products[i].Name,
                    MediaThumbUrl = products[i].ThumbUrl,
                    Description = products[i].Description,
                    FinalSellPrice = products[i].FinalSellPrice
                });
            }
            return result;
        }

        public Task<SearchGridEntity> SelectForCriteriea(int CategoryId)
        {
            throw new NotImplementedException();
        }
    }
}
