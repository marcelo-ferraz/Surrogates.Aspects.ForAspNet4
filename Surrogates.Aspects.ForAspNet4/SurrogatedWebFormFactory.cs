using Surrogates.Aspects.ForAspNet4.Container;
using Surrogates.Aspects.ForAspNet4.Container.Cache;
using Surrogates.Aspects.ForAspNet4.Exceptions;
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

namespace Surrogates.Aspects.ForAspNet4
{
    public class SurrogatedWebFormFactory : PageHandlerFactory
    {
        internal static WebFormContainer Container;

        static SurrogatedWebFormFactory()
        {
            Container = new WebFormContainer();
        }

        public override IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
        {
            if (!Container.Has(type: typeof(Page), key: virtualPath.ToLower()))
            { return base.GetHandler(context, requestType, virtualPath, path); }

            Action<WebFormEntry> firstCall = 
                (entry) =>
                {
                    var handler = base
                        .GetHandler(context, requestType, virtualPath, path) as Page;

                    if (handler == null)
                    { throw new NotAPageSoNotSupportedException(handler.GetType()); }

                    entry.AppRelativeVirtualPath = handler.AppRelativeVirtualPath;
                    entry.AppRelativeTemplateSourceDirectory = handler.AppRelativeTemplateSourceDirectory;
                };

            return Container.InvokeHandler(virtualPath,
                firstCall: firstCall,
                beforeInstatiate: (before) => before(context),
                afterInstatiate: (after, obj) => after(context, obj));            
        }

        public override void ReleaseHandler(IHttpHandler handler) { }
    }
}
