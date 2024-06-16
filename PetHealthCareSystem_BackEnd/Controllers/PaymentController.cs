using Microsoft.AspNetCore.Mvc;
using PetHealthCareSystem_BackEnd.Validations;
using ServiceContracts.DTO.AppointmentDTO;
using ServiceContracts;
using ServiceContracts.DTO.Result;
using ServiceContracts.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Entities;
using PetHealthCareSystem_BackEnd.Extensions;
using RepositoryContracts;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Entities.Enum;
using Services;
using Braintree;


namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        public IBrainTreeConfig _config;
        public IAppointmentService _appointmentService;
        public UserManager<User> _userService;

        public PaymentController(IBrainTreeConfig config, IAppointmentService appointmentService, UserManager<User> userService)
        {
            _config = config;
            _appointmentService = appointmentService;
            _userService = userService;
        }

        public static readonly TransactionStatus[] transactionSuccessStatuses =
            {
            TransactionStatus.AUTHORIZED,
            TransactionStatus.AUTHORIZING,
            TransactionStatus.SETTLED,
            TransactionStatus.SETTLING,
            TransactionStatus.SETTLEMENT_CONFIRMED,
            TransactionStatus.SETTLEMENT_PENDING,
            TransactionStatus.SUBMITTED_FOR_SETTLEMENT
        };

        [HttpGet, Route("GenerateToken")]
        public async Task<IActionResult> GenerateToken()
        {
            var gateway = _config.GetGateway();
            var clientToken = gateway.ClientToken.Generate();
            BusinessResult businessResult = new BusinessResult();

            if (clientToken == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "Token Generation Failed";
                return BadRequest(businessResult);
            }

            businessResult.Status = 200;
            businessResult.Data = clientToken;
            businessResult.Message = "Token Generated Successfully";

            return Ok(businessResult);

        }
        [HttpPost, Route("Checkout")]
        public async Task<IActionResult> Checkout(int appointmentid, string Nonce)
        {
            BusinessResult businessResult = new BusinessResult();
            string paymentStatus = string.Empty;
            var gateway = _config.GetGateway();

            var model = await _appointmentService.GetAppointmentByIdAsync(appointmentid);

            if(model == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "Appointment Not Found";
                return BadRequest(businessResult);
            }

            if(model.Status == AppointmentStatus.Done)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "Appointment Already Paid";
                return BadRequest(businessResult);
            }

            if(model.RefundAmount == null)
            {
                model.RefundAmount = 0;
            }

            var request = new TransactionRequest
            {
                Amount = (decimal)model.RefundAmount,
                PaymentMethodNonce = Nonce,

                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);
            if (result.IsSuccess())
            {
                paymentStatus = "Succeded";

                //Do Database Operations Here

                businessResult.Status = 200;
                businessResult.Data = result;
                businessResult.Message = "Payment Successfull";
                return Ok(businessResult);
            }
            else
            {
                string errorMessages = "";
                foreach (ValidationError error in result.Errors.DeepAll())
                {
                    errorMessages += "Error: " + (int)error.Code + " - " + error.Message + "\n";
                }

                paymentStatus = errorMessages;
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = errorMessages;
                return BadRequest(businessResult);
            }

           
        }

        //public async Task<IActionResult> Refund(int customerid, int appointmentid)
        //{
        //    BusinessResult businessResult = new BusinessResult();
        //    string paymentStatus = string.Empty;
        //    var gateway = _config.GetGateway();
        //    if(!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentid);
        //    if (appointment == null)
        //    {
        //        businessResult.Status = 404;
        //        businessResult.Data = null;
        //        businessResult.Message = "Appointment Not Found";
        //        return BadRequest(businessResult);
        //    }
        //    if(appointment.Status == AppointmentStatus.Done || appointment.Status == AppointStatus.Cancelled)
        //    {
        //        businessResult.Status = 404;
        //        businessResult.Data = null;
        //        businessResult.Message = "Appointment Already Paid";
        //        return BadRequest(businessResult);
        //    }


        //    var customerName =  _userService.GetUserName(this.User);
        //    var customer = await _userService.FindByNameAsync(customerName);
        //    if (customer == null)
        //    {
        //        businessResult.Status = 404;
        //        businessResult.Data = null;
        //        businessResult.Message = "Customer Not Found";
        //        return BadRequest(businessResult);
        //    }

        //    if(appointment.Customer!= customer)
        //    {
        //        businessResult.Status = 404;
        //        businessResult.Data = null;
        //        businessResult.Message = "Customer Not Owner Of This Transaction";
        //        return BadRequest(businessResult);
        //    }


            // var transactionId = _InvoiceService.GetTransactionIdByAppointmentId(appointmentid);
            // if (transactionId == null)
            // {
            //     businessResult.Status = 404;
            //     businessResult.Data = null;
            //     businessResult.Message = "Transaction Not Found";
            //     return BadRequest(businessResult);
            // }

            // Result<Transaction> result = gateway.Transaction.Refund(transactionId);
            // if (result.IsSuccess())
            // {
            //     paymentStatus = "Succeded";
            //     //Do Database Operations Here
            //}
            // else
            // {
            //     string errorMessages = "";
            //     foreach (ValidationError error in result.Errors.DeepAll())
            //     {
            //         errorMessages += "Error: " + (int)error.Code + " - " + error.Message + "\n";
            //     }
            //     paymentStatus = errorMessages;
            // }

        //}
    }
}
