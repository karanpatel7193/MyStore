using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Admin.Master.CategoryProperty;

namespace ECommerce.Repository.Admin.Master.CategoryProperty
{
    public interface ICategoryPropertyRepository : IBusiness<CategoryPropertyEntity, CategoryPropertyMainEntity, CategoryPropertyAddEntity, CategoryPropertyEditEntity, CategoryPropertyListEntity, CategoryPropertyGridEntity, CategoryPropertyParameterEntity, int>
    {
        Task<object> SelectForGrid(CategoryPropertyParameterEntity categoryPropertyParameterEntity);
        public Task<List<CategoryPropertyEntity>> SelectForCategoryProperty(CategoryPropertyParameterEntity categoryPropertyParameterEntity);

    }
}
