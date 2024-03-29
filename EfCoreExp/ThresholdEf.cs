﻿namespace EfCoreExp;

public enum ThresholdParameter
{
    LossInEvent, SpanLoss, ReflectanceInEvent, FiberAttenuation, SegmentAttenuation, SegmentLoss, SegmentLengthChange, PortHealth
}

public class Threshold
{
    public int Id { get; init; }
    public ThresholdParameter Parameter { get; init; }

    public bool IsMinorOn { get; set; }
    public double? Minor { get; set;} 
    public bool IsMajorOn { get; set; }
    public double? Major { get; set;}
    public bool IsCriticalOn { get; set; }
    public double? Critical { get; set; }
}

public class ThresholdEf
{
    public int Id { get; init; }
    public ThresholdParameter Parameter { get; init; }

    public bool IsMinorOn { get; set; }
    public double? Minor { get; set; }
    public bool IsMajorOn { get; set; }
    public double? Major { get; set; }
    public bool IsCriticalOn { get; set; }
    public double? Critical { get; set; }


    public int? AlarmProfileId { get; set; } // foreign key
}