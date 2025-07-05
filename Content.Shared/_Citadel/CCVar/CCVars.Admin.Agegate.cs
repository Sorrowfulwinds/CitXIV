using Robust.Shared.Configuration;

namespace Content.Shared._Citadel.CCVar;

public sealed partial class CCVars
{
    /// <summary>
    ///     Don't show age gate to localhost/loopback interface.
    /// </summary>
    public static readonly CVarDef<bool> AgeGateExemptLocal =
        CVarDef.Create("agegate.exempt_local", true, CVar.SERVERONLY);
}
