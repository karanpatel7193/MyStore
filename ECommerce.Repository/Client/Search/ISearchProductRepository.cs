using ECommerce.Entity.Client.Search;

namespace ECommerce.Repository.Client.Search
{
    public interface ISearchProductRepository
    {
        public Task<SearchGridEntity> SelectForCriteriea(int CategoryId);
        public Task<List<SearchPropertyEntity>> SelectForSearch(SearchProductParameterEntity searchProductParameterEntity);

    }
}
