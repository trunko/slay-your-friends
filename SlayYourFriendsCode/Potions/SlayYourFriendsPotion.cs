using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Potions;
using SlayYourFriends.SlayYourFriendsCode.Extensions;

namespace SlayYourFriends.SlayYourFriendsCode.Potions;

public abstract class SlayYourFriendsPotion(PotionRarity rarity, PotionUsage usage, TargetType targetType)
    : CustomPotionModel
{
    public override PotionRarity Rarity => rarity;

    public override PotionUsage Usage => usage;

    public override TargetType TargetType => targetType;

    //Image size:
    //Full art: 80x80
    public override string? CustomPackedImagePath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImagePath();
    public override string? CustomPackedOutlinePath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionOutlineImagePath();
}