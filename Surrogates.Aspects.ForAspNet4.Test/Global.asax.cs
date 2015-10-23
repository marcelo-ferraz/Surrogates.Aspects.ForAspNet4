using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace Surrogates.Aspects.ForAspNet4.Test
{
    public class Interceptor 
    {
        public void Harmless(object s_holder) 
        { 
        }

        public void Intercept(HttpContext context)
        {
            context.Response.Write("This page had passed through an interceptor");
        }
    }

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var @int = new Interceptor();

            SurrogatesWebFormConfig.Map(
                m => m.From<_Default>("/Default.aspx", before: c => c.Response.Write("This page had passed through an interceptor"), after: (c, d) => d.Throw = false));
        }
    }
}