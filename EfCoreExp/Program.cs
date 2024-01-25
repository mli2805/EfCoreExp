using System.Diagnostics;

namespace EfCoreExp;

internal class Program
{
    static async Task Main(string[] args)
    {

        var thr1 = new Threshold() { Parameter = ThresholdParameter.LossInEvent, Critical = 2.1 };
        var thr2 = new Threshold()
        {
            Parameter = ThresholdParameter.FiberAttenuation, Minor = 0.5, Major = 1.0, Critical = 2.0
        };
        var thr3 = new Threshold() { Parameter = ThresholdParameter.PortHealth, IsEnabled = true, Critical = 99.99 };
        var thr4 = new Threshold() { Parameter = ThresholdParameter.SegmentAttenuation, Critical = 6 };

        var alarmProfile1 = new AlarmProfile()
        {
            Kind = AlarmProfileKind.P2P, 
            Name = "Customers1",
            IsProvisioningMode = false,
            Thresholds = new List<Threshold> {thr1,  thr2 }
        };

        var alarmProfile2 = new AlarmProfile()
        {
            Kind = AlarmProfileKind.P2P,
            Name = "Default",
            IsProvisioningMode = false,
            Thresholds = new List<Threshold> { thr3, thr4 }
        };

        var ct = new CancellationToken();
        await using var myContext = new MyContext();
        var repo = new AlarmProfileRepository(myContext);
        await repo.Add(alarmProfile1, ct);
        await repo.Add(alarmProfile2, ct);

        var list = await repo.GetAll(ct);
        Debug.Assert(list.Count == 2);

        var alarmProfile = await repo.GetById(2, ct);
        Debug.Assert(alarmProfile != null);
        Debug.Assert(alarmProfile.Thresholds.Count == 2);
        Debug.Assert(alarmProfile.Thresholds.First(t=>t.Parameter == ThresholdParameter.PortHealth).IsEnabled);

        await repo.Delete(1, ct);
        var list2 = await repo.GetAll(ct);
        Debug.Assert(list2.Count == 1);

        Console.WriteLine("Done.");
        Console.WriteLine("");
        Console.WriteLine("");
    }
}