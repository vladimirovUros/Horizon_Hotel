using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public interface ICommand<Tdata> : IUseCase
    {
        void Execute(Tdata data);
    }
}
