using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Core.ML.Data
{
    /// Clase de salida del modelo de ML.NET - representa la predicción resultante
    public class TextPrediction
    {
        /// Categoría predicha por el modelo (ej: "Positivo", "Negativo")
        [ColumnName("PredictedLabel")]
        public string Categoria { get; set; } = string.Empty;

        /// Array de confianzas para cada categoría posible
        [ColumnName("Score")]
        public float[]? Confidencias { get; set; }

        /// Obtiene la confianza de la categoría predicha
        public float ConfianzaPrincipal => Confidencias?.Max() ?? 0f;
    }
}
