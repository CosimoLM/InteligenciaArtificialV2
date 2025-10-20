using IA_V2.Core.ML;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IA_V2.Core.Services
{
    public class ModeloIAService
    {
        private readonly MLContext _mlContext;
        private readonly string _modeloPath = "modeloIA.zip";
        private readonly PredictionEngine<TextInput, TextPrediction> _predEngine;

        public ModeloIAService()
        {
            _mlContext = new MLContext();

            // Si el modelo no existe, lo entrena automáticamente
            if (!File.Exists(_modeloPath))
            {
                Console.WriteLine("No se encontró el modelo. Entrenando uno nuevo...");
                EntrenarYGuardarModelo();
            }

            // Carga el modelo existente o recién creado
            using var stream = File.OpenRead(_modeloPath);
            var loadedModel = _mlContext.Model.Load(stream, out _);

            // Crea el motor de predicción
            _predEngine = _mlContext.Model.CreatePredictionEngine<TextInput, TextPrediction>(loadedModel);
        }

        // -----------------------------------------------------------
        // ENTRENAMIENTO AUTOMÁTICO
        // -----------------------------------------------------------
        private void EntrenarYGuardarModelo()
        {
            var trainingData = new List<TextTrainingData>
            {
                new TextTrainingData { Texto = "El día está hermoso", Label = "Positivo" },
                new TextTrainingData { Texto = "Odio este clima", Label = "Negativo" },
                new TextTrainingData { Texto = "Amo mi trabajo", Label = "Positivo" },
                new TextTrainingData { Texto = "Estoy cansado del tráfico", Label = "Negativo" }
            };

            var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            // Define el pipeline de procesamiento y clasificación
            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(TextTrainingData.Texto))
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label"))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            // Entrena el modelo
            var model = pipeline.Fit(dataView);

            // Guarda el modelo entrenado en archivo ZIP
            _mlContext.Model.Save(model, dataView.Schema, _modeloPath);
            Console.WriteLine("Modelo entrenado y guardado correctamente en modeloIA.zip");
        }

        // -----------------------------------------------------------
        // PREDICCIÓN
        // -----------------------------------------------------------
        public TextPrediction Predecir(string texto)
        {
            return _predEngine.Predict(new TextInput { Texto = texto });
        }
    }

    // -----------------------------------------------------------
    // CLASES AUXILIARES
    // -----------------------------------------------------------
    public class TextInput
    {
        [ColumnName("Texto")]
        public string Texto { get; set; } = string.Empty;
    }

    public class TextPrediction
    {
        [ColumnName("PredictedLabel")]
        public string Categoria { get; set; } = string.Empty;

        [ColumnName("Score")]
        public float[]? Confidencias { get; set; }
    }

    public class TextTrainingData
    {
        public string Texto { get; set; } 
        public string Label { get; set; }
    }
}
