using Palindrome.Core.Abstract.AppService;
using Palindrome.Core.Abstract.Repository;
using Palindrome.Core.Validator;
using Palindrome.Core.ViewModel;
using Lib.Utils.Abstract;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Palindrome.Core.Model;

namespace Palindrome.AppService
{
    public class PalindromeService : IPalindromeService
    {
        private readonly IAppRepository _appRepository;
        private readonly IModelValidator _modelValidator;
        private readonly IObjectMapper _objectMapper;

        public PalindromeService(IAppRepository appRepository, IModelValidator modelValidator, IObjectMapper objectMapper)
        {
            _appRepository = appRepository;
            _modelValidator = modelValidator;
            _objectMapper = objectMapper;
        }

        public async Task<Guid> Add(PalindromeVm viewModel)
        {
            await _modelValidator.ValidateAsync<PalindromeVm, PalindromeVmValidator>(viewModel);

            var model = _objectMapper.Map<PalindromeVm, PalindromeModel>(viewModel);

            _appRepository.Create(model);
            await _appRepository.SaveAsync();

            return model.Id;
        }

        public async Task<IEnumerable<PalindromeVm>> GetAll()
        {
            var modelList = await _appRepository.GetAsync<PalindromeModel>();
            return _objectMapper.Map<IEnumerable<PalindromeModel>, IEnumerable<PalindromeVm>>(modelList);
        }
    }
}
