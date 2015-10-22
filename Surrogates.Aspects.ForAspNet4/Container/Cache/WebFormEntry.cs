using Surrogates.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surrogates.Aspects.ForAspNet4.Container.Cache
{
    public class WebFormEntry : Entry
    {
        public string AppRelativeVirtualPath { get; set; }

        public string AppRelativeTemplateSourceDirectory { get; set; }
    }
}
