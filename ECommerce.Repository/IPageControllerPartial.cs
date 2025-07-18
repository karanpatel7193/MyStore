using CommonLibrary;

namespace ECommerce.Repository
{
    public interface IPageControllerPartial<TEntity, TParameter, TPrimaryKey>
    {
        Task<Response> GetForRecord(TPrimaryKey objPK);

        Task<Response> GetForLOV(TParameter objParameter);

        Task<Response> GetForGrid(TParameter objParameter);

        Task<Response> Insert(TEntity objEntity);

        Task<Response> Update(TEntity objEntity);

        Task<Response> Delete(TPrimaryKey objPK);
    }
}
