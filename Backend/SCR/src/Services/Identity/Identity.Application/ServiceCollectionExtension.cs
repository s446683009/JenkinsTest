using Identity.Application.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application
{
    public static class ServiceCollectionExtension
    {

      
        public static void AddQueries(this IServiceCollection services, Type[] types)
        {
            foreach (var @interface in types.Where(
                t => t.IsInterface && t.GetInterfaces().Any(i => i == typeof(IQuery))))
            {
                var implement = types.FirstOrDefault(t => t.GetInterfaces().Any(i => i == @interface));
                if (implement == null) continue;
                services.AddScoped(@interface, implement);
            }
        }

    }
}
