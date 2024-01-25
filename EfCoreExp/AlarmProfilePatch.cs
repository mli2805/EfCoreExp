namespace EfCoreExp;

public record AlarmProfilePatch(
    AlarmProfileKind? Kind, 
    string? Name,
    bool? IsProvisioningMode,
    List<ThresholdPatch>? Thresholds
);