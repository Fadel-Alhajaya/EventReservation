using EventReservation.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.Service
{
    public interface IReportService
    {
        List<EventInfoDTO> EventAcceptedInterval(ReportIntervalDTO reportInterval);
        CountsDTO CountUser();
        CountsDTO CountEventAccepted();
        CountsDTO CountEventRejected();
        CountsDTO CountEventPending();



    }
}
