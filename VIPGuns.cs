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
    public override string ModuleVersion => "V. 1.0.2 [Beta]";

    private static readonly int?[] Used = new int?[65];
    private static readonly int?[] LastUsed = new int?[65];

    public int Round;

    private string Prefix = $"[{ChatColors.Lime}MadGames.eu{ChatColors.White}]";


    public override void Load(bool hotReload)
    {
        Console.WriteLine("VIP Menu started, created by DeadSwim");
        RegisterListener<Listeners.OnMapStart>(name =>
        {
            Round = 0;
        });
    }
    [RequiresPermissions("@css/vip")]
    [ConsoleCommand("ak", "Give a VIP Ak47")]
    public void CommandVIP_ak(CCSPlayerController? player, CommandInfo info)
    {
        var client = player.EntityIndex!.Value.Value;
        if (!player.IsValid || !player.PlayerPawn.IsValid)
        {
            return;
        }

        if (Round < 5)
        {
            player.PrintToChat($"{Prefix} Must be third round to use this command.");
        }
        else
        {
            if (Used[client] == 1)
            {
                player.PrintToChat($"{Prefix} You can't use this command 2x.");
            }
            else
            {

                player.GiveNamedItem("weapon_ak47");
                player.PrintToChat($"{Prefix} You got AK-47.");
                Used[client] = 1;
                LastUsed[client] = 1;
            }
        }
    }
    [RequiresPermissions("@deadswim/vip")]
    [ConsoleCommand("pack1", "Give you Pack 2")]
    public void CommandVIP_pack2(CCSPlayerController? player, CommandInfo info)
    {
        var client = player.EntityIndex!.Value.Value;
        if (!player.IsValid || !player.PlayerPawn.IsValid)
        {
            return;
        }
        if (Round < 5)
        {
            player.PrintToChat($"{Prefix} Must be third round to use this command.");
        }
        {
            if (Used[client] == 1)
            {
                player.PrintToChat($"{Prefix} You can't use this command 2x.");
            }
            else
            {
                player.GiveNamedItem("weapon_ak47");
                player.GiveNamedItem("weapon_deagle");
                player.GiveNamedItem("weapon_healthshot");

                player.GiveNamedItem("weapon_molotov");
                player.GiveNamedItem("weapon_smokegrenade");
                player.GiveNamedItem("weapon_hegrenade");
                player.PrintToChat($"{Prefix} You got pack number 2.");
                Used[client] = 1;
                LastUsed[client] = 2;
            }
        }
    }
    [RequiresPermissions("@deadswim/vip")]
    [ConsoleCommand("pack1", "Give you Pack 1")]
    public void CommandVIP_pack1(CCSPlayerController? player, CommandInfo info)
    {
        var client = player.EntityIndex!.Value.Value;
        if (!player.IsValid || !player.PlayerPawn.IsValid)
        {
            return;
        }
        if (Round < 5)
        {
            player.PrintToChat($"{Prefix} Must be third round to use this command.");
        }
        {
            if (Used[client] == 1)
            {
                player.PrintToChat($"{Prefix} You can't use this command 2x.");
            }
            else
            {
                player.GiveNamedItem("weapon_m4a1");
                player.GiveNamedItem("weapon_deagle");
                player.GiveNamedItem("weapon_healthshot");

                player.GiveNamedItem("weapon_molotov");
                player.GiveNamedItem("weapon_smokegrenade");
                player.GiveNamedItem("weapon_hegrenade");
                player.PrintToChat($"{Prefix} You got pack number 1.");
                Used[client] = 1;
                LastUsed[client] = 3; 
            }
        }
    }
    [RequiresPermissions("@css/vip")]
    [ConsoleCommand("guns_off", "Turn off giving automaticlly weapons")]
    public void CommandGUNS_off(CCSPlayerController? player, CommandInfo info)
    {
        var client = player.EntityIndex!.Value.Value;
        if (!player.IsValid || !player.PlayerPawn.IsValid)
        {
            return;
        }
        if (LastUsed[client] >= 1)
        {
            LastUsed[client] = 0;
            player.PrintToCenter($"You turn off automatically weapon..");
        }
    }
    [RequiresPermissions("@css/vip")]
    [ConsoleCommand("m4", "Give a VIP M4A1")]
    public void CommandVIP_m4(CCSPlayerController? player, CommandInfo info)
    {
        var client = player.EntityIndex!.Value.Value;
        if (!player.IsValid || !player.PlayerPawn.IsValid)
        {
            return;
        }

        if (Round < 5)
        {
            player.PrintToChat($"{Prefix} Must be third round to use this command.");
        }
        {
            if (Used[client] == 1)
            {
                player.PrintToChat($"{Prefix} You can't use this command 2x.");
            }
            else
            {
                player.GiveNamedItem("weapon_m4a1");
                player.PrintToChat($"{Prefix} You got M4A1.");
                Used[client] = 1;
                LastUsed[client] = 4;


            }
        }
    }
    [GameEventHandler]
    public HookResult OnClientConnect(EventPlayerConnectFull @event, GameEventInfo info)
    {
        CCSPlayerController player = @event.Userid;

        if (player == null || !player.IsValid || player.IsBot)
            return HookResult.Continue;


        var client = player.EntityIndex!.Value.Value;
        Used[client] = 0;
        LastUsed[client] = 0;
        //player.PrintToChat($"{Prefix} You can use /ak for give AK47 or /m4 for give M4A1");
        
        return HookResult.Continue;
    }
    [GameEventHandler]
    public HookResult OnRoundStart(EventRoundStart @event, GameEventInfo info)
    {
        Round++;
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
            if (LastUsed[client] == 1) {
                player.GiveNamedItem("weapon_ak47");
                player.PrintToChat($"{Prefix} You got automatically AK-47, if you wanna turn off type /guns_off.");
                Used[client] = 1;
            }
            if (LastUsed[client] == 2)
            {
                player.GiveNamedItem("weapon_ak47");
                player.GiveNamedItem("weapon_deagle");
                player.GiveNamedItem("weapon_healthshot");

                player.GiveNamedItem("weapon_molotov");
                player.GiveNamedItem("weapon_smokegrenade");
                player.GiveNamedItem("weapon_hegrenade");
                player.PrintToChat($"{Prefix} You got automatically Pack 2, if you wanna turn off type /guns_off.");
                Used[client] = 1;
            }
            if (LastUsed[client] == 3)
            {
                player.GiveNamedItem("weapon_m4a1");
                player.GiveNamedItem("weapon_deagle");
                player.GiveNamedItem("weapon_healthshot");

                player.GiveNamedItem("weapon_molotov");
                player.GiveNamedItem("weapon_smokegrenade");
                player.GiveNamedItem("weapon_hegrenade");
                player.PrintToChat($"{Prefix} You got automatically Pack 1, if you wanna turn off type /guns_off.");
                Used[client] = 1;
            }
            if (LastUsed[client] == 4)
            {
                player.GiveNamedItem("weapon_m4a1");
                player.PrintToChat($"{Prefix} You got automatically M4A1, if you wanna turn off type /guns_off.");
                Used[client] = 1;
            }
            //player.PrintToChat($"{Prefix} You can use /ak for give AK47 or /m4 for give M4A1");
        }

        return HookResult.Continue;
    }

}
