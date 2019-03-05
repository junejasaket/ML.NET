using Microsoft.ML.Data;

namespace SentimentAnalysis.Models
{
    public class SentimentData
    {
        [Column(ordinal: "0", name: "Label")]
        public float Sentiment;

        [Column(ordinal: "1")]
        public string SentimentText;
    }
}