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
using RX.Gateway.ServiceAgent.Acquirier;
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

            #region Acquirer Stone

            var transaction = new GatewayApiClient.DataContracts.CreditCardTransaction()
            {
                AmountInCents = 10000,
                CreditCard = new GatewayApiClient.DataContracts.CreditCard()
                {
                    CreditCardBrand = GatewayApiClient.DataContracts.EnumTypes.CreditCardBrandEnum.Visa,
                    CreditCardNumber = "4111111111111111",
                    ExpMonth = 10,
                    ExpYear = 22,
                    HolderName = "LUKE SKYWALKER",
                    SecurityCode = "123"
                },
                InstallmentCount = 1
            };

            // Cria requisição.
            var createSaleRequest = new GatewayApiClient.DataContracts.CreateSaleRequest()
            {
                // Adiciona a transação na requisição.
                CreditCardTransactionCollection = new System.Collections.ObjectModel.Collection<GatewayApiClient.DataContracts.CreditCardTransaction>(new GatewayApiClient.DataContracts.CreditCardTransaction[] { transaction }),
                Order = new GatewayApiClient.DataContracts.Order()
                {
                    OrderReference = "NumeroDoPedido"
                }
            };

            // Coloque a sua MerchantKey aqui.
            Guid merchantKey = Guid.Parse("F2A1F485-CFD4-49F5-8862-0EBC438AE923");

            StoneServiceAgent stoneServiceAgent = new StoneServiceAgent();

            var salesResponse = stoneServiceAgent.MakeCreditCardTransaction(createSaleRequest);
            #endregion

            #region Acquirer Cielo
            #endregion

            TransactionResponse transactionResponse = new TransactionResponse(null, Model.API.EStatusResponse.Success, "");
            return transactionResponse;
        }
    }
}