using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Il2CppSystem;
using Reactor;
using UnityEngine;

namespace MoreHats
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class MoreHats : BasePlugin
    {
        public const string Id = "vac.more.hats";

        public Harmony Harmony { get; } = new Harmony(Id);

        public override void Load()
        {
            Assets.LoadAssetBundle();
            Harmony.PatchAll();
        }
        
        private static bool _customHatsLoaded = false;

        [HarmonyPatch(typeof(HatManager), nameof(HatManager.GetHatById))]
        public static class AddCustomHats
        {
            public static void Prefix(PlayerControl __instance)
            {
                if (!_customHatsLoaded)
                {                    
                    var allHats = HatManager.Instance.AllHats;                    

                    var customHatNames = new[] { "Kaisar", "4head", "Dash", "Safari", "VACEfron", "xelA", "Jamilla", "Tato", "Jos", "Flub" };

                    foreach (var hatName in customHatNames)
                        allHats.Add(CreateHat(GetSprite(hatName)));

                    _customHatsLoaded = true;
                }               
            }

            private static Sprite GetSprite(string name)
                => Assets.LoadAsset(name).Cast<GameObject>().GetComponent<SpriteRenderer>().sprite;

            private static HatBehaviour CreateHat(Sprite sprite)
            {
                return new HatBehaviour
                {
                    MainImage = sprite,
                    ProductId = $"hat_{sprite.name}",
                    InFront = true,
                    NoBounce = true
                };
            }
        }
    }
}
