using ECommerce.Entity.Admin.Globalization;

namespace ECommerce.Repository.Admin.Globalization
{
    public interface ICountryRepository
    {
        public Task<List<CountryMainEntity>> SelectForLOV(CountryParemeterEntity countryParameterEntity);

    }
}
