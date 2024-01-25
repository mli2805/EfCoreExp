namespace EfCoreExp;

public record ThresholdPatch
(
    ThresholdParameter? Parameter,
    bool? IsEnabled,
    double? Minor,
    double? Major,
    double? Critical
);