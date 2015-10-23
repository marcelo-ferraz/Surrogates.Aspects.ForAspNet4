using Surrogates.Aspects.ForAspNet4;
using Surrogates.Aspects.ForAspNet4.Container.Cache;
using Surrogates.Aspects.ForAspNet4.Exceptions;
using Surrogates.Aspects.ForAspNet4.Expressions;
using Surrogates.Expressions;
using Surrogates.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace Surrogates.Aspects.ForAspNet4.Container
{
    public class WebFormContainer : SurrogatesContainer
    {
        internal new Access DefaultPermissions { get; set; }

        public WebFormContainer() 
        {
            DefaultPermissions =
               Access.Container | Access.StateBag | Access.AnyMethod | Access.AnyField | Access.AnyBaseProperty | Access.AnyNewProperty | Access.Instance;
        }

        public WebFormContainer Map(Action<WebFormExpression> map)
        {
            InternalMap(e => map((WebFormExpression)e));
            return this;
        }

        protected override void InternalMap(Action<NewExpression> mapping)
        {
            var expression =
                new WebFormExpression(ModuleBuilder, this);

            mapping(expression);

            var entry =
                expression.Strategies.Apply();
                        
            Cache.Add(expression.Strategies.Path.ToLower(), entry);            
        }

        public override bool Has(Type type = null, string key = null)
        {
            if (string.IsNullOrEmpty(key))
            { key = string.Concat(type.Name, "Proxy"); }

            return Cache.ContainsKey(key);
        }

        public Page InvokeHandler(string path, Action<WebFormEntry> firstCall, Func<Func<HttpContext, bool>, bool> beforeInstatiate, Func<Func<HttpContext, object, bool>, object, bool> afterInstatiate, Action<dynamic> stateBag = null)
        {
            var entry = 
                Cache[path.ToLower()] as WebFormEntry;

            if (string.IsNullOrEmpty(entry.AppRelativeVirtualPath))
            {
                if (firstCall == null)
                { throw new NotInitializingPageException(); }

                firstCall(entry);
            }

            if (beforeInstatiate != null && 
                entry.BeforeInstantiate != null && 
                !beforeInstatiate(entry.BeforeInstantiate)) 
            { return null; }

            var page = Activator
                .CreateInstance(entry.Type, null) as Page;

            if (entry.StateProperty != null && stateBag != null)
            { stateBag(((IContainsStateBag)page).StateBag); }

            if (entry.ContainerProperty != null)
            { entry.ContainerProperty.SetValue(page, this, null); }

            foreach (var prop in entry.Properties)
            {
                if (prop.Value != null)
                {
                    prop.Info.SetValue(page, prop.Value, null);
                }
            }

            page.AppRelativeVirtualPath =
               entry.AppRelativeVirtualPath;

            page.AppRelativeTemplateSourceDirectory =
                entry.AppRelativeTemplateSourceDirectory;

            if (afterInstatiate != null &&
                entry.AfterInstantiate != null &&
                !afterInstatiate(entry.AfterInstantiate, page))
            { return null; }


            return page;
        }
    }
}
