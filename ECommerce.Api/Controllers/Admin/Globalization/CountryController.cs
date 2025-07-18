using CommonLibrary;
using ECommerce.Api.Common;
using ECommerce.Entity.Admin.Globalization;
using ECommerce.Repository.Admin.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Admin.Globalization
{
    [Route("admin/country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        ICountryRepository countryRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CountryController(IWebHostEnvironment hostingEnvironment, ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        [Route("geLovValue", Name = "admin.country.getLovValue")]
        [AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.IgnoreAuthorization)]

        public async Task<Response> GetForStateLOV(CountryParemeterEntity countryParameterEntity)
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
