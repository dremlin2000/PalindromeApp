using Data.Repository.Base.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Palindrome.Web.Infrastructure.ActionFilters
{
    public class UnitOfWorkActionFilter : IActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkActionFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _unitOfWork.BeginTransaction();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null && context.ModelState.IsValid)
            {
                _unitOfWork.Commit();
            }
            else
            {
                _unitOfWork.Rollback();
            }
        }
    }
}
