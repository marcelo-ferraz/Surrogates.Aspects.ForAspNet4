using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surrogates.Aspects.ForAspNet4.Exceptions
{
    public class NotInitializingPageException : Exception
    {
        public NotInitializingPageException()
            : base("The page needs to pass through some procedures in order to Surrogates fool asp.net. Please mind the FirstCall event.")
        { }
    }
}
