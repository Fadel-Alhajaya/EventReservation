using EventReservation.Core.DTO;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Infra.Service
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportrepository;
        public ReportService(IReportRepository reportRepository)
        {
            _reportrepository = reportRepository;
        }
        public CountsDTO CountEventAccepted()
        {
            return _reportrepository.CountEventAccepted();
        }

        public CountsDTO CountEventPending()
        {
            return _reportrepository.CountEventPending();
        }

        public CountsDTO CountEventRejected()
        {
            return _reportrepository.CountEventRejected();
        }

        public CountsDTO CountUser()
        {
            return _reportrepository.CountUser();
        }

        public List<EventInfoDTO> EventAcceptedInterval(ReportIntervalDTO reportInterval)
        {
            return _reportrepository.EventAcceptedInterval(reportInterval);
        }
    }
}
