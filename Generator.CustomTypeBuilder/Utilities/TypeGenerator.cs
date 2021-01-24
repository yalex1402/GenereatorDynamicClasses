using System;
using System.Collections.Generic;
using System.Text;

namespace Generator.CustomTypeBuilder.Utilities
{
    public static class TypeGenerator
    {
        public static Type ConvertToSystemType(string objectType)
        {
            switch (objectType)
            {
                case "string":
                    return typeof(string);
                case "int":
                    return typeof(int);
                case "bool":
                    return typeof(bool);
                case "double":
                    return typeof(double);
                case "array":
                    return typeof(List<>);
                case "object":
                    return typeof(object);
                default:
                    return null;
            }
        }

        public static List<T> CreateList<T>(T type)
        {
            return new List<T>();
        }
    }
}
