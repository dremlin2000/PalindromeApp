using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Palindrome.Core.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Palindrome.Web.Controllers
{
    public class BaseController: Controller
    {
        protected async Task<IActionResult> ExecuteAsync<T>(Func<Task<T>> func)
        {
            try
            {
                var result = await func.Invoke();
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new Error { Errors = ex.Errors.Select(x => new ErrorMessage { PropertyName = x.PropertyName, Message = x.ErrorMessage, ErrorCode = x.ErrorCode }).ToList()});
            }
        }
    }
}