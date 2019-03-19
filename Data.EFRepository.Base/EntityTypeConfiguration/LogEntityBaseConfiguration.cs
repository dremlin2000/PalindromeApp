using Data.Repository.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EFRepository.Base.EntityTypeConfiguration
{
    public abstract class LogEntityBaseConfiguration<TEntity, TKey> : EntityBaseConfiguration<TEntity, TKey> where TEntity : LogEntityBase<TKey>
    {
        protected LogEntityBaseConfiguration(bool hasIdentityId = true) : base(hasIdentityId)
        {
        }

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(128);
            builder.Property(x => x.CreatedDate).IsRequired();
        }
    }
}
