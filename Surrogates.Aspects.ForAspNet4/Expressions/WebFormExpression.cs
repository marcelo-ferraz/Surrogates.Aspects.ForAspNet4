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

        public override ExpressionFactory<T> From<T>(string path = "", Access? access = null, Access? excludeAccess = null)
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

            ThosePlans = new WebFormStrategies(
                typeof(T), path, ModuleBuilder, permissions);

            return new ExpressionFactory<T>(
                Container,
                new Strategy(ThosePlans),
                ThosePlans);
        }
    }
}
