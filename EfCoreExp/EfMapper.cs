namespace EfCoreExp
{
    public static class EfMapper
    {
        public static AlarmProfile FromEf(this AlarmProfileEf alarmProfileEf)
        {
            return new AlarmProfile()
            {
                Id = alarmProfileEf.Id,
                Kind = alarmProfileEf.Kind,
                Name = alarmProfileEf.Name,
                IsProvisioningMode = alarmProfileEf.IsProvisioningMode,

                Thresholds = alarmProfileEf.Thresholds.Select(t => t.FromEf()).ToList(),
            };
        }

        public static AlarmProfileEf ToEf(this AlarmProfile alarmProfile)
        {
            return new AlarmProfileEf()
            {
                Id = alarmProfile.Id,
                Kind = alarmProfile.Kind,
                Name = alarmProfile.Name,
                IsProvisioningMode = alarmProfile.IsProvisioningMode,

                Thresholds = alarmProfile.Thresholds.Select(t => t.ToEf()).ToList(),
            };
        }

        public static Threshold FromEf(this ThresholdEf thresholdEf)
        {
            return new Threshold()
            {
                Id = thresholdEf.Id,
                Parameter = thresholdEf.Parameter,
                IsEnabled = thresholdEf.IsEnabled,
                IsSimple = thresholdEf.IsSimple,
                SimpleValue = thresholdEf.SimpleValue,
                IsMinorEnabled = thresholdEf.IsMinorEnabled,
                Minor = thresholdEf.Minor,
                IsMajorEnabled = thresholdEf.IsMajorEnabled,
                Major = thresholdEf.Major,
                IsCriticalEnabled = thresholdEf.IsCriticalEnabled,
                Critical = thresholdEf.Critical,
            };
        }

        public static ThresholdEf ToEf(this Threshold threshold)
        {
            return new ThresholdEf()
            {
                Id = threshold.Id,
                Parameter = threshold.Parameter,
                IsEnabled = threshold.IsEnabled,
                IsSimple = threshold.IsSimple,
                SimpleValue = threshold.SimpleValue,
                IsMinorEnabled = threshold.IsMinorEnabled,
                Minor = threshold.Minor,
                IsMajorEnabled = threshold.IsMajorEnabled,
                Major = threshold.Major,
                IsCriticalEnabled = threshold.IsCriticalEnabled,
                Critical = threshold.Critical,
            };
        }
    }
}
