using Microsoft.ML.Data;

// CS0649 compiler warning is disabled because some fields are only 
// assigned to dynamically by ML.NET at runtime
#pragma warning disable CS0649

namespace ML.NET.Models
{
    partial class Program
    {
        // IrisPrediction is the result returned from prediction operations
        public class IrisPrediction
        {
            [ColumnName("PredictedLabel")]
            public string PredictedLabels;
        }
    }
}