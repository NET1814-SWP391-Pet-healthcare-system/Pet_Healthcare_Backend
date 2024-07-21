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
using Braintree.Test;
using Braintree;
using ServiceContracts.DTO.PaymentDTO;


namespace PetHealthCareSystem_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        public IBrainTreeConfig _config;
        public IAppointmentService _appointmentService;
        public UserManager<User> _userService;
        public ITransactionService _transactionService;
        public IHospitalizationService _hospitalizationService;

        public PaymentController(IBrainTreeConfig config, IAppointmentService appointmentService, UserManager<User> userService, ITransactionService transactionService, IHospitalizationService hospitalizationService)
        {
            _config = config;
            _appointmentService = appointmentService;
            _userService = userService;
            _transactionService = transactionService;
            _hospitalizationService = hospitalizationService;
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
        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout(CheckoutRequest checkoutRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            string paymentStatus = string.Empty;
            var gateway = _config.GetGateway();

            var model = await _appointmentService.GetAppointmentByIdAsync(checkoutRequest.AppointmentId);

            if(model == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "Appointment Not Found";
                return BadRequest(businessResult);
            }

            if (model.Status == AppointmentStatus.Done || model.PaymentStatus == PaymentStatus.Paid)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "Appointment Already Paid";
                return BadRequest(businessResult);
            }

            var PayAmount = model.Service.Cost;

            var request = new TransactionRequest
            {
                Amount = Convert.ToDecimal(PayAmount),
                PaymentMethodNonce = checkoutRequest.Nonce,

                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true,
                    
                }
            };

            Result<Braintree.Transaction> result = gateway.Transaction.Sale(request);
            if (result.IsSuccess())
            {
                paymentStatus = "Succeded";
                var customerName = _userService.GetUserName(this.User);
                if (string.IsNullOrEmpty(customerName))
                {
                    // Handle the case when customerName is null or empty
                    businessResult.Status = 400;
                    businessResult.Data = null;
                    businessResult.Message = "Invalid user information";
                    return BadRequest(businessResult);
                }
                var customer = await _userService.FindByNameAsync(customerName);
                //Add Transaction to database
                Entities.Transaction transaction = new Entities.Transaction
                {
                    TransactionId = result.Target.Id,
                    AppointmentId = checkoutRequest.AppointmentId,
                    CustomerId = customer.Id,
                    Amount = (double)PayAmount,
                    Date = DateTime.Now
                };
                
                var trans = await _transactionService.AddAsync(transaction);
                if(trans == null)
                {
                    result = await gateway.Transaction.RefundAsync(transaction.TransactionId);
                  
                    businessResult.Status = 404;
                    businessResult.Data = null;
                    businessResult.Message = "Transaction Failed";
                    return BadRequest(businessResult);
                }

                var appointment = await _appointmentService.UpdateAppointmentPaymentStatus(checkoutRequest.AppointmentId, PaymentStatus.Paid);
                businessResult.Status = 200;
                businessResult.Data = appointment;
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
        [HttpPost, Route("Refund")]
        public async Task<IActionResult> Refund(RefundRequest refundRequest)
        {
            BusinessResult businessResult = new BusinessResult();
            string paymentStatus = string.Empty;
            var gateway = _config.GetGateway();

            #region Validation
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appointment = await _appointmentService.GetAppointmentByIdAsync(refundRequest.AppointmentId);
            if (appointment == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "Appointment Not Found";
                return BadRequest(businessResult);
            }
            if (appointment.Status == AppointmentStatus.Done || appointment.Status == AppointmentStatus.Cancelled)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "Appointment Already Done";
                return BadRequest(businessResult);
            }
            if(appointment.PaymentStatus == PaymentStatus.Refunded)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "Appointment Already Refunded";
                return BadRequest(businessResult);
            }



            var customerName = _userService.GetUserName(this.User);
            var customer = await _userService.FindByNameAsync(customerName);
            if (customer == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "Customer Not Found";
                return BadRequest(businessResult);
            }

            if (appointment.Customer != customer)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "Customer Not Owner Of This Transaction";
                return BadRequest(businessResult);
            }
            #endregion

            var transactionList = await _transactionService.GetByUserIdAsync(customer.Id);
            var transactionId = transactionList.FirstOrDefault(t => t.AppointmentId == refundRequest.AppointmentId);
            

            if (transactionId == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "Transaction Not Found";
                return BadRequest(businessResult);
            }

            Result<Braintree.Transaction> result = gateway.Transaction.Refund(transactionId.TransactionId,(decimal)transactionId.Amount*CalculateRefundPercentage(appointment.Date));
            if (result.IsSuccess())
            {
                paymentStatus = "Succeded";
                var transaction = await _transactionService.GetByIdAsync(transactionId.TransactionId);
                transaction.Amount = 0;
                await _transactionService.UpdateAsync(transaction);
                //Do Database Operations Here
                await _appointmentService.UpdateAppointmentPaymentStatus(refundRequest.AppointmentId, PaymentStatus.Refunded);
                await _appointmentService.UpdateAppointmentStatus(refundRequest.AppointmentId, AppointmentStatus.Cancelled);
                businessResult.Status = 200;
                businessResult.Data = appointment;
                businessResult.Message = "Payment Refunded Successfully";
                return Ok(result);
            }
            else
            {
                string errorMessages = "";
                foreach (ValidationError error in result.Errors.DeepAll())
                {
                    errorMessages += "Error: " + (int)error.Code + " - " + error.Message + "\n";
                }
                paymentStatus = errorMessages;
            }
            businessResult.Status = 404;
            businessResult.Data = null;
            businessResult.Message = paymentStatus;
            return BadRequest(businessResult);
        }

        private decimal CalculateRefundPercentage(DateOnly appointmentDate)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var daysDifference = appointmentDate.DayNumber - today.DayNumber;

            if (daysDifference >= 7)
            {
                return 1.0m; // 100% refund
            }
            else if (daysDifference >= 3 && daysDifference <= 6)
            {
                return 0.75m; // 75% refund
            }
            else
            {
                return 0.0m; // No refund 
            }
        }

        [HttpGet, Route("Revenue")]
        public async Task<IActionResult> Revenue()
        {
            BusinessResult businessResult = new BusinessResult();
            var transactions = await _transactionService.GetAllAsync();
            if (transactions == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "No Transactions Found";
                return BadRequest(businessResult);
            }
            double totalRevenue = 0;
            double dailyRevenue = 0;
            double weeklyRevenue = 0;
            double monthlyRevenue = 0;
            double yearlyRevenue = 0;
            foreach (var transaction in transactions)
            {
                if(transaction.Date.Date == DateTime.Now.Date)
                {
                    dailyRevenue += transaction.Amount;
                }
                if(transaction.Date.Date >= DateTime.Now.AddDays(-7).Date)
                {
                     weeklyRevenue+= transaction.Amount;
                }
                if (transaction.Date.Date >= DateTime.Now.AddMonths(-1).Date)
                {
                    monthlyRevenue += transaction.Amount;
                }
                if (transaction.Date.Date >= DateTime.Now.AddYears(-1).Date)
                {
                    yearlyRevenue += transaction.Amount;
                }
                totalRevenue += transaction.Amount;
            }
            businessResult.Status = 200;
            businessResult.Data = new
            {
                TotalRevenue = totalRevenue,
                DailyRevenue = dailyRevenue,
                WeeklyRevenue = weeklyRevenue,
                MonthlyRevenue = monthlyRevenue,
                YearlyRevenue = yearlyRevenue
            };
            businessResult.Message = "Revenue Calculated Successfully";
            return Ok(businessResult);

        }

        [HttpGet, Route("GetTransactions")]
        public async Task<IActionResult> GetTransactions()
        {
            BusinessResult businessResult = new BusinessResult();
            var transactions = await _transactionService.GetAllAsync();
            if (transactions == null)
            {
                businessResult.Status = 404;
                businessResult.Data = null;
                businessResult.Message = "No Transactions Found";
                return BadRequest(businessResult);
            }
            businessResult.Status = 200;
            businessResult.Data = transactions.Select(t => t.ToCashOutDto());
            businessResult.Message = "Transactions Found Successfully";
            return Ok(businessResult);
        }

        [HttpPost, Route("CashOut")]
        public async Task<IActionResult> CashOut(CashRequest cashRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customer = await _userService.FindByIdAsync(cashRequest.customerId);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }
            var hospitalization = await _hospitalizationService.GetHospitalizationById(cashRequest.hospitalizationId);
            if(hospitalization == null)
            {
                return NotFound("Hospitalization not found");
            }
            if(hospitalization.PaymentStatus==PaymentStatus.Paid)
            {
                return BadRequest("Hospitalization already paid");
            }
            var transaction = new Entities.Transaction
            {
                CustomerId = customer.Id,
                Amount = (int) hospitalization.TotalCost,
                HospitalizationId = cashRequest.hospitalizationId,
                Date = DateTime.Now

            };
            var result = await _transactionService.AddAsync(transaction);
            if (result == null)
            {
                return BadRequest("Transaction failed");
            }
            hospitalization.PaymentStatus = PaymentStatus.Paid;
            await _hospitalizationService.UpdateHospitalization(hospitalization);
            return Ok(result.ToCashOutDto());

        }

        [HttpPost, Route("CashOutForAppointment")]
        public async Task<IActionResult> CashoutForAppointment(CashoutAppointRequest cashRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customer = await _userService.FindByIdAsync(cashRequest.customerId);

            if (customer == null)
            {
                return NotFound("Customer not found");
            }
            var appointment = await _appointmentService.GetAppointmentByIdAsync(cashRequest.appointmentId);
            if (appointment != null && (appointment.PaymentStatus == PaymentStatus.Pending || appointment.PaymentStatus == null))
            {
                appointment.PaymentStatus = PaymentStatus.Paid;
                await _appointmentService.UpdateAppointmentPaymentStatus(cashRequest.appointmentId, PaymentStatus.Paid);
            }   
            else
            {
                return BadRequest("Appointment already paid");
            }
            var transaction = new Entities.Transaction
            {
                CustomerId = customer.Id,
                Amount = cashRequest.ammount,
                Date = DateTime.Now,
                AppointmentId = cashRequest.appointmentId
            };
            var result = await _transactionService.AddAsync(transaction);
            if (result == null)
            {
                return BadRequest("Transaction failed");
            }
            return Ok(result.ToCashOutDto());

        }
    }
}
