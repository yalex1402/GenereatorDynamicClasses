using System;
using System.Collections.Generic;
using System.Text;

namespace Generator.CustomTypeBuilder.Utilities
{
    /// <summary>
    /// This class is configurated to generate any type
    /// </summary>
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
                case "BIGINT":
                    return typeof(long);
                case "INT":
                    return typeof(int);
                case string var when var.Contains("varchar"):
                    return typeof(string);
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
