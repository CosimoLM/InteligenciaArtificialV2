using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Core.ML.Data
{
    /// DTO para respuestas de predicción completas
    public class PredictionResponse
    {
        /// ID único de la predicción en la base de datos
        public int PredictionId { get; set; }

        /// Texto que fue analizado
        public string TextoAnalizado { get; set; } = string.Empty;

        /// Categoría predicha
        public string Categoria { get; set; } = string.Empty;

        /// Probabilidad de la predicción (0-1)
        public double Probabilidad { get; set; }

        /// Confidencias para todas las categorías posibles
        public float[] Confidencias { get; set; } = Array.Empty<float>();

        /// Fecha y hora de la predicción
        public DateTime FechaPrediccion { get; set; }

        /// Tiempo que tomó realizar la predicción en milisegundos
        public long TiempoProcesamientoMs { get; set; }
    }
}
