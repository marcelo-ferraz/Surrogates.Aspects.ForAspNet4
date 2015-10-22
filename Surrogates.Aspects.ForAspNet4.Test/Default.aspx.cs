using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Surrogates.Aspects.ForAspNet4.Test
{
    public partial class _Default : System.Web.UI.Page
    {
        public bool Throw { get; set; }

        public _Default()
        {
            Throw = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ThrowException();
        }

        public virtual void ThrowException() 
        {
            if (Throw)
            {
                throw new Exception("Non sense error!");
            }            
        }
    }
}