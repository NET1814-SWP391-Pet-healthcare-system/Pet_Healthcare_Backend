using Braintree;
using Microsoft.Extensions.Configuration;
using ServiceContracts;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BraintreeConfig : IBrainTreeConfig
    {
        private readonly IConfiguration _config;

        public BraintreeConfig(IConfiguration config)
        {
            _config = config;
        }


        public IBraintreeGateway CreateGateway()
        {
            var newGateway = new BraintreeGateway()
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = _config.GetValue<string>("BraintreeGateway:MerchantId"),
                PublicKey = _config.GetValue<string>("BraintreeGateway:PublicKey"),
                PrivateKey = _config.GetValue<string>("BraintreeGateway:PrivateKey")
            };
            if(newGateway == null)
            {
                newGateway = new BraintreeGateway()
                {
                    Environment = Braintree.Environment.SANDBOX,
                    MerchantId = "7hsbdwssq9dbhycd",
                    PublicKey = "63jk2fj3mc99kx3j",
                    PrivateKey = "da6fa41565db1282df581e7edfd71c9e"
                };
            }


            return newGateway;
        }

        public IBraintreeGateway GetGateway()
        {
            return CreateGateway();

        }
       
    }
}

