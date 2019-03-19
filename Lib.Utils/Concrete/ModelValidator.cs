using FluentValidation;
using Lib.Utils.Abstract;
using System;
using System.Threading.Tasks;

namespace Lib.Utils.Concrete
{
    public class ModelValidator : IModelValidator
    {
        public async Task ValidateAsync<TModel, TValidator>(TModel model)
            where TModel : class
            where TValidator : AbstractValidator<TModel>, new()
        {
            var validator = new TValidator();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}