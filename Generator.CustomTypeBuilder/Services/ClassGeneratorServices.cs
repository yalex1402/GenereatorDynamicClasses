using Generator.CustomTypeBuilder.Entities;
using Generator.CustomTypeBuilder.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Generator.CustomTypeBuilder.Services
{
    public static class ClassGeneratorServices
    {
        public static List<ConfigStructure> SpecificConfig { get; set; }
        public static List<object> GeneratedClasses { get; set; }

        public static object CreateCustomClass(dynamic objects)
        {
            SpecificConfig = new List<ConfigStructure>();
            GeneratedClasses = new List<object>();
            var fields = new List<Field>();
            string fieldName;
            Type fieldType;
            object customMainClass = null;
            foreach (var element in objects)
            {
                var dicElements = (IDictionary<string, object>)element;
                if (dicElements.ContainsKey("elements"))
                {
                    fieldName = $"Channel{dicElements["channel"].ToString()}";
                    var config = CreateConfigStructureInstance
                        (dicElements["channel"].ToString(), fieldName, dicElements["type"].ToString());
                    fieldType = TypeGenerator.ConvertToSystemType(dicElements["type"].ToString());
                    object customClass = CreateCustomSecondaryClass(dicElements["elements"], fieldName, config);
                    if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var listTransformed = TypeGenerator.CreateList((dynamic)customClass);
                        fieldType = listTransformed.GetType();
                    }
                    else
                    {
                        fieldType = customClass.GetType();
                    }
                }
                else
                {
                    fieldName = dicElements["fieldName"].ToString();
                    fieldType = TypeGenerator.ConvertToSystemType(dicElements["type"].ToString());
                }
                fields.Add(new Field(fieldName, fieldType));
            }
            customMainClass = BuilderServices.CreateNewObject(fields, "MainClass");
            return customMainClass;
        }

        public static object CreateCustomSecondaryClass(dynamic objects, string className, ConfigStructure config)
        {
            var fields = new List<Field>();
            foreach (var item in objects)
            {
                string name = "";
                var type = TypeGenerator.ConvertToSystemType(item.tipo.ToString());
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>) || type == typeof(object))
                {
                    name = $"Channel{item.channel.ToString()}";
                    var configInside = CreateConfigStructureInstance(item.channel.ToString(),name, item.type.ToString());
                    var elements = item.elements;
                    object generatedClass = CreateCustomSecondaryClass(elements, name, configInside);
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var listTransformed = TypeGenerator.CreateList((dynamic)generatedClass);
                        type = listTransformed.GetType();
                    }
                    else 
                    {
                        type = generatedClass.GetType();
                    }
                        
                }
                else
                {
                    name = item.fieldName.ToString();
                    config.Properties.Add(new PropertyStructure
                    {
                        Name = name,
                        Position = item.position.ToString(),
                        Functions = item.functions
                    });
                }
                fields.Add(new Field(name, type));
            }
            var customClass = BuilderServices.CreateNewObject(fields, className);
            GeneratedClasses.Add(customClass);
            SpecificConfig.Add(config);
            return customClass;
        }

        private static ConfigStructure CreateConfigStructureInstance(string key, string objectName, string objectType)
        {
            return new ConfigStructure()
            {
                Key = key,
                ObjectName = objectName,
                ObjectType = objectType,
                Properties = new List<PropertyStructure>()
            };
        }
    }
}
