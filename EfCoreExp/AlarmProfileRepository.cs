using Microsoft.EntityFrameworkCore;

namespace EfCoreExp
{
    public interface IAlarmProfileRepository
    {
        Task<List<AlarmProfile>> GetAll(CancellationToken ct);
        Task Add(AlarmProfile alarmProfile, CancellationToken ct);

    }

    public class AlarmProfileRepository(MyContext myContext) : IAlarmProfileRepository
    {
        public async Task<List<AlarmProfile>> GetAll(CancellationToken ct)
        {
            var alarmProfilesEf = await myContext.AlarmProfiles
                .Include(x => x.Thresholds)
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
    }
}
