using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace SlayYourFriends.SlayYourFriendsCode.Cards;

[Pool(typeof(RegentCardPool))]
public class Taxes() : SlayYourFriendsCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)

{
    public override bool CanBeGeneratedInCombat => false;
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardKeyword.Exhaust];

    protected override IEnumerable<DynamicVar> CanonicalVars => [new ("rate", 3M)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var goldGain = 0;
        
        foreach (var player in CombatState.Players)
        {
            if (player == Owner)
            {
                continue;
            }

            var amount = player.Gold * DynamicVars.GetValueOrDefault("rate").BaseValue / 100;
            await PlayerCmd.LoseGold(Mathf.Min((int) Math.Floor(amount), player.Gold), player);
            goldGain += (int) Math.Floor(amount);
        }
        await PowerCmd.Apply<RoyaltiesPower>(choiceContext, Owner.Creature, goldGain, Owner.Creature, this);
    }

    protected override void OnUpgrade() => DynamicVars.GetValueOrDefault("rate").UpgradeValueBy(2M);
}