using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;
using SlayYourFriends.SlayYourFriendsCode.Cards;

namespace SlayYourFriends.SlayYourFriendsCode.Cards;

[Pool(typeof(IroncladCardPool))]
public class Bodyguard() : SlayYourFriendsCard(0,
    CardType.Skill, CardRarity.Uncommon,
    TargetType.AnyPlayer)
{
    public override bool GainsBlock => true;
    
    public override CardMultiplayerConstraint MultiplayerConstraint
    {
        get => CardMultiplayerConstraint.MultiplayerOnly;
    }
    protected override IEnumerable<DynamicVar> CanonicalVars => [new GoldVar(2), new BlockVar(8, ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        await PlayerCmd.LoseGold(Mathf.Min(DynamicVars.Gold.IntValue,  cardPlay.Target.Player.Gold),  cardPlay.Target.Player);
        Bodyguard bodyguard = this;
        ArgumentNullException.ThrowIfNull((object) cardPlay.Target, "cardPlay.Target");
        Decimal num = await CreatureCmd.GainBlock(cardPlay.Target, bodyguard.DynamicVars.Block, cardPlay);
        
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Gold.UpgradeValueBy(2M);
        this.DynamicVars.Block.UpgradeValueBy(5M);
    }
}