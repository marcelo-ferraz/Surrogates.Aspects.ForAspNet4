using Surrogates.Aspects.ForAspNet4.Container.Cache;
using Surrogates.Model.Entities;
using Surrogates.Tactics;
using Surrogates.Utilities.WhizzoDev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Compilation;

namespace Surrogates.Aspects.ForAspNet4.Tactics
{
    public class WebFormStrategies : Strategies
    {
        internal Func<HttpContext, bool> BeforeInstantiate { get; set; }
        internal Func<HttpContext, object, bool> AfterInstantiate { get; set; }

        private static MethodInfo _typeGetter;

        static WebFormStrategies()
        {
            _typeGetter = Type.GetType(
                "System.Web.Util.ITypedWebObjectFactory, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")
                .GetProperty("InstantiatedType", BindingFlags.Instance | BindingFlags.Public)
                .GetGetMethod();
        }

        private static Type GetBaseType(string path)
        {
            var factory =
                BuildManager.GetObjectFactory(path, false);

            if (factory == null)
            {
                throw new EntryPointNotFoundException(string.Concat(
                    "Could not resolve the provided path: '", path.ToClassName(), "' !"));
            }

            return (Type)_typeGetter.Invoke(factory, null);
        }

        public string Path { get; set; }

        public WebFormStrategies(Type baseType, string path, ModuleBuilder moduleBuilder, Access permissions, Func<HttpContext, bool> beforeInstantiate = null, Func<HttpContext, object, bool> afterInstantiate = null)
            : base(GetBaseType(path), null, moduleBuilder, permissions)
        {
            Path = path;
            BeforeInstantiate = beforeInstantiate;
            AfterInstantiate = afterInstantiate;
        }

        public override Entry Apply()
        {
            var entry = base.Apply();
            
            return new WebFormEntry
            {
                Type = entry.Type,
                Properties = entry.Properties,
                BeforeInstantiate = BeforeInstantiate,
                AfterInstantiate = AfterInstantiate,
                StateProperty = entry.StateProperty,
                ContainerProperty = entry.ContainerProperty
            };
        }
    }
}
