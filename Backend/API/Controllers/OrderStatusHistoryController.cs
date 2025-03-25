using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Aplication.Interfaces;


namespace API.Controllers {

    [Authorize]
    [ApiController]
    [Route("api/osh")]
    public class OrderStatusHistoryController : ControllerBase {

        
        private readonly IOrderStatusHistoryService _orderStatusHistoryService;

        public OrderStatusHistoryController(IOrderStatusHistoryService orderStatusHistoryService) {
            _orderStatusHistoryService = orderStatusHistoryService;
        }        

    }
}