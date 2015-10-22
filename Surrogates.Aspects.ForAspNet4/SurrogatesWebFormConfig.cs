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
        public static void Map(Action<NewExpression> map)
        {
            SurrogatedWebFormFactory.Container.Map(map);
        }
    }
}
