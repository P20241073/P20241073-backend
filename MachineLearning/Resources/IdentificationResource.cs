using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MachineLearning.Resources;
public class PredictionResult
{
    public List<Prediction> Output1 { get; set; }
}

public class Prediction
{
    public double PredictedPrice { get; set; }
}

public class IdentificationResource
{
    public PredictionResult Results { get; set; }
}