using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ECommerce.Entity.Admin.Master.CategoryEntity;

namespace ECommerce.Repository.Admin.Master
{
    public interface ICategoryRepository : IBusiness<CategoryEntity, CategoryMainEntity, CategoryAddEntity, CategoryEditEntity, CategoryGridEntity, CategoryListEntity, CategoryParemeterEntity, int>
    {
        //public Task<CategoryEntity> SelectForRecord(int Id);
        //public Task<List<CategoryEntity>> SelectForLOV();
        //public Task<CategoryGridEntity> SelectForGrid(CategoryParameterEntity categoryParameterEntity);
        //public Task MapGridEntity(int resultSet, CategoryGridEntity CategoryGridEntity, IDataReader reader);
        //public Task<int> Insert(CategoryEntity CategoryEntity);
        //public Task<int> Update(CategoryEntity CategoryEntity);
        //public Task Delete(int Id);
        public Task<List<CategoryEntity>> SelectParent();
        public Task<List<CategoryEntity>> SelectChild(CategoryParemeterEntity categoryParemeterEntity);

        public  Task<List<CategoryPropertyValueEntity>> SelectCategoryPropertyValue();
    }

}

