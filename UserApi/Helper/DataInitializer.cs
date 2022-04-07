using Newtonsoft.Json;
using UserApi.Data;
using UserApi.Model;

namespace UserApi.Helper
{
    public static class DataInitializer
    {
        public static void InitializeData(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<UserDataContext>();

            
            if (!context.Set<User>().Any())
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "users.json");
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    throw new ArgumentException($"Value of {filePath} must be supplied to Seed Users");
                }
                if (!File.Exists(filePath))
                {
                    throw new ArgumentException($"The file { filePath} does not exist");
                }
                var userSeedData = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(filePath));
                context.Set<User>().AddRange(userSeedData);
                context.SaveChanges();
            }

        }
    }
}
