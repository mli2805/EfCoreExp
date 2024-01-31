namespace EfCoreExp;

public record AlarmProfilePatch(
    string? Name,
    List<ThresholdPatch>? Thresholds
);