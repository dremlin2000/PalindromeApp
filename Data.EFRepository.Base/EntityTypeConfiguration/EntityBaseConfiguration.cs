using Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EFRepository.Base.EntityTypeConfiguration
{
    public abstract class EntityBaseConfiguration<TEntity, TKey>: IEntityTypeConfiguration<TEntity> where TEntity: EntityBase<TKey>
    {
        private readonly bool _hasIdentityId;
        protected EntityBaseConfiguration(bool hasIdentityId = true)
        {
            _hasIdentityId = hasIdentityId;
        }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            if (_hasIdentityId) builder.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }
}
