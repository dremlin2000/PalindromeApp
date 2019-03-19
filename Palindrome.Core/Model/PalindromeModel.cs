using Data.Repository.Base;
using System;

namespace Palindrome.Core.Model
{
    public class PalindromeModel: EntityBase<Guid>
    {
        public string Phrase { get; set; }
    }
}
