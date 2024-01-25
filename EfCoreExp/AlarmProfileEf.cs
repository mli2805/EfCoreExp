namespace EfCoreExp;

public enum AlarmProfileKind
{
    P2P,
    Pon
}


public class AlarmProfile
{
    public int Id { get; init; }

    public AlarmProfileKind Kind { get; set; }
    public string Name { get; set; } = null!;
    public bool IsProvisioningMode { get; set; }

    public List<Threshold> Thresholds { get; set; } = null!;
}

public class AlarmProfileEf
{
    public int Id { get; init; }
    public AlarmProfileKind Kind { get; set; }
    public string Name { get; set; } = null!;
    public bool IsProvisioningMode { get; set; }

    public ICollection<ThresholdEf> Thresholds { get; set; } = null!;
}