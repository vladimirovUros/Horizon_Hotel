using Application;
using DataAccess;
using Domain;
using Implementation.UseCases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Implementation.Logging.UseCases
{
    public class EfUseCaseLogger : EfUseCase, IUseCaseLogger
    {
        public EfUseCaseLogger(HotelHorizonContext context) : base(context)
        {
        }

        public void Log(UseCaseLog log)
        {
            Context.AuditLogs.Add(new AuditLog
            {
                UseCaseName = log.UseCaseName,
                Username = log.Username,
                UseCaseData = JsonConvert.SerializeObject(log.UseCaseData),
                ExecutedAt = DateTime.UtcNow
            });
            
            Context.SaveChanges();
        }
    }
}
