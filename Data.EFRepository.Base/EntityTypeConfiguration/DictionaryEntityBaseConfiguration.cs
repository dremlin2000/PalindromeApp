using Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EFRepository.Base.EntityTypeConfiguration
{
    public abstract class DictionaryEntityBaseConfiguration<TEntity, TKey> : EntityBaseConfiguration<TEntity, TKey> where TEntity : DictionaryEntityBase<TKey>
    {
        protected DictionaryEntityBaseConfiguration(bool hasIdentityId = true) : base(hasIdentityId)
        {
        }

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Code).IsRequired().HasMaxLength(128);
            builder.HasIndex(x => x.Code).IsUnique().HasName("IX_Unique_Code");

            builder.Property(x => x.Description).IsRequired().HasMaxLength(400);
            builder.HasIndex(x => x.Description).IsUnique().HasName("IX_Description");
        }
    }
}
