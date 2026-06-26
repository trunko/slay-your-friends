using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace SlayYourFriends.SlayYourFriendsCode.Powers;

public class RedirectVictimPower() : SlayYourFriendsPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Single;

    public override async Task AfterEnergyReset(Player player)
    {
        await PowerCmd.Remove(this);
    }
    
    public override Decimal ModifyDamageMultiplicative(
        Creature? target,
        Decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        // Splits damage from the redirect among other alive players
        Decimal damageScale = CombatState.Players.Count(x => x.Creature.IsAlive) switch
        {
            2 => 2m,
            3 => 1.5m,
            4 => 4 / 3m,
            _ => 0m
        };

        return target != Owner || !props.IsPoweredAttack() ? 1M : damageScale;
    }
}