using Palindrome.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Palindrome.Core.Abstract.AppService
{
    public interface IPalindromeService
    {
        Task<IEnumerable<PalindromeVm>> GetAll();
        Task<Guid> Add(PalindromeVm paymentDetails);
    }
}
