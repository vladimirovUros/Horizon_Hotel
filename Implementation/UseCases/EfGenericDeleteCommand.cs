using Application.Exceptions;
using Application.UseCases;
using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases
{
    public abstract class EfGenericDeleteCommand<TEntity> : EfUseCase, ICommand<int>
        where TEntity : Entity
    {
        protected EfGenericDeleteCommand(HotelHorizonContext context) 
            : base(context)
        {
        }

        public abstract int Id { get;}

        public abstract string Name { get; }

        public void Execute(int data)
        {
            TEntity item = Context.Set<TEntity>().Find(data) ?? throw new EntityNotFoundException(typeof(TEntity).Name, data);
            if (item.IsActive == false)
                throw new EntityNotFoundException(nameof(TEntity), data);
            item.IsActive = false;
            Context.SaveChanges();
        }
    }
}
