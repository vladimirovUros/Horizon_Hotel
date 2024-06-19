using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases
{
    public abstract class EfUseCase
    {
        private readonly HotelHorizonContext _context;

        protected EfUseCase(HotelHorizonContext context)
        {
            _context = context;
        }
        protected HotelHorizonContext Context => _context;
    }
}
