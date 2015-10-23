using Surrogates.Aspects.ForAspNet4;
using Surrogates.Aspects.ForAspNet4.Container;
using Surrogates.Aspects.ForAspNet4.Exceptions;
using Surrogates.Aspects.ForAspNet4.Tactics;
using Surrogates.Expressions;
using Surrogates.Model.Entities;
using Surrogates.Tactics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Surrogates.Aspects.ForAspNet4.Expressions
{
    public class WebFormExpression : NewExpression
    {
        private WebFormContainer _container;
        private WebFormStrategies _strats;


        internal new WebFormStrategies Strategies 
        { 
            get { return _strats ?? (_strats = this.ThosePlans as WebFormStrategies); } 
        }

        public WebFormExpression(ModuleBuilder moduleBuilder, BaseContainer4Surrogacy container)
            : base(moduleBuilder, container)
        {
            _container = (WebFormContainer) container;
        }


        public ExpressionFactory<T> From<T>(string path = "", Access? access = null, Access? excludeAccess = null, Func<HttpContext, bool> before = null, Func<HttpContext, T, bool> after = null)
        {            
            if (string.IsNullOrEmpty(path))
            { throw new MissingRelativePathException(); }

            Name = path;

            var permissions = access.HasValue ?
                access.Value :
                _container.DefaultPermissions;

            if (excludeAccess.HasValue)
            {
                permissions &= ~excludeAccess.Value;
            }

            var a = after != null ? 
                (app, page) => after(app, (T) page) : 
                (Func<HttpContext, object, bool>) null;

            ThosePlans = ThosePlans ?? new WebFormStrategies(
                typeof(T), path, ModuleBuilder, permissions, before, a);
            
            return new ExpressionFactory<T>(
                Container,
                new Strategy(ThosePlans),
                ThosePlans);
        }

        public ExpressionFactory<T> From<T>(string path = "", Access? access = null, Access? excludeAccess = null, Action<HttpContext> before = null, Action<HttpContext, T> after = null)
        {
            var b = before != null ? 
                app => 
                {
                    before(app); 
                    return true; 
                } : 
                (Func<HttpContext, bool>) null;
            
            var a = after != null ? 
                (app, page) => 
                {
                    after(app, page); 
                    return true; 
                } : 
                (Func<HttpContext, T, bool>) null;                

            return From<T>(
                path, access, excludeAccess, b, a);
        }

        public override ExpressionFactory<T> From<T>(string path = "", Access? access = null, Access? excludeAccess = null)
        {
            return From<T>(path, access, excludeAccess, (Func<HttpContext, bool>)null, (Func<HttpContext, T, bool>) null);
        }
    }
}
