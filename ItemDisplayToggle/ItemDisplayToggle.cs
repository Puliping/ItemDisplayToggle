using BepInEx;
using BepInEx.Configuration;
using RoR2;
using UnityEngine;

namespace ItemDisplayToggle
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class ItemDisplayToggle : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Puliping";
        public const string PluginName = "ItemDisplayToggle";
        public const string PluginVersion = "0.1.0";

        // Config entries
        public static ConfigEntry<bool> ItemEnable { get; set; }
        public static ConfigEntry<bool> EquipEnable { get; set; }

        public void Awake()
        {
            // Binding config entries
            ItemEnable = Config.Bind<bool>(
                "Item Display",
                "ItemEnable",
                true,
                "If true, enables display of items on characters."
            );
            On.RoR2.CharacterModel.EnableItemDisplay += (orig, self, itemIndex) => {
                if(ItemEnable.Value) orig(self, itemIndex);
            };

            EquipEnable = Config.Bind<bool>(
                "Item Display",
                "EquipEnable",
                true,
                "If true, enables display of equipment on characters."
            );
            On.RoR2.CharacterModel.SetEquipmentDisplay += (orig, self, newEquipIndex) => {
                if(EquipEnable.Value) orig(self, newEquipIndex);
            };
        }
    }
}
