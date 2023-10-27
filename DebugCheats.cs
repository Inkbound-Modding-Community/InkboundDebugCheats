using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using ShinyShoe;
using ShinyShoe.Ares;
using System;
using System.Reflection;

namespace InkboundDebugCheats {
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class DebugCheats : BaseUnityPlugin {
        public const string PLUGIN_GUID = "ADDB.DebugCheats";
        public const string PLUGIN_NAME = "Debug Cheats";
        public const string PLUGIN_VERSION = "1.0.1";
        private bool pressed1 = false;
        private bool pressed2 = false;
        private bool pressed3 = false;
        private bool pressed4 = false;
        private bool pressed5 = false;
        private bool pressed6 = false;
        private bool pressed7 = false;
        private bool pressed8 = false;
        private bool pressed9 = false;
        private bool pressed10 = false;
        private bool pressed11 = false;
        private bool pressed12 = false;
        private bool pressed13 = false;
        private ConfigEntry<KeyboardShortcut> addKwillings;
        private ConfigEntry<KeyboardShortcut> addGlyphs;
        private ConfigEntry<KeyboardShortcut> spawnVestige;
        private ConfigEntry<KeyboardShortcut> addStatus;
        private ConfigEntry<KeyboardShortcut> winFight;
        private ConfigEntry<KeyboardShortcut> winBook;
        private ConfigEntry<KeyboardShortcut> winRun;
        private ConfigEntry<KeyboardShortcut> skipToVillain;
        private ConfigEntry<KeyboardShortcut> cooldowns;
        private ConfigEntry<KeyboardShortcut> ascension;
        private ConfigEntry<KeyboardShortcut> augment;
        private ConfigEntry<KeyboardShortcut> ability;
        private ConfigEntry<KeyboardShortcut> godMode;
        private ConfigEntry<int> kwillingsIncrement;
        private ConfigEntry<int> glyphIncrement;
        private ConfigEntry<string> vestigeDataID;
        private ConfigEntry<string> statusDataID;
        private ConfigEntry<string> ascensionDataID;
        private ConfigEntry<string> augmentDataID;
        private ConfigEntry<string> abilityID;
        private static ManualLogSource log;
        private void Awake() {
            addKwillings = Config.Bind("Currency", "Add Kwillings Hotkey", new KeyboardShortcut(UnityEngine.KeyCode.F1, UnityEngine.KeyCode.LeftShift));
            kwillingsIncrement = Config.Bind("Currency", "Kwillings Increment", 1000);
            addGlyphs = Config.Bind("Currency", "Add Glyphs Hotkey", new KeyboardShortcut(UnityEngine.KeyCode.F2, UnityEngine.KeyCode.LeftShift));
            glyphIncrement = Config.Bind("Currency", "Glyphs Increment", 20);
            spawnVestige = Config.Bind("Vestiges", "Spawn Vestige Hotkey", new KeyboardShortcut(UnityEngine.KeyCode.F3, UnityEngine.KeyCode.LeftShift));
            vestigeDataID = Config.Bind("Vestiges", "DataID of Vestige which will be spawned", "yyxTLlM3");
            addStatus = Config.Bind("Status Effect", "Add Status Effect Hotkey", new KeyboardShortcut(UnityEngine.KeyCode.F4, UnityEngine.KeyCode.LeftShift));
            statusDataID = Config.Bind("Status Effect", "DataID of Status Effect which will be added", "dSV3stvC");
            winFight = Config.Bind("Combat and Progress", "Win Fight Hotkey", new KeyboardShortcut(UnityEngine.KeyCode.F5, UnityEngine.KeyCode.LeftShift));
            winBook = Config.Bind("Combat and Progress", "Win Book Hotkey", new KeyboardShortcut(UnityEngine.KeyCode.F6, UnityEngine.KeyCode.LeftShift));
            winRun = Config.Bind("Combat and Progress", "Win Run Hotkey", new KeyboardShortcut(UnityEngine.KeyCode.F7, UnityEngine.KeyCode.LeftShift));
            skipToVillain = Config.Bind("Combat and Progress", "Skip to Villain Hotkey", new KeyboardShortcut(UnityEngine.KeyCode.F8, UnityEngine.KeyCode.LeftShift));
            cooldowns = Config.Bind("Combat and Progress", "Clear Cooldowns Hotkey", new KeyboardShortcut(UnityEngine.KeyCode.F9, UnityEngine.KeyCode.LeftShift));
            ascension = Config.Bind("Abilities", "Draft Ascension Hotkey", new KeyboardShortcut(UnityEngine.KeyCode.F10, UnityEngine.KeyCode.LeftShift));
            ascensionDataID = Config.Bind("Abilities", "DataID of Ascension which will be drafted", "ASj6kIxa");
            augment = Config.Bind("Abilities", "Draft Augment Hotkey", new KeyboardShortcut(UnityEngine.KeyCode.F11, UnityEngine.KeyCode.LeftShift));
            augmentDataID = Config.Bind("Abilities", "GUID of Augment which will be drafted", "L6NjlY59");
            ability = Config.Bind("Abilities", "Draft Ability Hotkey", new KeyboardShortcut(UnityEngine.KeyCode.F12, UnityEngine.KeyCode.LeftShift));
            abilityID = Config.Bind("Abilities", "ID of Ability which will be drafted", "5wyBWo28");
            godMode = Config.Bind("Combat and Progress", "Toggle Godmode Hotkey", new KeyboardShortcut(UnityEngine.KeyCode.F1, UnityEngine.KeyCode.LeftControl));
            log = Logger;
            new Harmony(PLUGIN_GUID).PatchAll(Assembly.GetExecutingAssembly());
            Logger.LogInfo($"Plugin {PLUGIN_GUID} is loaded!");
        }
        private void Update() {
            try {
                var aps = ClientApp.Inst?._applicationState;
                if (aps != null) {
                    WorldCommand wc = null;
                    if (addKwillings.Value.IsDown() && !pressed1) {
                        pressed1 = true;
                        wc = new WorldCommandModifyInventoryCurrency(aps.GetNetworkRo().LocalEntityHandleInWorld, CurrencyType.Run, kwillingsIncrement.Value);
                    }
                    if (addGlyphs.Value.IsDown() && !pressed2) {
                        pressed2 = true;
                        wc = new WorldCommandModifyInventoryCurrency(aps.GetNetworkRo().LocalEntityHandleInWorld, CurrencyType.Gold, glyphIncrement.Value);
                    }
                    if (spawnVestige.Value.IsDown() && !pressed3) {
                        pressed3 = true;
                        wc = new WorldCommandCheatSpawnItem(aps.GetNetworkRo().LocalEntityHandleInWorld, vestigeDataID.Value);
                    }
                    if (addStatus.Value.IsDown() && !pressed4) {
                        pressed4 = true;
                        wc = new WorldCommandAddStatusEffect(aps.GetNetworkRo().LocalEntityHandleInWorld, aps.GetNetworkRo().LocalEntityHandleInWorld, 1, statusDataID.Value);
                    }
                    if (winFight.Value.IsDown() && !pressed5) {
                        pressed5 = true;
                        wc = new WorldCommandCheatWinBattle();
                    }
                    if (winBook.Value.IsDown() && !pressed6) {
                        pressed6 = true;
                        wc = new WorldCommandCheatWinBook();
                    }
                    if (winRun.Value.IsDown() && !pressed7) {
                        pressed7 = true;
                        wc = new WorldCommandCheatWinRun();
                    }
                    if (skipToVillain.Value.IsDown() && !pressed8) {
                        pressed8 = true;
                        wc = new WorldCommandCheatAdvanceToVillain();
                    }
                    if (cooldowns.Value.IsDown() && !pressed9) {
                        pressed9 = true;
                        wc = new WorldCommandCheatClearCooldowns(aps.GetNetworkRo().LocalEntityHandleInWorld);
                    }
                    if (ascension.Value.IsDown() && !pressed10) {
                        pressed10 = true;
                        wc = new WorldCommandCheatApplyAbilityAscension() {
                            playerUnitHandle = aps.GetNetworkRo().LocalEntityHandleInWorld,
                            abilityAscensionGuid = ascensionDataID.Value
                        };
                    }
                    if (augment.Value.IsDown() && !pressed11) {
                        pressed11 = true;
                        wc = new WorldCommandCheatApplyAbilityUpgrade(aps.GetNetworkRo().LocalEntityHandleInWorld, augmentDataID.Value);
                    }
                    if (ability.Value.IsDown() && !pressed12) {
                        pressed12 = true;
                        wc = new WorldCommandCheatDraftAbility(aps.GetNetworkRo().LocalEntityHandleInWorld, abilityID.Value);
                    }
                    if (godMode.Value.IsDown() && !pressed13) {
                        pressed13 = true;
                        wc = new WorldCommandCheatGodMode(aps.GetNetworkRo().LocalEntityHandleInWorld, true, false);
                    }
                    if (wc != null) {
                        aps.WorldClient.BufferWorldCommand(wc);
                    } else {
                        pressed1 = false;
                        pressed2 = false;
                        pressed3 = false;
                        pressed4 = false;
                        pressed5 = false;
                        pressed6 = false;
                        pressed7 = false;
                        pressed8 = false;
                        pressed9 = false;
                        pressed10 = false;
                        pressed11 = false;
                        pressed12 = false;
                        pressed13 = false;
                    }
                }
            } catch (Exception ex) {
                log.LogError(ex);
            }
        }
        [HarmonyPatch(typeof(CoreUtilShared))]
        public static class CoreUtilShared_Patch {
            [HarmonyPatch(nameof(CoreUtilShared.AreCheatsEnabled))]
            [HarmonyPostfix]
            public static void AreCheatsEnabled(ref bool __result) {
                __result = true;
            }
        }
    }
}
