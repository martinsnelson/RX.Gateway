using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RX.Gateway.Model.API.Request;
using RX.Gateway.Model.API.Response;

namespace RX.Gateway.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Gateway")]
    public class GatewayController : Controller
    {
        [HttpPost]
        [Route("MakeTransaction")]
        public TransactionResponse Process(TransactionRequest transactionRequest)
        {
            #region AntiFraud ClearSale

            #endregion

            TransactionResponse transactionResponse = new TransactionResponse(null, Model.API.EStatusResponse.Success, "");
            return transactionResponse;
        }
    }
}