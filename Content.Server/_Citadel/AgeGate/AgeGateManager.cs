using System.Net;
using Content.Server.Database;
using Content.Shared.CCVar;
using Content.Shared._Citadel.AgeGate;
using Robust.Shared.Configuration;
using Robust.Shared.Network;

namespace Content.Server._Citadel.AgeGate;

/// <summary>
/// Will send the user a pop-up asking for them to input their age. If they submit an age below 18 they will be banned.
/// </summary>
public sealed class AgeGateManager
{
    [Dependency] private readonly IServerDbManager _dbManager = default!;
    [Dependency] private readonly INetManager _netManager = default!;
    [Dependency] private readonly IConfigurationManager _cfg = default!;

    public void Initialize()
    {
        _netManager.Connected += OnConnected;
        _netManager.RegisterNetMessage<SendAgeGateCheckMessage>();
        _netManager.RegisterNetMessage<AgeGateAcceptedMessage>(OnAgeGateAccepted);
    }

    private async void OnConnected(object? sender, NetChannelArgs e)
    {
        //true if we are both the local host and the AgeGateExemptLocal rule is true.
        var isLocalhost = IPAddress.IsLoopback(e.Channel.RemoteEndPoint.Address) &&
                          _cfg.GetCVar(CCVars.AgeGateExemptLocal);
        //Check if user has already completed the agegate.
        var alreadyChecked = await _dbManager.GetAgeGate(e.Channel.UserId);

        //Return early if we do not need to check. Do not send anything to client.
        if (isLocalhost || alreadyChecked)
        {
            return;
        }

        var showAgeGate = new SendAgeGateMessage();
        _netManager.ServerSendMessage(showRulesMessage, e.Channel);
    }

    private async void OnAgeGateAccepted(AgeGateAcceptedMessage message)
    {
        var overEighteen = message.MsgChannel.BirthDate

        await _dbManager.SetAgeGate(message.MsgChannel.UserId, overEighteen);
    }
}
