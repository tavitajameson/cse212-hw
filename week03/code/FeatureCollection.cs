using System.Collections.Generic;

public class FeatureCollection
{
    public List<Feature> Features { get; set; } = new();
}

public class Feature
{
    public FeatureProperties Properties { get; set; } = new();
}

public class FeatureProperties
{
    public double? Mag { get; set; }
    public string? Place { get; set; }
}
