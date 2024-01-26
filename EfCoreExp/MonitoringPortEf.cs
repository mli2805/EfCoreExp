namespace EfCoreExp;

public class MonitoringPort
{
    public int Id { get; init; }

}
public class MonitoringPortEf
{
    public int Id { get; init; }
    public int? AlarmProfileId { get; set; } // foreign key

}