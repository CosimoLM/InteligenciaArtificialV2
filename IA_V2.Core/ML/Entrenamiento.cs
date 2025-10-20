using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Core.ML
{
    public class Entrenamiento
    {
        public static void EntrenarYGuardarModelo()
        {
            var mlContext = new MLContext();

            // Datos de entrenamiento
            var trainingData = new List<TextTrainingData>
            {
                new TextTrainingData { Texto = "El día está hermoso", Label = "Positivo" },
                new TextTrainingData { Texto = "Odio este clima", Label = "Negativo" },
                new TextTrainingData { Texto = "Amo mi trabajo", Label = "Positivo" },
                new TextTrainingData { Texto = "Estoy cansado del tráfico", Label = "Negativo" }
            };

            var dataView = mlContext.Data.LoadFromEnumerable(trainingData);

            // Definir pipeline
            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(TextTrainingData.Texto))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(TextTrainingData.Label)))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            // Entrenar
            var model = pipeline.Fit(dataView);

            // Guardar modelo
            mlContext.Model.Save(model, dataView.Schema, "modeloIA.zip");
            Console.WriteLine("Modelo guardado correctamente en modeloIA.zip");
        }
    }

    public class TextTrainingData
    {
        public string Texto { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
    }
}
