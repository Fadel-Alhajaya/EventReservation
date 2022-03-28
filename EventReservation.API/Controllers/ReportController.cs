using Dapper;
using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace EventReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {


        private readonly IReportService _reportService;
        private readonly IUserService _userService;
        
        public ReportController(IReportService reportService, IUserService userService)
        {
            _reportService = reportService;
            _userService = userService;
        }


        

        //public List<CntEventDTO> GetAllEvent()
        //{
        //    IEnumerable<CntEventDTO> result = _dbContext.Connection.Query<CntEventDTO>("Report_PACKAGE.IntervalReport", commandType: CommandType.StoredProcedure);

        //    return result.ToList();
        //}
        [HttpGet("GeneratePDF")]
      public IActionResult GeneratePDF([FromHeader]ReportIntervalDTO reportInterval)
        {
            //var datest = .Date.ToString("dd-MM-yyyy hh:mm:ss");
            //var daten = reportInterval.endDate.Date.ToString("dd-MM-yyyy hh:mm:ss");
            var p = new DynamicParameters();
            p.Add("StartAt", reportInterval.startDate, dbType: DbType.Date, direction: ParameterDirection.Input);
            p.Add("END_DATE", reportInterval.endDate, dbType: DbType.Date, direction: ParameterDirection.Input);

            var document = new PdfDocument();
            IEnumerable<EventInfoDTO> result = _reportService.EventAcceptedInterval(reportInterval);
            // IEnumerable<Event> EventAccepted = _dbContext.Connection.Query<Event>("Event_PACKAGE.GetEventAccepted", commandType: CommandType.StoredProcedure);
            // IEnumerable<Event> EventRejeted = _dbContext.Connection.Query<Event>("Event_PACKAGE.GetEventRejected", commandType: CommandType.StoredProcedure);
            // IEnumerable<Event> EventPending = _dbContext.Connection.Query<Event>("Event_PACKAGE.GetEventPending", commandType: CommandType.StoredProcedure);
            //IEnumerable<User> NumUsers = _dbContext.Connection.Query<User>("USER_F_PACKAGE.GETALLUSERS", commandType: CommandType.StoredProcedure);
            var countuser = _reportService.CountUser();
            var cntaccepted = _reportService.CountEventAccepted();
            var cntrejected = _reportService.CountEventRejected();
            var cntpending = _reportService.CountEventPending();

            



            DateTime date = new DateTime();
            
            
             string htmlstring = " <h1 style='color:#04AA6D;text-shadow: 3px 3px 2px gray;display:inline;'> EventUp Reservation Report </h1> <img style='float:right;width:100px;height:100px;box-shadow:3px 7px 5px #dddd;' src='https://res.cloudinary.com/dczqrvtip/image/upload/v1647800253/logooo_uaicty.jpg'><br><br>";
            htmlstring += "The Number Accepted Events Is  :  " + cntaccepted.count + "<br><br>" +
            "The Number Rejected Events Is  :  " + cntrejected.count + "<br><br> The Number Pending Events Is  :  " + cntpending.count + "<br><br>"
            + "The Number Of Users Is  :  " + countuser.count + "<br><hr><br>" + "<h1> Annual Monthly Report :  </h1>  ";


            htmlstring += "<br><br>The Event Information Between " + "<strong>" + reportInterval.startDate + "</strong>" + "  And "
               + "<strong>" + reportInterval.endDate + "</strong>" + "    :   " +
               "";

            htmlstring += "<table style='width:100%;padding: 4px;font-size:0.50vw; border-collapse: collapse;'> <thead> <tr> ";

            htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Event  Type</th>";
            htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Start Date</th>";
            htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>End Date</th>";
            htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Status</th>";
            htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Name Of Hall</th>";
            htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Use Of Hall</th>";
            htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Reservation Price</th>";

            htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Rate</th>";

            htmlstring += "</tr></thead>  <tbody>";
            dynamic sumprice = 0;
            dynamic acceptinterval = 0;
            dynamic pendinginterval = 0;
              
                foreach (EventInfoDTO obj in result)
            {
                sumprice += obj.reservationprice;
                if (obj.status == "Accepted")
                {
                    acceptinterval++;
                }
                if (obj.status == "Pending")
                {
                    pendinginterval++;
                }

                htmlstring += "<tr><td style = 'width:11%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.eventtype + " </td> ";

                htmlstring += "<td style = 'width:11%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.Startdate + " </td> ";
                htmlstring += "<td style = 'width:11%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.Enddate + " </td> ";
                htmlstring += "<td style = 'width:11%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.status + " </td> ";
                htmlstring += "<td style = 'width:11%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.name + " </td> ";
                htmlstring += "<td style = 'width:11%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.usage + " </td> ";
                htmlstring += "<td style = 'width:11%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.reservationprice + " </td> ";

                htmlstring += "<td style = 'width:11%;text-align:left;padding: 8px;border:1px solid #dddd;'> " + obj.rate
                + " </td> </tr>";




            }
            
                        htmlstring += "</tbody></table><br><br>";
            htmlstring += "The Accept Event In Interval Dates :  " + acceptinterval + "<br><br>" +
                "And The Pending Event In  Interval Dates  Is  : " + pendinginterval + "<br><br>" +
                "The Total Reservation Price In Interval Dates :    " + sumprice;
            PdfGenerator.AddPdfPages(document, htmlstring, PageSize.A4);

            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                res = ms.ToArray();
            }

            return File(res, "application/pdf");
        }
    




    [HttpGet("GenerateUserReport")]
    public IActionResult GenerateUserReport()
    {
        var document = new PdfDocument();
            dynamic cntuser = 0;
            var allUser = _userService.GetAllUsers().Result;
            var user = from data in allUser
                       where data.Position == "NormalUser"
                       select new UsertoResultDto
                       {
                           Userid = data.Userid,
                           Firstname = data.Firstname,
                           Lastname = data.Lastname,

                           Email = data.Email,
                           Birthdate = data.Birthdate,
                           Position = data.Position,
                          

                       };



          string htmlstring = " <h1 style='color:#04AA6D;text-shadow: 3px 3px 2px gray;display:inline;'> EventUp Reservation Report </h1> <img style='float:right;width:100px;height:100px;box-shadow:3px 7px 5px #dddd;' src='https://res.cloudinary.com/dczqrvtip/image/upload/v1647800253/logooo_uaicty.jpg'><br><br>";
            htmlstring += "<h3>All Users Signed In Event Up Website</h3>";
            
         htmlstring += "<table style='width:100%;padding: 4px; border-collapse: collapse;'> <thead> <tr> ";

        htmlstring += "<th style='width:20%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>FirstName</th>";
        htmlstring += "<th style='width:20%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Last Name</th>";
            htmlstring += "<th style='width:20%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Birth Date</th>";

            htmlstring += "<th style='width:20%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Email</th>";
        htmlstring += "<th style='width:20%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Position</th>";
     
        htmlstring += "</tr></thead>  <tbody>";
       
        foreach (UsertoResultDto obj in user)
        {


            htmlstring += "<tr><td style = 'width:20%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.Firstname + " </td> ";

            htmlstring += "<td style = 'width:20%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.Lastname + " </td> ";
            htmlstring += "<td style = 'width:20%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.Birthdate + " </td> ";
                htmlstring += "<td style = 'width:20%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.Email + " </td> ";

            htmlstring += "<td style = 'width:20%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.Position + " </td> ";
            htmlstring += "<td style = 'width:20%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.Username + " </td> "

            + " </tr>";




        }

        htmlstring += "</tbody></table><br><br>";

            htmlstring += "<h4>The Number of user  : " + user.Count() + "</h4>";



            PdfGenerator.AddPdfPages(document, htmlstring, PageSize.A4);

        Byte[] res = null;
        using (MemoryStream ms = new MemoryStream())
        {
            document.Save(ms);
            res = ms.ToArray();
        }

        return File(res, "application/pdf");
    }
}





}
