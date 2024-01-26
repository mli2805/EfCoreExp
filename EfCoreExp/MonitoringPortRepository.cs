using Microsoft.EntityFrameworkCore;

namespace EfCoreExp;

public interface IMonitoringPortRepository
{
    // Task AssignAlarmProfile(int monitoringPortId, int alarmProfileId, CancellationToken ct);
    Task<MonitoringPort?> GetById (int monitoringPortId);
}

public class MonitoringPortRepository(MyContext myContext): IMonitoringPortRepository
{
    // public async Task AssignAlarmProfile(int monitoringPortId, int alarmProfileId, CancellationToken ct)
    // {
    //     var portEf = myContext.MonitoringPorts
    //         .Include(x => x.AlarmProfile)
    //         .Single(x => x.Id == monitoringPortId);
    //
    //     if (portEf.AlarmProfile != null && portEf.AlarmProfile.Id == alarmProfileId)
    //     {
    //         return;
    //     }
    //
    //     var alarmProfileEf = await myContext.AlarmProfiles
    //         .Include(x => x.Thresholds)
    //         .FirstOrDefaultAsync(a => a.Id == alarmProfileId, ct);
    //
    //     if (alarmProfileEf == null)
    //     {
    //         throw new NullReferenceException($"alarm profile {alarmProfileId} not found");
    //     }
    //
    //     portEf.AlarmProfile = alarmProfileEf;
    //     await myContext.SaveChangesAsync(ct);
    // }

    public async Task<MonitoringPort?> GetById(int monitoringPortId)
    {
        var portEf = await myContext.MonitoringPorts
            .FirstOrDefaultAsync(p => p.Id == monitoringPortId);
        return portEf?.FromEf() ?? null;
    }
}