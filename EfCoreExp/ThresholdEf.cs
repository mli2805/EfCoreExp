namespace EfCoreExp;

public enum RftsParameter
{
    LossInEvent, SpanLoss, FiberAttenuation, SegmentAttenuation, SegmentLoss, SegmentLengthChange, PortHealth
}

public class Threshold
{
    public int Id { get; init; }
    public RftsParameter Parameter { get; init; }
    public bool IsEnabled { get; set; }

    public bool IsSimple { get; set; }
    public double? SimpleValue { get; set; }
    public bool IsMinorEnabled { get; set; }
    public double? Minor { get; set;}
    public bool IsMajorEnabled { get; set; }
    public double? Major { get; set;}
    public bool IsCriticalEnabled { get; set; }
    public double? Critical { get; set; }
}

public class ThresholdEf
{
    public int Id { get; init; }
    public RftsParameter Parameter { get; init; }
    public bool IsEnabled { get; set; }

    public bool IsSimple { get; set; }
    public double? SimpleValue { get; set; }
    public bool IsMinorEnabled { get; set; }
    public double? Minor { get; set;}
    public bool IsMajorEnabled { get; set; }
    public double? Major { get; set;}

    public bool IsCriticalEnabled { get; set; }
    public double? Critical { get; set; }


    public int? AlarmProfileId { get; set; } // внешний ключ
    public AlarmProfileEf? AlarmProfile { get; set; } // навигационное свойство
}