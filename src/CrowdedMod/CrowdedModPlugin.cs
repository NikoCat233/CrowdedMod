﻿using AmongUs.GameOptions;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using CrowdedMod.Components;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using System.Linq;

namespace CrowdedMod;

[BepInAutoPlugin("xyz.crowdedmods.crowdedmod")]
[BepInProcess("Among Us.exe")]
//[BepInDependency(ReactorPlugin.Id)]
//[BepInDependency("gg.reactor.debugger", BepInDependency.DependencyFlags.SoftDependency)] // fix debugger overwriting MinPlayers
public partial class CrowdedModPlugin : BasePlugin
{
    public const int MaxPlayers = 127;
    public const int MaxImpostors = 127 / 2;

    private Harmony Harmony { get; } = new(Id);

    public override void Load()
    {
        NormalGameOptionsV07.RecommendedImpostors = NormalGameOptionsV07.MaxImpostors = Enumerable.Repeat(127, 127).ToArray();
        NormalGameOptionsV07.MinPlayers = Enumerable.Repeat(4, 127).ToArray();

        ClassInjector.RegisterTypeInIl2Cpp<MeetingHudPagingBehaviour>();
        ClassInjector.RegisterTypeInIl2Cpp<ShapeShifterPagingBehaviour>();
        ClassInjector.RegisterTypeInIl2Cpp<VitalsPagingBehaviour>();

        Harmony.PatchAll();
    }
}