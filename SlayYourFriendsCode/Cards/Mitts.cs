using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace SlayYourFriends.SlayYourFriendsCode.Cards;

[Pool(typeof(TokenCardPool))]
public class Mitts : SlayYourFriendsCard
{
	protected override IEnumerable<DynamicVar> CanonicalVars =>
	[
		new DynamicVar("StrikeRequirement", 2m),
		new DynamicVar("StrengthPerTick", 1m)
	];

	public override CardMultiplayerConstraint MultiplayerConstraint => (CardMultiplayerConstraint)1;

	public override IEnumerable<CardKeyword> CanonicalKeywords => [ CardKeyword.Exhaust];

	public Mitts()
		: base(0, (CardType)2, (CardRarity)7, (TargetType)6)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
	{
		Mitts cardSource = this;
		if (play.Target == null)
		{
			return;
		}
		ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
		ushort strikeCounter = 0;
		if (play.Target.Player == null)
		{
			return;
		}
		PlayerCombatState playerCombatState = play.Target.Player.PlayerCombatState;
		foreach (CardModel card in (playerCombatState != null) ? playerCombatState.AllCards : null)
		{
			if ((int)card.Rarity == 1 && card.Tags.Contains((CardTag)1))
			{
				strikeCounter++;
				if ((decimal)strikeCounter % ((IReadOnlyDictionary<string, DynamicVar>)((CardModel)this).DynamicVars).GetValueOrDefault("StrikeRequirement").BaseValue == 0m)
				{
					await PowerCmd.Apply<StrengthPower>(choiceContext, ((CardModel)cardSource).Owner.Creature, 1m, ((CardModel)cardSource).Owner.Creature, (CardModel)(object)cardSource, false);
				}
			}
		}
	}

	protected override void OnUpgrade()
	{
		((CardModel)this).RemoveKeyword((CardKeyword)1);
	}
}
