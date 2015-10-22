using Surrogates.Expressions;
using Surrogates.Model.Entities;
using Surrogates.Tactics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;

namespace Surrogates.Aspects.ForAspNet4.Exceptions
{
    public class NotAPageSoNotSupportedException : Exception
    {
        public NotAPageSoNotSupportedException(Type type)
            : base(string.Format("The given type: '{0}' is not a webform page, therefore not supported for this Container", type)) { }
    }
}