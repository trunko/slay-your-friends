using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace SlayYourFriends.SlayYourFriendsCode.Cards;

[Pool(typeof(CurseCardPool))]
public class LowTierCurse() : SlayYourFriendsCard(
    0,
    CardType.Curse,
    CardRarity.Curse,
    TargetType.Self)
{
    public override bool CanBeGeneratedByModifiers => false;
    
    protected override bool ShouldGlowRedInternal => true;
    
    public override int MaxUpgradeLevel => 0;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.Kill(Owner.Creature);
    }
    
    public override bool ShouldPlay(CardModel card, AutoPlayType autoPlayType)
    {
        if (card.Owner != Owner)
            return true;
            
        return (Pile != null ? (Pile.Type != PileType.Hand ? 1 : 0) : 1) != 0 || card is LowTierCurse || autoPlayType != AutoPlayType.None;
    }
}