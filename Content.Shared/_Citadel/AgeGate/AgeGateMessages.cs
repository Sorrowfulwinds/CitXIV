using Lidgren.Network;
using Robust.Shared.Network;
using Robust.Shared.Serialization;

namespace Content.Shared._Citadel.AgeGate;

/// <summary>
/// Sent by the server when the client connects to check the client's age and displaying a popup.
/// </summary>
public sealed class SendAgeGateCheckMessage : NetMessage
{
    public override MsgGroups MsgGroup => MsgGroups.Command;

    public override void ReadFromBuffer(NetIncomingMessage buffer, IRobustSerializer serializer)
    {
    }

    public override void WriteToBuffer(NetOutgoingMessage buffer, IRobustSerializer serializer)
    {
    }
}

/// <summary>
/// Sent by the client when it has submitted an age gate check.
/// </summary>
public sealed class AgeGateAcceptedMessage : NetMessage
{
    public override MsgGroups MsgGroup => MsgGroups.Command;

    public DateOnly BirthDate { get; set; }

    public override void ReadFromBuffer(NetIncomingMessage buffer, IRobustSerializer serializer)
    {
    }

    public override void WriteToBuffer(NetOutgoingMessage buffer, IRobustSerializer serializer)
    {
        buffer.Write(BirthDate.DayNumber);
    }
}
