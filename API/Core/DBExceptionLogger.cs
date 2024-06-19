using Application;
using DataAccess;
using Domain;
using Implementation.UseCases;

namespace API.Core
{
    public class DBExceptionLogger : EfUseCase, IExceptionLogger
    {
        public DBExceptionLogger(HotelHorizonContext context) 
            : base(context)
        {
        }

        public Guid Log(Exception ex, IApplicationActor actor)
        {
            Guid id = Guid.NewGuid();

            ErrorLog log = new()
            {
                Id = id,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Time = DateTime.UtcNow
            };

            Context.ErrorLogs.Add(log);

            Context.SaveChanges();

            return id;
        }
    }
}
