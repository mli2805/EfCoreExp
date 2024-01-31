namespace EfCoreExp;

public class AlarmProfile
{
    public int Id { get; init; }

    public string Name { get; set; } = null!;

    public List<Threshold> Thresholds { get; set; } = null!;
    public List<MonitoringPort> MonitoringPorts { get; set; } = null!;

}

public class AlarmProfileEf
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;

    public ICollection<ThresholdEf> Thresholds { get; set; } = null!;
    public ICollection<MonitoringPortEf> MonitoringPorts { get; set; } = null!;
}