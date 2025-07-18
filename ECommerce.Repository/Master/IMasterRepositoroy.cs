using ECommerce.Entity.Master;
using System.Data;

namespace ECommerce.Repository.Master
{
    public interface IMasterRepositoroy
    {
        public MasterEntity MapData(IDataReader reader);
        public  Task<int> Insert(MasterEntity masterEntity);
        public  Task<int> Update(MasterEntity masterEntity);
        public  Task Delete(int id);
        public  Task<List<MasterEntity>> SelectForGrid();
        public  Task<MasterEntity> SelectForRecord(short Id);

    }
}
