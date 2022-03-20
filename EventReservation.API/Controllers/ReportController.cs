using Dapper;
using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Service;
using LMSTahaluf.Infra.Common;
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
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
         
        }



        ////public List<CntEventDTO> GetAllEvent()
        ////{
        ////    IEnumerable<CntEventDTO> result = _dbContext.Connection.Query<CntEventDTO>("Report_PACKAGE.IntervalReport", commandType: CommandType.StoredProcedure);

        ////    return result.ToList();
        ////}
        //[HttpGet("GeneratePDF")]
        //public async Task<ActionResult> GeneratePDF([FromBody] ReportIntervalDTO reportInterval)
        //{
        //    //var datest = .Date.ToString("dd-MM-yyyy hh:mm:ss");
        //    //var daten = reportInterval.endDate.Date.ToString("dd-MM-yyyy hh:mm:ss");
        //    var p = new DynamicParameters();
        //    p.Add("StartAt", reportInterval.startDate, dbType: DbType.Date, direction: ParameterDirection.Input);
        //    p.Add("END_DATE", reportInterval.endDate, dbType: DbType.Date, direction: ParameterDirection.Input);

        //    var document = new PdfDocument();
        //    IEnumerable<EventInfoDTO> result = _reportService.EventAcceptedInterval(reportInterval);
        //    // IEnumerable<Event> EventAccepted = _dbContext.Connection.Query<Event>("Event_PACKAGE.GetEventAccepted", commandType: CommandType.StoredProcedure);
        //    // IEnumerable<Event> EventRejeted = _dbContext.Connection.Query<Event>("Event_PACKAGE.GetEventRejected", commandType: CommandType.StoredProcedure);
        //    // IEnumerable<Event> EventPending = _dbContext.Connection.Query<Event>("Event_PACKAGE.GetEventPending", commandType: CommandType.StoredProcedure);
        //    //IEnumerable<User> NumUsers = _dbContext.Connection.Query<User>("USER_F_PACKAGE.GETALLUSERS", commandType: CommandType.StoredProcedure);
        //    var countuser = _reportService.CountUser();
        //    var cntaccepted = _reportService.CountEventAccepted();
        //    var cntrejected = _reportService.CountEventRejected();
        //    var cntpending = _reportService.CountEventPending();






        //    string htmlstring = "<h1 style='color:red;'text-decoration:underline;>Event Reservation Report </h1><br><br>";

        //    htmlstring += "The Number Accepted Events Is  :  " + cntaccepted.count + "<br><br>" +
        //    "The Number Rejected Events Is  :  " + cntrejected.count + "<br><br> The Number Pending Events Is  :  " + cntpending.count + "<br><br>"
        //    + "The Number Of Users Is  :  " + countuser.count + "<br><br><br><hr><br><br>" + "<h1> Annual Monthly Report :  </h1> <br><br> ";


        //    htmlstring += "<br><br>The Event Information Between " + "<strong>" + reportInterval.startDate + "</strong>" + "  And "
        //       + "<strong>" + reportInterval.endDate + "</strong>" + "    :   <br><br><br>" +
        //       "<hr><br>";

        //    htmlstring += "<table style='width:100%;padding: 4px;font-size:0.50vw; border-collapse: collapse;'> <thead> <tr> ";

        //    htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Event  Type</th>";
        //    htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Start Date</th>";
        //    htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>End Date</th>";
        //    htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Status</th>";
        //    htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Name Of Hall</th>";
        //    htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Use Of Hall</th>";
        //    htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Reservation Price</th>";

        //    htmlstring += "<th style='width:11%;  border: 1px solid #04AA6D;padding: 8px; background-color: #04AA6D;color:white;'>Rate</th>";

        //    htmlstring += "</tr></thead>  <tbody>";
        //    dynamic sumprice = 0;
        //    dynamic acceptinterval = 0;
        //    dynamic pendinginterval = 0;
        //    foreach (EventInfoDTO obj in result)
        //    {
        //        sumprice += obj.RESRVITIONPRICE;
        //        if (obj.status == "Accepted")
        //        {
        //            acceptinterval++;
        //        }
        //        if (obj.status == "Pending")
        //        {
        //            pendinginterval++;
        //        }

        //        htmlstring += "<tr><td style = 'width:11%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.eventtype + " </td> ";

        //        htmlstring += "<td style = 'width:11%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.startDate + " </td> ";
        //        htmlstring += "<td style = 'width:11%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.endDate + " </td> ";
        //        htmlstring += "<td style = 'width:11%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.status + " </td> ";
        //        htmlstring += "<td style = 'width:11%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.name + " </td> ";
        //        htmlstring += "<td style = 'width:11%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.usage + " </td> ";
        //        htmlstring += "<td style = 'width:11%;padding: 8px;text-align:left;border:1px solid #dddd;'> " + obj.RESRVITIONPRICE + " </td> ";

        //        htmlstring += "<td style = 'width:11%;text-align:left;padding: 8px;border:1px solid #dddd;'> " + obj.rate
        //        + " </td> </tr>";




        //    }

        //    htmlstring += "</tbody></table><br><br>";
        //    htmlstring += "The Accept Event In Interval Dates :  " + acceptinterval + "<br><br>" +
        //        "And The Pending Event In  Interval Dates  Is  : " + pendinginterval + "<br><br>" +
        //        "The Total Reservation Price In Interval Dates :    " + sumprice;
        //    PdfGenerator.AddPdfPages(document, htmlstring, PageSize.A4);

        //    Byte[] res = null;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        document.Save(ms);
        //        res = ms.ToArray();
        //    }

        //    return File(res, "application/pdf");
        //}
    }





}
