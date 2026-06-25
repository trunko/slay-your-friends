using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using SlayYourFriends.SlayYourFriendsCode.Powers;

namespace SlayYourFriends.SlayYourFriendsCode.Cards;

[Pool(typeof(ColorlessCardPool))]
public class Redirect() : SlayYourFriendsCard(
    3,
    CardType.Power,
    CardRarity.Rare,
    TargetType.Self
)
{
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "PowerUp", Owner.Character.PowerUpAnimDelay);
        await PowerCmd.Apply<RedirectPower>(choiceContext, Owner.Creature, 1M, Owner.Creature, this);
    }

  protected override void OnUpgrade() => AddKeyword(CardKeyword.Retain);
}