using CommonLibrary;
using ECommerce.Entity.Client.Globalization;
using ECommerce.Repository.Client.Globalization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Controllers.Client.Globalization
{
    [Route("client/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepositoryClient _cityRepository;

        public CityController(ICityRepositoryClient cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpPost]
        [Route("getLovValueCity", Name = "client.city.getLovValue")]
        public async Task<Response> GetForCityLOV([FromBody] CityParemeterClientEntity cityParameterEntity)
        {
            try
            {
                var result = await _cityRepository.SelectForLOV(cityParameterEntity);
                return new Response(result);
            }
            catch (Exception ex)
            {
                return new Response(await ex.WriteLogFileAsync(), ex);
            }
        }
    }
}
