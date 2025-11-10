using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Core.ML.Data
{
    /// Clase para datos de entrenamiento del modelo
    public class TextTrainingData
    {
        /// Texto de ejemplo para entrenamiento
        public string Texto { get; set; } = string.Empty;

        /// Etiqueta o categoría esperada para el texto
        public string Label { get; set; } = string.Empty;
    }
}
