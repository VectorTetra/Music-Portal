using Microsoft.Extensions.DependencyInjection;
using Music_Portal.DAL.Interfaces;
using Music_Portal.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.BLL.Infrastructure
{
    public static class UnitOfWorkServiceExtensions
    {
        public static void AddUnitOfWorkService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        }
    }
}
