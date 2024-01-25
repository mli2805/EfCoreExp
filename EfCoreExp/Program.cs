namespace EfCoreExp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var thr1 = new Threshold() { Parameter = RftsParameter.LossInEvent, IsSimple = true, SimpleValue = 2.1 };
            var thr2 = new Threshold()
            {
                Parameter = RftsParameter.FiberAttenuation, IsSimple = false, Minor = 0.5, Major = 1.0, Critical = 2.0
            };
            var thr3 = new Threshold() { Parameter = RftsParameter.PortHealth, IsSimple = true, SimpleValue = 99.99 };
            var thr4 = new Threshold() { Parameter = RftsParameter.SegmentAttenuation, IsSimple = false, Critical = 6 };

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
            Console.WriteLine($"{list.Count} alarm profiles in DB");

            Console.WriteLine("Done.");
            Console.ReadKey();
        }
    }
}
