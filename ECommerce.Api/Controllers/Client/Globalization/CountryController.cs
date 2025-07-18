using CommonLibrary;
using ECommerce.Api.Common;
using ECommerce.Entity.Client.Globalization;
using ECommerce.Repository.Client.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Client.Globalization
{
    [Route("client/country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        ICountryRepositoryClient countryRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CountryController(IWebHostEnvironment hostingEnvironment, ICountryRepositoryClient countryRepository)
        {
            this.countryRepository = countryRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        [Route("geLovValue", Name = "client.country.getLovValue")]
        //[AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.IgnoreAuthorization)]

        public async Task<Response> GetForStateLOV(CountryParemeterClientEntity countryParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await countryRepository.SelectForLOV(countryParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

    }
}
