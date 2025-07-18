using ECommerce.Entity.Master;
using ECommerce.Repository;
using System.Data;

namespace ECommerce.Entity.Admin.Master
{
    //public interface IProductRepositoroy
    public interface IProductRepositoroy : IBusiness<ProductEntity, ProductMainEntity, ProductAddEntity, ProductEditEntity, ProductGridEntity, ProductListEntity, ProductParameterEntity, int>
    {
        public Task<ProductPropertyGridEntity> SelectForPropertyGrid(ProductPropertyParameterEntity productPropertyParameterEntity);
        public Task InsertProperty(ProductPropertyParameterEntity productPropertyParameterEntity);
        public Task SyncAll();
        public Task Sync(ProductParameterEntity productParameterEntity);

    }


}
