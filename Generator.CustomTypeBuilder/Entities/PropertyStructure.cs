using System;
using System.Collections.Generic;
using System.Text;

namespace Generator.CustomTypeBuilder.Entities
{
    /// <summary>
    /// This model describes values to configure every field read
    /// </summary>
    public class PropertyStructure
    {
        public string Name { get; set; }

        public string Position { get; set; }

        public dynamic Functions { get; set; }
    }
}
