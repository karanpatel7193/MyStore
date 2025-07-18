using ECommerce.Entity.Client.Globalization;

namespace ECommerce.Repository.Client.Globalization
{
    public interface ICityRepositoryClient
    {
        public Task<List<CityMainClientEnttity>> SelectForLOV(CityParemeterClientEntity CityParameterEntity);

    }
}
