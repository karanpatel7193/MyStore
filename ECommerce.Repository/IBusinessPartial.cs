using System.Data;

namespace ECommerce.Repository
{
    public interface IBusinessPartial<TEntity, TMainEntity, TGridEntity, TParameter, TPrimaryKey>
    {
        public TEntity MapData(IDataReader reader);

        public Task<TEntity> SelectForRecord(TPrimaryKey objPK);

        public Task<List<TMainEntity>> SelectForLOV(TParameter objParameter);

        public Task<TGridEntity> SelectForGrid(TParameter objParameter);

        public Task MapGridEntity(int resultSet, TGridEntity objGridEntity, IDataReader reader);

        public Task<TPrimaryKey> Insert(TEntity objEAL);

        public Task<TPrimaryKey> Update(TEntity objEAL);

        public Task Delete(TPrimaryKey objPK);
    }
}
