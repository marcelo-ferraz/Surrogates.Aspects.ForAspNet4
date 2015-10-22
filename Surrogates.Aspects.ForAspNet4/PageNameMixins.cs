using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Surrogates.Aspects.ForAspNet4
{
    public static class PageNameMixins
    {
        private static Regex _classNameExp;

        static PageNameMixins()
        {
            _classNameExp = new Regex(
                @"(?<className>\w*)\.aspx$", RegexOptions.Compiled);
        }

        public static string ToAspxPath(this string value)
        {
            if (value == "_Default")
            {
                value = value.Substring(1);
            }

            return string.Concat("/", value, ".aspx");
        }

        public static string ToClassName(this string value)
        {
            //tratar com regex para que seja extraido um nome do caminho
            //pensar em como recuperar esse nome, de forma que seja simples
            var m = _classNameExp.Match(value);

            if (m.Success)
            { return m.Groups["className"].Value; }

            throw new NotSupportedException(
                string.Format("Could not create a class path based on : {0}", value));
        }
    }
}
