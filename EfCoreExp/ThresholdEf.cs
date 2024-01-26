namespace EfCoreExp;

public enum ThresholdParameter
{
    LossInEvent, SpanLoss, FiberAttenuation, SegmentAttenuation, SegmentLoss, SegmentLengthChange, PortHealth
}

public class Threshold
{
    public int Id { get; init; }
    public ThresholdParameter Parameter { get; init; }
    public bool IsEnabled { get; set; } // threshold generally

    public double? Minor { get; set;} // level is disabled if Minor == null
    public double? Major { get; set;}
    public double? Critical { get; set; }
}

public class ThresholdEf
{
    public int Id { get; init; }
    public ThresholdParameter Parameter { get; init; }
    public bool IsEnabled { get; set; }

    public double? Minor { get; set; }
    public double? Major { get; set; }
    public double? Critical { get; set; }


    public int? AlarmProfileId { get; set; } // foreign key
}