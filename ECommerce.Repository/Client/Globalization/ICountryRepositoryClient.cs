using ECommerce.Entity.Client.Globalization;

namespace ECommerce.Repository.Client.Globalization
{
    public interface ICountryRepositoryClient
    {
        public Task<List<CountryMainClientEnttity>> SelectForLOV(CountryParemeterClientEntity countryParameterEntity);

    }
}
