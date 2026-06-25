using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using SlayYourFriends.SlayYourFriendsCode.Potions;

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
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Retain];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPotion<Grass>()];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await PotionCmd.TryToProcure<Grass>(Owner);
        await CreatureCmd.Kill(Owner.Creature);
    }
    
    public override bool ShouldPlay(CardModel card, AutoPlayType autoPlayType)
    {
        if (card.Owner != Owner)
            return true;

        if (Pile == null)
            return true;
        
        return Pile.Type != PileType.Hand || card is LowTierCurse || autoPlayType != AutoPlayType.None;
    }
}