using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoilerPlate.Service.Contract;
using BoilerPlate.Utils;
using BoilerPlate.Bootstrapper;
using BoilerPlate.Web.Entities;

namespace BoilerPlate.Web.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IIdentityService _identityService;
        protected override Market MarketConfiguration => base.MarketConfiguration;

        public OrderController(UserManager<ApplicationUser> userManager,
            ApplicationSettings applicationSettings,
            ICampaignService campaignService,
            IIdentityService identityService,
            IDistributedCache cache,
            IEncryptionService encryptionService,
            ILogger<OrderController> logger) : base(userManager, campaignService, logger, applicationSettings, cache, encryptionService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        [APIUserAuthorize]
        [Route("api/orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<List<OrderResponseModel>> GetFoundOrders()
        {
            return await GetFoundOrders(new ListRequestModel() { PageItemCount = 100, PageNumber = 1 });
        }

        [HttpGet]
        [APIUserAuthorize]
        [Route("api/orders/{pageNumber}/{PageItemCount}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<List<OrderResponseModel>> GetFoundOrders([FromRoute] ListRequestModel listModel)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [APIUserAuthorize]
        [Route("api/orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<OrderResponseModel> Create([FromBody] CreateOrderRequestModel createOrderRequestModel)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [APIUserAuthorize]
        [Route("api/orders/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task Update(string orderId, [FromBody] UpdateOrderRequestModel updateOrderRequestModel)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [APIUserAuthorize]
        [Route("api/orders/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task DeleteByOrderNumber(string orderId)
        {
            throw new NotImplementedException();
        }
    }
}
