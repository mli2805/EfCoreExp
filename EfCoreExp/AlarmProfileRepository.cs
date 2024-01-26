using Microsoft.EntityFrameworkCore;

namespace EfCoreExp;

public interface IAlarmProfileRepository
{
    Task<List<AlarmProfile>> GetAll(CancellationToken ct);
    Task Add(AlarmProfile alarmProfile, CancellationToken ct);
    Task Delete(int id, CancellationToken ct);

    Task<AlarmProfile?> GetById(int id, CancellationToken ct);

    Task AssignToMonitoringPort(int alarmProfileId, int monitoringPortId, CancellationToken ct);
    Task<AlarmProfile?> GetForMonitoringPort(int monitoringPortId, CancellationToken ct);

    Task UpdateAlarmProfile(int id, AlarmProfilePatch patch, CancellationToken ct);
}

public class AlarmProfileRepository(MyContext myContext) : IAlarmProfileRepository
{
    public async Task<List<AlarmProfile>> GetAll(CancellationToken ct)
    {
        var alarmProfilesEf = await myContext.AlarmProfiles
            .Include(x => x.Thresholds)
            .Include(x=>x.MonitoringPorts)
            .ToListAsync(ct);

        var alarmProfiles = alarmProfilesEf.Select(a => a.FromEf()).ToList();
        return alarmProfiles;
    }

    public async Task Add(AlarmProfile alarmProfile, CancellationToken ct)
    {
        var alarmProfileEf = alarmProfile.ToEf();
        await myContext.AlarmProfiles.AddAsync(alarmProfileEf, ct);
        await myContext.SaveChangesAsync(ct);
    }

    public async Task Delete(int id, CancellationToken ct)
    {
        var existingProfile = await myContext.AlarmProfiles.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (existingProfile == null)
        {
            throw new NullReferenceException($"Alarm profile {id} not found");
        }

        myContext.Remove(existingProfile);
        await myContext.SaveChangesAsync(ct);
    }

    public async Task<AlarmProfile?> GetById(int id, CancellationToken ct)
    {
        var alarmProfileEf = await myContext.AlarmProfiles
            .Include(x=>x.Thresholds)
            .Include(x=>x.MonitoringPorts)
            .FirstOrDefaultAsync(a => a.Id == id, ct);
        return alarmProfileEf?.FromEf() ?? null;
    }

    public async Task AssignToMonitoringPort(int alarmProfileId, int monitoringPortId, CancellationToken ct)
    {
        var alarmProfileEf = await myContext.AlarmProfiles
            .Include(x=>x.Thresholds)
            .Include(x=>x.MonitoringPorts)
            .FirstOrDefaultAsync(a => a.Id == alarmProfileId, ct);

        if (alarmProfileEf == null)
        {
            throw new NullReferenceException($"Alarm profile {alarmProfileId} not found");
        }

        var monitoringPortEf = await myContext.MonitoringPorts.SingleAsync(m => m.Id == monitoringPortId, ct);
        if (monitoringPortEf == null)
        {
            throw new NullReferenceException($"Monitoring port {monitoringPortId} not found");
        }

        alarmProfileEf.MonitoringPorts.Add(monitoringPortEf);
        await myContext.SaveChangesAsync(ct);
    }

    public async Task<AlarmProfile?> GetForMonitoringPort(int monitoringPortId, CancellationToken ct)
    {
        var alarmProfileEf = await myContext.AlarmProfiles
            .Include(x => x.Thresholds)
            .Include(x => x.MonitoringPorts)
            .FirstOrDefaultAsync(a => a.MonitoringPorts
                .Select(m=>m.Id).Contains(monitoringPortId), ct);

        return alarmProfileEf?.FromEf() ?? null;
    }


    public async Task UpdateAlarmProfile(int id, AlarmProfilePatch patch, CancellationToken ct)
    {
        var existingProfile = await myContext.AlarmProfiles.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (existingProfile == null)
        {
            throw new NullReferenceException($"Alarm profile {id} not found");
        }

        await using var transaction = await myContext.Database.BeginTransactionAsync(ct);

        try
        {
            await PatchAlarmProfile(existingProfile, patch, ct);

            //TODO update thresholds

            await transaction.CommitAsync(ct);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await transaction.RollbackAsync(ct);
            throw;
        }
    }

    private async Task PatchAlarmProfile(AlarmProfileEf alarmProfile, AlarmProfilePatch patch, CancellationToken ct)
    {
        if (patch.Name is not null) { alarmProfile.Name = patch.Name; }

        if (patch.Kind is not null)
        {
            alarmProfile.Kind = (AlarmProfileKind)patch.Kind;
        }

        if (patch.IsProvisioningMode is not null)
        {
            alarmProfile.IsProvisioningMode = (bool)patch.IsProvisioningMode;
        }

        await myContext.SaveChangesAsync(ct);
    }
}