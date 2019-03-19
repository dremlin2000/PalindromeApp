using Microsoft.EntityFrameworkCore;
using Palindrome.Core.Model;
using System;
using System.Linq;
using System.Reflection;

namespace Palindrome.Repository
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            //Apply all existing EntityTypeConfigurations in executing assembly
            var configTypes = 
                Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition && 
                t.GetTypeInfo().ImplementedInterfaces.Any(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var configType in configTypes)
            {
                dynamic config = Activator.CreateInstance(configType);
                modelBuilder.ApplyConfiguration(config);
            }
        }
    }
}
