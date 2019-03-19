using FluentValidation;
using System;
using System.Threading.Tasks;

namespace Lib.Utils.Abstract
{
    public interface IModelValidator
    {
        Task ValidateAsync<TModel, TValidator>(TModel model)
            where TModel : class
            where TValidator : AbstractValidator<TModel>, new();
    }
}
