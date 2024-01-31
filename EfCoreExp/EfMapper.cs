namespace EfCoreExp;

public static class EfMapper
{
    public static AlarmProfile FromEf(this AlarmProfileEf alarmProfileEf)
    {
        return new AlarmProfile()
        {
            Id = alarmProfileEf.Id,
            Name = alarmProfileEf.Name,

            Thresholds = alarmProfileEf.Thresholds.Select(t => t.FromEf()).ToList(),
        };
    }

    public static AlarmProfileEf ToEf(this AlarmProfile alarmProfile)
    {
        return new AlarmProfileEf()
        {
            Id = alarmProfile.Id,
            Name = alarmProfile.Name,

            Thresholds = alarmProfile.Thresholds.Select(t => t.ToEf()).ToList(),
        };
    }

    public static Threshold FromEf(this ThresholdEf thresholdEf)
    {
        return new Threshold()
        {
            Id = thresholdEf.Id,
            Parameter = thresholdEf.Parameter,
            IsMinorOn = thresholdEf.IsMinorOn,
            IsMajorOn = thresholdEf.IsMajorOn,
            IsCriticalOn = thresholdEf.IsCriticalOn,
            Minor = thresholdEf.Minor,
            Major = thresholdEf.Major,
            Critical = thresholdEf.Critical,
        };
    }

    public static ThresholdEf ToEf(this Threshold threshold)
    {
        return new ThresholdEf()
        {
            Id = threshold.Id,
            Parameter = threshold.Parameter,
            IsMinorOn = threshold.IsMinorOn,
            IsMajorOn = threshold.IsMajorOn,
            IsCriticalOn = threshold.IsCriticalOn,
            Minor = threshold.Minor,
            Major = threshold.Major,
            Critical = threshold.Critical,
        };
    }

    public static MonitoringPort FromEf(this MonitoringPortEf monitoringPortEf)
    {
        return new MonitoringPort()
        {
            Id = monitoringPortEf.Id,
        };
    }

    public static MonitoringPortEf ToEf(this MonitoringPort monitoringPort)
    {
        return new MonitoringPortEf()
        {
            Id = monitoringPort.Id,
        };
    }
}