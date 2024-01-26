using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace EfCoreExp;

internal class Program
{
    static async Task Main(string[] args)
    {
        var myContext = new MyContext();
        myContext.Database.EnsureDeleted();
        myContext.Database.EnsureCreated();

        await SeedMonitoringPorts();
        await SeedAlarmProfilesWithThresholds();
        await CheckGetAlarmProfiles();

        await CheckAssignAlarmProfileToMonitoringPort();
        await CheckDeleteAlarmProfile();

        Console.WriteLine("Done.");
        Console.WriteLine("");
        Console.WriteLine("");
    }

    private static async Task SeedMonitoringPorts()
    {
        var ct = new CancellationToken();
        await using var myContext = new MyContext();
        for (int i = 0; i < 5; i++)
        {
            var monitoringPort = new MonitoringPort();
            myContext.MonitoringPorts.Add(monitoringPort.ToEf());
        }
        await myContext.SaveChangesAsync(ct);
    }
    
    private static async Task SeedAlarmProfilesWithThresholds()
    {
        var ct = new CancellationToken();
        await using var myContext = new MyContext();
        var thr1 = new Threshold() { Parameter = ThresholdParameter.LossInEvent, Critical = 2.1 };
        var thr2 = new Threshold()
        {
            Parameter = ThresholdParameter.FiberAttenuation,
            Minor = 0.5,
            Major = 1.0,
            Critical = 2.0
        };
        var thr3 = new Threshold() { Parameter = ThresholdParameter.PortHealth, IsEnabled = true, Critical = 99.99 };
        var thr4 = new Threshold() { Parameter = ThresholdParameter.SegmentAttenuation, Critical = 6 };

        var alarmProfile1 = new AlarmProfile()
        {
            Kind = AlarmProfileKind.P2P,
            Name = "Customers1",
            IsProvisioningMode = false,
            Thresholds = new List<Threshold> { thr1, thr2 }
        };

        var alarmProfile2 = new AlarmProfile()
        {
            Kind = AlarmProfileKind.P2P,
            Name = "Default",
            IsProvisioningMode = false,
            Thresholds = new List<Threshold> { thr3, thr4 }
        };

        var alarmProfileRepo = new AlarmProfileRepository(myContext);
        await alarmProfileRepo.Add(alarmProfile1, ct);
        await alarmProfileRepo.Add(alarmProfile2, ct);
    }

    private static async Task CheckGetAlarmProfiles()
    {
        var ct = new CancellationToken();
        await using var myContext = new MyContext();
        var alarmProfileRepo = new AlarmProfileRepository(myContext);

        var list = await alarmProfileRepo.GetAll(ct);
        Debug.Assert(list.Count == 2);

        var alarmProf2 = await alarmProfileRepo.GetById(2, ct);
        Debug.Assert(alarmProf2 != null);
        Debug.Assert(alarmProf2.Thresholds.Count == 2);
        Debug.Assert(alarmProf2.Thresholds.First(t => t.Parameter == ThresholdParameter.PortHealth).IsEnabled);
    }

    private static async Task CheckAssignAlarmProfileToMonitoringPort()
    {
        var ct = new CancellationToken();
        await using var myContext = new MyContext();

        var alarmProfileRepository = new AlarmProfileRepository(myContext);
        await alarmProfileRepository.AssignToMonitoringPort(1, 3, ct);
        await alarmProfileRepository.AssignToMonitoringPort(2, 4, ct);

        var alarmProfile = await alarmProfileRepository.GetForMonitoringPort(3, ct);
        Debug.Assert(alarmProfile != null);
        Debug.Assert(alarmProfile.Name == "Customers1");
    }

    private static async Task CheckDeleteAlarmProfile()
    {
        var ct = new CancellationToken();
        await using var myContext = new MyContext();

        var alarmProfileRepo = new AlarmProfileRepository(myContext);
        await alarmProfileRepo.Delete(1, ct);
        var list2 = await alarmProfileRepo.GetAll(ct);
        Debug.Assert(list2.Count == 1);

        var alarmProfile = await alarmProfileRepo.GetForMonitoringPort(3, ct);
        Debug.Assert(alarmProfile == null);
    }
}