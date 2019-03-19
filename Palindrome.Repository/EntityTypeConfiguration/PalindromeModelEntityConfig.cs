using Data.EFRepository.Base.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Palindrome.Core.Model;
using System;

namespace Palindrome.Repository.EntityTypeConfiguration
{
    public class PalindromeModelEntityConfig: EntityBaseConfiguration<PalindromeModel, Guid>
    {
        public override void Configure(EntityTypeBuilder<PalindromeModel> builder)
        {
            base.Configure(builder);

            builder.ToTable(nameof(PalindromeModel));
        }
    }
}
