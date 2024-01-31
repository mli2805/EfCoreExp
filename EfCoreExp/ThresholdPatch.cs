namespace EfCoreExp;

public record ThresholdPatch
(
    ThresholdParameter Parameter,
    bool IsMinorOn,
    bool IsMajorOn,
    bool IsCriticalOn,
    double? Minor,
    double? Major,
    double? Critical
);