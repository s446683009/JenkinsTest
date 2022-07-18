using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCR.Api.Controllers
{
    [ApiController]
    [Route("api/v1/order")]
    [Authorize]
    public class OrderController : Controller
    {
        [Route("getOrderId")]
        [HttpGet]
        public int GetOrderId() {
            return 2;
        }
    }
}
