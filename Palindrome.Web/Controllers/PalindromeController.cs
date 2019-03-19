using Microsoft.AspNetCore.Mvc;
using Palindrome.Core.Abstract.AppService;
using Palindrome.Core.ViewModel;
using Palindrome.Web.Infrastructure.ActionFilters;
using System.Threading.Tasks;

namespace Palindrome.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(UnitOfWorkActionFilter))]
    public class PalindromeController : BaseController
    {
        private readonly IPalindromeService _palindromeService;

        public PalindromeController(IPalindromeService palindromeService)
        {
            _palindromeService = palindromeService;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody]PalindromeVm viewModel)
        {
            return await ExecuteAsync(async () =>
            {
                var result = await _palindromeService.Add(viewModel);
                return result;
            });
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return await ExecuteAsync(async () =>
            {
                var result = await _palindromeService.GetAll();
                return result;
            });
        }

    }
}