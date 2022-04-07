using Microsoft.EntityFrameworkCore;
using UserApi.Model;

namespace UserApi.Data
{
    public class UserDataContext:DbContext
    {
        public DbSet<User> User { get; set; }
        public UserDataContext(DbContextOptions<UserDataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuider)
        {
            base.OnModelCreating(modelBuider);
        }
    }
}
