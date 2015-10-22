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
using System.Web.Compilation;

namespace Surrogates.Aspects.ForAspNet4.Tactics
{
    public class WebFormStrategies : Strategies
    {
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

        public WebFormStrategies(Type baseType, string path, ModuleBuilder moduleBuilder, Access permissions)
            : base(GetBaseType(path), null, moduleBuilder, permissions)
        {
            Path = path;
        }

        public override Entry Apply()
        {
            var entry = base.Apply();

            return new WebFormEntry
            {
                Type = entry.Type,
                Properties = entry.Properties,
                StateProperty = entry.StateProperty,
                ContainerProperty = entry.ContainerProperty
            };
        }
    }
}
