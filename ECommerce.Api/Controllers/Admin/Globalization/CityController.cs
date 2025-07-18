using CommonLibrary;
using ECommerce.Api.Common;
using ECommerce.Entity.Admin.Globalization;
using ECommerce.Repository.Admin.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Admin.Globalization
{
    [Route("admin/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        ICityRepository cityRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CityController(IWebHostEnvironment hostingEnvironment, ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        [Route("getLovValue", Name = "admin.city.getLovValue")]
        //[AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.IgnoreAuthorization)]

        public async Task<Response> GetForCityLOV(CityParemeterEntity cityParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await cityRepository.SelectForLOV(cityParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
    }
}
