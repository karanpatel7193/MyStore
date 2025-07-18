using ECommerce.Entity.Client.Globalization;

namespace ECommerce.Repository.Client.Globalization
{
    public interface IStateRepositoryClient
    {
        public Task<List<StateMainClientEnttity>> SelectForLOV(StateParemeterClientEntity stateParameterEntity);

    }
}
