using FluentValidation;
using Palindrome.Core.ViewModel;
using System.Linq;

namespace Palindrome.Core.Validator
{
    public class PalindromeVmValidator : AbstractValidator<PalindromeVm>
    {
        public PalindromeVmValidator()
        {
            RuleFor(x => x.Phrase)
              .NotEmpty()
              .WithMessage(x => $"[{nameof(x.Phrase)}]: value is required")
              .Must((x) =>
              {
                  return IsPalindrome(x);
              })
              .WithMessage(x => $"[{nameof(x.Phrase)}]: value is not palindrome");
        }

        private bool IsPalindrome(string value)
        {
            int min = 0;
            int max = value.Length - 1;
            while (true)
            {
                if (min > max)
                {
                    return true;
                }
                char a = value[min];
                char b = value[max];

                while (!char.IsLetterOrDigit(a))
                {
                    min++;
                    if (min > max)
                    {
                        return true;
                    }
                    a = value[min];
                }

                while (!char.IsLetterOrDigit(b))
                {
                    max--;
                    if (min > max)
                    {
                        return true;
                    }
                    b = value[max];
                }

                if (char.ToLower(a) != char.ToLower(b))
                {
                    return false;
                }
                min++;
                max--;
            }
        }
    }
}
