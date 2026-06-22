using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using SlayYourFriends.SlayYourFriendsCode.Cards;

namespace SlayYourFriends.SlayYourFriendsCode.Potions;

[Pool(typeof(SharedPotionPool))]
public class Syrup() : SlayYourFriendsPotion(
    PotionRarity.Rare,
    PotionUsage.CombatOnly,
    TargetType.AnyPlayer)
{
    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        AssertValidForTargetedPotion(target);
        NCombatRoom.Instance?.PlaySplashVfx(target, Colors.Purple);
        CardModel card = target.CombatState.CreateCard<LowTierCurse>(target.Player);
        CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(
            card, PileType.Hand, target.Player, CardPilePosition.Top)); 
    }
}