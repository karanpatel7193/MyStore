using CommonLibrary;
using ECommerce.Api.Common;
using ECommerce.Entity.Admin.Globalization;
using ECommerce.Repository.Admin.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Admin.Globalization
{
    [Route("admin/state")]
    [ApiController]
    public class StateController : ControllerBase
    {
        IStateRepository stateRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public StateController(IWebHostEnvironment hostingEnvironment, IStateRepository stateRepository)
        {
            this.stateRepository = stateRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        [Route("getLovValue", Name = "admin.state.getLovValue")]
        [AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.IgnoreAuthorization)]

        public async Task<Response> GetForStateLOV(StateParemeterEntity stateParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await stateRepository.SelectForLOV(stateParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
    }
}
