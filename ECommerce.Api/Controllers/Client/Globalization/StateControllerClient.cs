using CommonLibrary;
using ECommerce.Entity.Client.Globalization;
using ECommerce.Repository.Client.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Client.Globalization
{
    [Route("client/state")]
    [ApiController]
    public class StateControllerClient : ControllerBase
    {
        private readonly IStateRepositoryClient stateRepositoryClient;

        public StateControllerClient(IStateRepositoryClient stateRepositoryClient)
        {
            this.stateRepositoryClient = stateRepositoryClient;
        }

        [HttpPost]
        [Route("geLovValueState", Name = "client.state.geLovValue")]
        public async Task<Response> GetForStateLOV(StateParemeterClientEntity stateParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await stateRepositoryClient.SelectForLOV(stateParemeterEntity));
            }
            catch (Exception ex)
            {
                response = new Response("An error occurred while fetching states.", ex);
            }
            return response;
        }
    }
}
