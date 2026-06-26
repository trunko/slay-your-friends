using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using SlayYourFriends.SlayYourFriendsCode.Cards;

namespace SlayYourFriends.SlayYourFriendsCode.Relics;

[Pool(typeof(SharedRelicPool))]
public class BoxingMitts : SlayYourFriendsRelic
{
	public override RelicRarity Rarity => (RelicRarity)3;

	public CardMultiplayerConstraint MultiplayerConstraint => (CardMultiplayerConstraint)1;

	protected override IEnumerable<IHoverTip> ExtraHoverTips =>
	[
		..HoverTipFactory.FromCardWithCardHoverTips<Mitts>(),
		HoverTipFactory.FromPower<StrengthPower>()
	];

	public override async Task BeforeCombatStartLate()
	{
		BoxingMitts relicSource = this;
		if (((RelicModel)relicSource).Owner.Creature.CombatState != null)
		{
			((RelicModel)relicSource).Flash();
			await CardPileCmd.AddGeneratedCardToCombat((CardModel)(object)((RelicModel)relicSource).Owner.Creature.CombatState.CreateCard<Mitts>(((RelicModel)relicSource).Owner), (PileType)2, ((RelicModel)relicSource).Owner, (CardPilePosition)1);
		}
	}
}
