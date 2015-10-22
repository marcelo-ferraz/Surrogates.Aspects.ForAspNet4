using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace Surrogates.Aspects.ForAspNet4.Test
{
    public class CustomPageHandlerFactory : PageHandlerFactory
    {
        private static FieldInfo _isInheritedInstanceField;

        static CustomPageHandlerFactory() 
        {
            _isInheritedInstanceField = typeof(PageHandlerFactory)
                .GetField("_isInheritedInstance", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public CustomPageHandlerFactory() 
        {
            _isInheritedInstanceField.SetValue(this, false);
        }

        public override IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
        {
            //var stack = new StackTrace();
            
            //var fs = stack.GetFrames().Select(f => f.ToString()).ToArray();

            var page = base.GetHandler(context, requestType, virtualPath, path);

            ((_Default)page).Throw = false;
            
            return page;
        }
    }
}