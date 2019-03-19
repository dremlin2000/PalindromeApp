using System;
using System.Collections.Generic;
using System.Text;

namespace Palindrome.Core.ViewModel
{
    public class LogMessage
    {
        public string Message { get; set; }
        public Severity Severity { get; set; }
    }

    public enum Severity
    {
        Debug = 1,
        Trace = 2,
        Info = 3,
        Warning = 4,
        Critical = 5,
        Error = 6
    }
}