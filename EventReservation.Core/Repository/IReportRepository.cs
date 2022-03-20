using EventReservation.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventReservation.Core.Repository
{
    public interface IReportRepository
    {
        List<EventInfoDTO> EventAcceptedInterval(ReportIntervalDTO reportInterval);
        CountsDTO CountUser();
        CountsDTO CountEventAccepted();
        CountsDTO CountEventRejected();
        CountsDTO CountEventPending();

    }
}
