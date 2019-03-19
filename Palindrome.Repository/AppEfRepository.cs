using System;
using System.Threading.Tasks;
using Data.EFRepository.Base;
using Palindrome.Core.Abstract.Repository;


namespace Palindrome.Repository
{
    public partial class AppEfRepository : EntityFrameworkRepository<AppDbContext>, IAppRepository
    {
        public AppEfRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
