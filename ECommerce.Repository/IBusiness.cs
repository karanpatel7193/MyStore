using System.Data;

namespace ECommerce.Repository
{
    public interface IBusiness<TEntity, TMainEntity, TAddEntity, TEditEntity, TGridEntity, TListEntity, TParameter, TPrimaryKey> : IBusinessPartial<TEntity, TMainEntity, TGridEntity, TParameter, TPrimaryKey>
    {
        public Task<TAddEntity> SelectForAdd(TParameter objParameter);
        public Task MapAddEntity(int resultSet, TAddEntity objAddEntity, IDataReader reader);

        public Task<TEditEntity> SelectForEdit(TParameter objParameter);
        public Task MapEditEntity(int resultSet, TEditEntity objEditEntity, IDataReader reader);

        public Task<TListEntity> SelectForList(TParameter objParameter);
        public Task MapListEntity(int resultSet, TListEntity objListEntity, IDataReader reader);
    }
}
