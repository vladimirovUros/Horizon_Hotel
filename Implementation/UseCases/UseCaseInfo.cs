using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases
{
    public static class UseCaseInfo
    {
        //public static IEnumerable<int> AllUseCases
        //{
        //    get
        //    {
        //var type = typeof(IUseCase);
        //var types = typeof(UseCaseInfo).Assembly.GetTypes()
        //    .Where(t => t.GetInterfaces().Contains(type) && t.Name.EndsWith("Command"));
        //var type = typeof(IUseCase);
        //var types = typeof(UseCaseInfo).Assembly.GetTypes()
        //    .Where(t => type.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
        //var useCases = types.Select(t => (IUseCase)Activator.CreateInstance(t));
        //return useCases.Select(uc => uc.Id);
        public static int MaxUseCaseId => 41;
    }
}

