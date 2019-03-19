using AutoMapper;
using Lib.Utils.Abstract;
using Lib.Utils.Concrete;
using Moq;
using NUnit.Framework;
using Palindrome.AppService;
using Palindrome.Core.Abstract.Repository;
using Palindrome.Core.Automapper.Profiles;
using Palindrome.Core.Model;
using Palindrome.Core.Validator;
using Palindrome.Core.ViewModel;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Palindrome.Test
{
    [TestFixture]
    public class PalindromeServiceTest
    {
        [Test]
        public async Task GivenAppService_WhenValidPhraseIsPassed_ThenViewModelValidated_And_PhraseAdded_And_IdRetrievedBack()
        {
            //Arrange
            var expectedId = Guid.NewGuid();
            var mockAppRepo = new Mock<IAppRepository>();
            PalindromeModel model = null;
            mockAppRepo.Setup(x => x.Create(It.IsAny<PalindromeModel>())).Callback((PalindromeModel x) => { model = x; }).Verifiable();
            mockAppRepo.Setup(x => x.SaveAsync()).Callback(() => { model.Id = expectedId; }).Returns(Task.FromResult<object>(null)).Verifiable();
            var mockModelValidator = new Mock<IModelValidator>();
            var mockPaymentValidator = new Mock<PalindromeVmValidator>();
            var autoMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AppProfile()));
            var objectMapper = new ObjectMapper(autoMapperConfig.CreateMapper());
            var palindromeService = new PalindromeService(mockAppRepo.Object, mockModelValidator.Object, objectMapper);

            //Act
            var receiptNum = await palindromeService.Add(new PalindromeVm());

            //Assert
            receiptNum.ShouldBe(expectedId);
            mockModelValidator.Verify(x => x.ValidateAsync<PalindromeVm, PalindromeVmValidator>(It.IsAny<PalindromeVm>()), Times.Once);
            mockAppRepo.Verify(x => x.SaveAsync(), Times.Once);
            mockAppRepo.Verify(x => x.Create(It.IsAny<PalindromeModel>()), Times.Once);
        }

        [Test]
        public async Task GivenAppService_WhenGetAllCalled_ThenAllPalindromesRetrievedBack()
        {
            //Arrange
            var expectedId = Guid.NewGuid();
            var mockAppRepo = new Mock<IAppRepository>();
            var modelList = new List<PalindromeModel> { new PalindromeModel { Id = Guid.NewGuid(), Phrase = "Was it a cat I saw" } };
            mockAppRepo.Setup(x => x.GetAsync<PalindromeModel>(It.IsAny<Expression<Func<PalindromeModel, bool>>>(),
                It.IsAny<Func<IQueryable<PalindromeModel>, IOrderedQueryable<PalindromeModel>>>(),
                It.IsAny<IEnumerable<Expression<Func<PalindromeModel, object>>>>(),
                It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<bool>()))
                .ReturnsAsync(() => modelList);
            var mockModelValidator = new Mock<IModelValidator>();
            var mockPaymentValidator = new Mock<PalindromeVmValidator>();
            var autoMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AppProfile()));
            var objectMapper = new ObjectMapper(autoMapperConfig.CreateMapper());
            var palindromeService = new PalindromeService(mockAppRepo.Object, mockModelValidator.Object, objectMapper);

            //Act
            var list = await palindromeService.GetAll();

            //Assert
            list.Count().ShouldBe(modelList.Count());
            mockAppRepo.Verify(x => x.GetAsync<PalindromeModel>(It.IsAny<Expression<Func<PalindromeModel, bool>>>(),
                It.IsAny<Func<IQueryable<PalindromeModel>, IOrderedQueryable<PalindromeModel>>>(),
                It.IsAny<IEnumerable<Expression<Func<PalindromeModel, object>>>>(),
                It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<bool>()), Times.Once);
        }
    }
}