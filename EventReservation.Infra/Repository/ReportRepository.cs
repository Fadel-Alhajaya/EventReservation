using Dapper;
using EventReservation.Core.Common;
using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace EventReservation.Infra.Repository
{
    public class ReportRepository : IReportRepository
    {

        private readonly IDbContext _dbContext;
        public ReportRepository(IDbContext _DbContext)
        {
            _dbContext = _DbContext;
        }

        public CountsDTO CountEventAccepted()
        {
           
            var result = _dbContext.Connection.QueryFirstOrDefault<CountsDTO>("Event_PACKAGE.GetCountEventAccepted",  commandType: CommandType.StoredProcedure);

            return result;
        }

        public CountsDTO CountEventPending()
        {
            var result = _dbContext.Connection.QueryFirstOrDefault<CountsDTO>("Event_PACKAGE.GetCountEventPending", commandType: CommandType.StoredProcedure);

            return result;
        }

        public CountsDTO CountEventRejected()
        {
            var result = _dbContext.Connection.QueryFirstOrDefault<CountsDTO>("Event_PACKAGE.GetCountEventRejected", commandType: CommandType.StoredProcedure);

            return result;
        }

        public CountsDTO CountUser()
        {
            var result = _dbContext.Connection.QueryFirstOrDefault<CountsDTO>("USER_F_PACKAGE.GetCountNormUser", commandType: CommandType.StoredProcedure);

            return result;
        }

        public List<EventInfoDTO> EventAcceptedInterval(ReportIntervalDTO reportInterval)
        {
            var p = new DynamicParameters();
            p.Add("StartAt", reportInterval.startDate, dbType: DbType.Date, direction: ParameterDirection.Input);
            p.Add("END_DATE", reportInterval.endDate, dbType: DbType.Date, direction: ParameterDirection.Input);

            var document = new PdfDocument();
            
            IEnumerable<EventInfoDTO> result = _dbContext.Connection.Query<EventInfoDTO>("Event_PACKAGE.SearchBetweenDates", p, commandType: CommandType.StoredProcedure);
            return result.ToList();
        
        }
    }
    }

