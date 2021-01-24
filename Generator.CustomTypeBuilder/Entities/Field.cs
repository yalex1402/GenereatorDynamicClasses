using System;
using System.Collections.Generic;
using System.Text;

namespace Generator.CustomTypeBuilder.Entities
{
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
