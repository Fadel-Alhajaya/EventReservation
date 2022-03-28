using EventReservation.Core.DTO;
using EventReservation.Core.Service;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace EventReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        [HttpPost]
        [Route("refund")]
        public IActionResult refund(string id)
        {

            var options = new RefundCreateOptions
            {
                Charge = id,
            };
            var service = new RefundService();
            service.Create(options);

            return Ok("Done");
        }


        [HttpGet]
        [Route("GetCard")]

        public IActionResult GetCard()
        {
            
                var service = new CardService();
              var res=  service.Get("cus_LOVi5dSuMqYFHW", "card_1KhihNLa8FkurAU1GbbiWEFs");

            return  Ok(res);
        }
       
        [HttpGet]
        [Route("GetBalance")]
        public IActionResult GetBalance()
        {

            var service = new BalanceService();
            Balance balance = service.Get();
            var result = from data in balance.Available
                         select new
                         { 
                             balance= Convert.ToDecimal(data.Amount/ 100),
                             currency = data.Currency,
                             

                         };
             
            return Ok(balance);



        }
        [HttpGet]
        [Route("GetAllPayment")]

        public IActionResult GetAllPayment()
        {

            var options = new ChargeListOptions {Limit=3 };
            var service = new ChargeService();
            StripeList<Charge> charges = service.List(
              options);
            var servicee = new CustomerService();
            var res = from data in charges
                      select new
                      { 
                          id=data.Id,
                          amount = Convert.ToDecimal(data.Amount/100),
                          date=data.Created,
                          status = data.Status,
                          description = data.Description,
                          customerEmail = data.CustomerId !=null ? servicee.Get(data.CustomerId).Email : "nodata" ,
                          customerName = data.CustomerId != null ? servicee.Get(data.CustomerId).Name : "nodata",
                          receipt = data.ReceiptUrl,
                          
                      };
            return Ok(res);
        }
    
       

        [HttpPost]
        [Route("payment")]
        //[Authorize(Roles =("Admin"))]
        public async Task<IActionResult> Payment(EventResultToDto eventResultToDto)
        {
            
            var servicee = new CustomerService();
            var card=  servicee.Get(eventResultToDto.cardtokenid);

            //covert value to cent
            var ammount = eventResultToDto.totalprice * 100;
            var options = new ChargeCreateOptions
            {
                
                Amount =(long)ammount,
                Currency = "USD",
                Description = "Event-Reserve",
                Customer=card.Id,
                



                ReceiptEmail = eventResultToDto.Email


            };


            var service = new ChargeService();

            Charge charge = await service.CreateAsync(options);

            

         


            return Ok(charge);
      



        }

        [HttpPost]
        [Route("SendEmail")]
        public bool SendEmail( EventResultToDto eventResultToDto)
        {
            string MailText="";
            if (eventResultToDto.Status == "Rejected")
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\Email\\RejectEmail.html";
                StreamReader str = new StreamReader(FilePath);
               
                 MailText = str.ReadToEnd();
                MailText = MailText.Replace("{name}", eventResultToDto.Name);
                MailText = MailText.Replace("{price}", eventResultToDto.totalprice.ToString());
                str.Close();
            }
            else if(eventResultToDto.Status == "Accepted")
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\Email\\AccpetEmail.html";
                StreamReader str = new StreamReader(FilePath);
                MailText = str.ReadToEnd();
                MailText = MailText.Replace("{name}", eventResultToDto.Name);
                MailText = MailText.Replace("{price}", eventResultToDto.totalprice.ToString());
                MailText = MailText.Replace("{startdate}", eventResultToDto.Startdate.ToString());
                MailText = MailText.Replace("{enddate}", eventResultToDto.Enddate.ToString());
                str.Close();

            }
           


            MimeMessage myMessage = new MimeMessage();

            MailboxAddress mailFrom = new MailboxAddress("EventReservation", "saleh1aqrabawi@gmail.com");

            MailboxAddress mailTo = new MailboxAddress("User", eventResultToDto.Email);

            myMessage.From.Add(mailFrom);

            myMessage.To.Add(mailTo);

            myMessage.Subject = "Event reserve";

            BodyBuilder body = new BodyBuilder();
            body.HtmlBody = MailText;

            myMessage.Body = body.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("email", "password");
                client.Send(myMessage);
                client.Disconnect(true);
            }

            return true;
        }






    }
}
