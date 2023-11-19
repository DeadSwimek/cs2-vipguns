using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Utils;
using CounterStrikeSharp.API.Modules.Menu;
using CounterStrikeSharp.API.Modules.Entities;

namespace VIPGuns;


public class VIPGuns : BasePlugin
{
    public override string ModuleName => "[VIP] Guns Menu";
    public override string ModuleAuthor => "DeadSwim";
    public override string ModuleDescription => "Take gun for VIP Players.";
    public override string ModuleVersion => "V. 1.0.0 [Beta]";
    private static readonly int?[] Used = new int?[65];


    private string Prefix = $"[{ChatColors.Lime}MadGames.eu{ChatColors.White}]";


    public override void Load(bool hotReload)
    {
        AddCommandListener("ak", CommandVIP_ak);
        AddCommandListener("m4", CommandVIP_m4);
    }
    [RequiresPermissions("@css/vip")]
    private HookResult CommandVIP_ak(CCSPlayerController? player, CommandInfo info)
    {
        var client = player.EntityIndex!.Value.Value;
        if (!player.IsValid)
        {
            return HookResult.Continue;
        }

        if (!player.PlayerPawn.IsValid)
        {
            return HookResult.Continue;
        }
        if (Used[client] == 1)
        {
            player.PrintToChat($"{Prefix} You can't use this command 2x.");
        }
        else
        {

            player.GiveNamedItem("weapon_ak47");
            player.PrintToChat($"{Prefix} You got AK-47.");
            Used[client] = 1;
        }
        return HookResult.Stop;
    }
    [RequiresPermissions("@css/vip")]
    private HookResult CommandVIP_m4(CCSPlayerController? player, CommandInfo info)
    {
        var client = player.EntityIndex!.Value.Value;
        if (!player.IsValid)
        {
            return HookResult.Continue;
        }

        if (!player.PlayerPawn.IsValid)
        {
            return HookResult.Continue;
        }
        if (Used[client] == 1)
        {
            player.PrintToChat($"{Prefix} You can't use this command 2x.");
        }
        else
        {
            player.GiveNamedItem("weapon_m4a1");
            player.PrintToChat($"{Prefix} You got M4A1.");
            Used[client] = 1;
        }
        return HookResult.Stop;
    }
    [GameEventHandler]
    public HookResult OnClientConnect(EventPlayerConnectFull @event, GameEventInfo info)
    {
        CCSPlayerController player = @event.Userid;

        if (player == null || !player.IsValid || player.IsBot)
            return HookResult.Continue;


        var client = player.EntityIndex!.Value.Value;
        Used[client] = 0;
        //player.PrintToChat($"{Prefix} You can use /ak for give AK47 or /m4 for give M4A1");
        
        return HookResult.Continue;
    }
    [GameEventHandler]
    public HookResult OnClientSpawn(EventPlayerSpawn @event, GameEventInfo info)
    {
        CCSPlayerController player = @event.Userid;

        if (player == null || !player.IsValid || player.IsBot)
            return HookResult.Continue;

        if (player.UserId != null)
        {
            var client = player.EntityIndex!.Value.Value;
            Used[client] = 0;
            //player.PrintToChat($"{Prefix} You can use /ak for give AK47 or /m4 for give M4A1");
        }

        return HookResult.Continue;
    }

}
