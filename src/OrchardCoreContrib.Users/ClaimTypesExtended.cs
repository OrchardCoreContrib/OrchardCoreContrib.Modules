namespace OrchardCoreContrib.Users;

/// <summary>
/// Represents a claim types that be used for impersonation process.
/// </summary>
public static class ClaimTypesExtended
{
    /// <summary>
    /// Gets the impersonator name.
    /// </summary>
    public static readonly string ImpersonatorNameIdentifier = nameof(ImpersonatorNameIdentifier);

    /// <summary>
    /// Gets whether the current user is impersonated or not.
    /// </summary>
    public static readonly string IsImpersonating = nameof(IsImpersonating);
}
