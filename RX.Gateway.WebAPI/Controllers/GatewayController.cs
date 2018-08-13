using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RX.Gateway.Model.API.Request;
using RX.Gateway.Model.API.Request.AintFraud.ClearSale;
using RX.Gateway.Model.API.Response;
using RX.Gateway.Model.API.Response.AntiFraud.ClearSale;
using RX.Gateway.ServiceAgent.AntiFraud;

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

            ClearSaleServiceAgent clearSaleServiceAgent = new ClearSaleServiceAgent();
            Credentials login = new Credentials()
            {
                Apikey = "",
                ClientID = "",
                ClientSecret = ""
            };

            ResponseAuth responseAuth = clearSaleServiceAgent.Login(login);

            RequestSend requestSend = new RequestSend()
            {
                ApiKey = login.Apikey,
                LoginToken = responseAuth.Token.Value,
                Orders = null, //orders
                AnalysisLocation = "BR"
            };

            ResponseSend responseSend = clearSaleServiceAgent.Send(requestSend);
            clearSaleServiceAgent.Logout(login);

            #endregion

            TransactionResponse transactionResponse = new TransactionResponse(null, Model.API.EStatusResponse.Success, "");
            return transactionResponse;
        }
    }
}