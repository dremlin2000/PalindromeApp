using AutoMapper;
using System;

namespace Palindrome.Core.Automapper.Profiles
{
    public class AppProfile: Profile
    {
        public AppProfile()
        {
            CreateMap<Model.PalindromeModel, ViewModel.PalindromeVm>().ReverseMap();
        }
    }
}
