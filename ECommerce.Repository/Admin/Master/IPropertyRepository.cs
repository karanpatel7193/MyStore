using ECommerce.Entity.Admin.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Admin.Master
{
    public interface IPropertyRepository: IBusinessPartial<PropertyEntity, PropertyMainEntity, PropertyGridEntity, PropertyParameterEntity, int>
    {
        //public Task<PropertyEntity> SelectForRecord(int Id);
        //public Task<List<PropertyEntity>> SelectForLOV(PropertyParameterEntity PropertyParameterEntity);
        //public Task<PropertyGridEntity> SelectForGrid(PropertyParameterEntity PropertyParameterEntity);
        //public Task MapGridEntity(int resultSet, PropertyGridEntity PropertyGridEntity, IDataReader reader);
        //public Task<int> Insert(PropertyEntity PropertyEntity);
        //public Task<int> Update(PropertyEntity PropertyEntity);
        //public Task Delete(int Id);
        public List<PropertyEntity> SelectList();
    }

}
