using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRealBank.Contexts.Base
{
    public abstract class DbContextBase : DbContext
    {
        protected DbContextBase(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            EnforceRestrictedDeleteBehavior(modelBuilder);
            CreateIndexes(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }


        private static void EnforceRestrictedDeleteBehavior(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
             fk.DeleteBehavior = DeleteBehavior.Restrict;
            
        }


        private static void CreateIndexes(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.ClrType is not null)
                {
                    foreach (var attt in GetAttributes<IndexAttribute>(entity.ClrType))
                        modelBuilder.Entity(entity.ClrType).HasIndex(attt.Fields);


                    foreach (var att in GetAttributes<UniqueIndexAttribute>(entity.ClrType))
                        modelBuilder.Entity(entity.ClrType).HasIndex(att.Fields).IsUnique();
                }
            }
        }

        private static IEnumerable<T> GetAttributes<T>(Type type) where T : Attribute
            => type.GetCustomAttributes(typeof(T), false).OfType<T>();
       }
    }
