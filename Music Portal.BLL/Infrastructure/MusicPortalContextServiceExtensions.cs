using Microsoft.Extensions.DependencyInjection;
using Music_Portal.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace Music_Portal.BLL.Infrastructure
{
    public static class MusicPortalContextServiceExtensions
    {
        public static void AddMusicPortalContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<MusicPortalContext>(options => options.UseSqlServer(connection));
        }
    }
}
