using System;
using System.Collections.Generic;
using System.Text;

namespace Generator.CustomTypeBuilder.Entities
{
    /// <summary>
    /// This model save important configurations of the classes generated
    /// </summary>
    public class ConfigStructure
    {
        public string Key { get; set; }

        public string ObjectName { get; set; }

        public string ObjectType { get; set; }

        public List<PropertyStructure> Properties { get; set; }
    }
}
