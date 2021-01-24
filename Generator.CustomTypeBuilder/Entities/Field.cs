using System;
using System.Collections.Generic;
using System.Text;

namespace Generator.CustomTypeBuilder.Entities
{
    /// <summary>
    /// Field is an important class, it's used to generate the properties in the new class.
    /// All fields that you put inside will be the properties of the class
    /// </summary>
    public class Field
    {
        public Field(string name, Type type)
        {
            this.FieldName = name;
            this.FieldType = type;
        }

        public string FieldName { get; set; }

        public System.Type FieldType { get; set; }
    }
}
