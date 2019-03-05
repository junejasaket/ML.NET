using Microsoft.ML;
using System;

// CS0649 compiler warning is disabled because some fields are only 
// assigned to dynamically by ML.NET at runtime
#pragma warning disable CS0649

namespace myApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var mlContext = new MLContext();

            var trainingDataView = mlContext.Data.ReadFromTextFile<ML.NET.Models.Program.IrisData>(
                path: "iris-data.txt", 
                hasHeader: false, 
                separatorChar: ','
            );

            var pipeline = 
                mlContext.Transforms.Conversion.MapValueToKey("Label")
                .Append(mlContext.Transforms.Concatenate("Features", "SepalLength", "SepalWidth", "PetalLength", "PetalWidth"))
                .AppendCacheCheckpoint(mlContext)
                .Append(mlContext.MulticlassClassification.Trainers.StochasticDualCoordinateAscent(labelColumn: "Label", featureColumn: "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var model = pipeline.Fit(trainingDataView);

            // You can change these numbers to test different predictions
            var prediction = model.CreatePredictionEngine<ML.NET.Models.Program.IrisData, ML.NET.Models.Program.IrisPrediction>(mlContext).Predict(
                new ML.NET.Models.Program.IrisData()
                {
                    SepalLength = 3.3f,
                    SepalWidth = 1.6f,
                    PetalLength = 0.2f,
                    PetalWidth = 5.1f,
                });

            Console.WriteLine($"Predicted flower type is: {prediction.PredictedLabels}");

            Console.WriteLine("Press any key to exit....");
            Console.ReadLine();
        }
    }
}