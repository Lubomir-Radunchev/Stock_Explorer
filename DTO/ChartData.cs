public class ChartData
{
    public string Id { get; set; }
    public string Type { get; set; }
    public Dictionary<string, ChartDataPoint> attributes { get; set; }
}

public class ChartAttributes
{
}



public class ChartDataPoint
{
    public string Open { get; set; }
    public string High { get; set; }
    public string Low { get; set; }
    public string Close { get; set; }
    public string Volume { get; set; }
    public string Adj { get; set; }
}




public class ChartDataTest
{
    public string Key { get; set; }
    public ChartDataPoint Value { get; set; }
}