using Microsoft.EntityFrameworkCore;
using UserApi.Data;

namespace UserApi.Helper
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<UserDataContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                        DataInitializer.InitializeData(scope.ServiceProvider);
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }
            return host;
        }
    }
}
