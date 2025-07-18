using ECommerce.Entity.Admin.Globalization;

namespace ECommerce.Repository.Admin.Globalization
{
    public interface IStateRepository
    {
        public Task<List<StateMainEntity>> SelectForLOV(StateParemeterEntity stateParameterEntity);

    }
}
