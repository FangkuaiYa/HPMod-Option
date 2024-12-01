using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Unity.IL2CPP;
using CustomOption.CustomOption;
using HarmonyLib;

namespace CustomOption;

[BepInPlugin(Id, ModName, VersionString)]
[BepInProcess("Among Us.exe")]

public partial class CustomOptionPlugin : BasePlugin
{
    public Harmony Harmony { get; } = new(Id);
    public const string ModName = "CustomOption";
    public const string Id = "harrypotter.fangkuai.customoption";
    public const string VersionString = "1.0.0";
    public static int optionsPage = 1;

    public override void Load()
    {
        Generate.GenerateAll();
        Harmony.PatchAll();
    }
}
