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
    }

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            SurrogatesWebFormConfig.Map(
                m => m.From<_Default>("/Default.aspx").Replace.This(d => (Action)d.ThrowException).Using<Interceptor>("Harmless"));
        }
    }
}