using Surrogates.Aspects.ForAspNet4.Expressions;
using Surrogates.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surrogates.Aspects.ForAspNet4
{
    public static class SurrogatesWebFormConfig
    {
        public static void Map(Action<WebFormExpression> map)
        {
            SurrogatedWebFormFactory.Container.Map(map);
        }
    }
}
