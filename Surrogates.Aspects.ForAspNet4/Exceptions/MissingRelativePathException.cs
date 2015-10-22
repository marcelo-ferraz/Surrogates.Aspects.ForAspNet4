using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surrogates.Aspects.ForAspNet4.Exceptions
{
    public class MissingRelativePathException : ArgumentException
    {
        public MissingRelativePathException()
            : base("The relative path to the page was not informed!")
        { }
    }
}
