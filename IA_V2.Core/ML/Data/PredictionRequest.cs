using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Core.ML.Data
{
    /// DTO para solicitudes de predicción desde los controladores
    public class PredictionRequest
    {
        /// Texto a analizar
        /// Ej:Me encanta este producto, es excelente
        public string Texto { get; set; } = string.Empty;

        /// ID opcional del usuario que realiza la predicción
        public int? UserId { get; set; }

        /// ID opcional del texto asociado (si ya existe en BD)
        public int? TextId { get; set; }
    }
}
