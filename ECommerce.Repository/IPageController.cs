using CommonLibrary;

namespace ECommerce.Repository
{
    public interface IPageController<TEntity, TParameter, TPrimaryKey> : IPageControllerPartial<TEntity, TParameter, TPrimaryKey>
    {
        Task<Response> GetForAdd(TParameter objParameter);

        Task<Response> GetForEdit(TParameter objParameter);

        Task<Response> GetForList(TParameter objParameter);
    }
}
