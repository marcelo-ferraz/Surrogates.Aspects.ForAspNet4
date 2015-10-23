using Surrogates.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Surrogates.Aspects.ForAspNet4.Container.Cache
{
    public class WebFormEntry : Entry
    {
        public string AppRelativeVirtualPath { get; set; }

        public string AppRelativeTemplateSourceDirectory { get; set; }

        public Func<HttpContext, bool> BeforeInstantiate { get; set; }

        public Func<HttpContext, object, bool> AfterInstantiate { get; set; }
    }
}
