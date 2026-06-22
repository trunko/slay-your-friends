using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace SlayYourFriends.SlayYourFriendsCode.Potions;

[Pool(typeof(TokenPotionPool))]
public class Grass() : SlayYourFriendsPotion(
    PotionRarity.Token,
    PotionUsage.AnyTime,
    TargetType.Self)
{
    private const string _healPercentKey = "HealPercent";
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new ("HealPercent", 75M)];
    
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        AssertValidForTargetedPotion(target);
        NCombatRoom.Instance?.PlaySplashVfx(target, Colors.Green);
        await CreatureCmd.Heal(target, target.MaxHp * DynamicVars["HealPercent"].BaseValue / 100M);
    }
}