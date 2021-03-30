using System.Linq;
using System.Reflection;
using Reactor.Extensions;
using UnityEngine;

namespace MoreHats
{
    public class Assets
    {
        private static AssetBundle AssetBundle;

        public static void LoadAssetBundle()
        {
            byte[] bundleRead = Assembly.GetCallingAssembly().GetManifestResourceStream("MoreHats.Assets.hatobjects").ReadFully();
            AssetBundle = AssetBundle.LoadFromMemory(bundleRead);           
        }

        public static Object LoadAsset(string name)
        {
            return AssetBundle.LoadAsset(name);
        }
    }
}

