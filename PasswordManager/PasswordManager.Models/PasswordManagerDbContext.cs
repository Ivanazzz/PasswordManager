using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Models.Entities;
using System.Reflection;

namespace PasswordManager.Models
{
    public class PasswordManagerDbContext : DbContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public PasswordManagerDbContext(DbContextOptions<PasswordManagerDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Info> Infos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ApplyConfiguration(builder);
        }

        protected void ApplyConfiguration(ModelBuilder builder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Any(gi =>
                    gi.IsGenericType
                    && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                builder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}
