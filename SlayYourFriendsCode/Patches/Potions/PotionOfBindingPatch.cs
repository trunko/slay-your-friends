using HarmonyLib;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Potions;

namespace SlayYourFriends.SlayYourFriendsCode.Patches.Potions;

public class PotionOfBindingPatch
{
    [HarmonyPatch(typeof(PotionOfBinding), nameof(PotionOfBinding.ExtraHoverTips), MethodType.Getter)]
    public class ExtraHoverTipsPatch
    {
        [HarmonyPrefix]
        static bool Custom(ref IEnumerable<IHoverTip> __result)
        {
            __result = Array.Empty<IHoverTip>();
            return false;
        }
    }
}
