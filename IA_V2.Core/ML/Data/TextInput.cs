using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Core.ML.Data
{
    /// Clase de entrada para el modelo de ML.NET - representa el texto a analizar
    public class TextInput
    {
        /// Texto que será analizado por el modelo de IA
        /// Ejm: El día está hermoso y soleado"
        [ColumnName("Texto")]
        public string Texto { get; set; } = string.Empty;
    }
}
