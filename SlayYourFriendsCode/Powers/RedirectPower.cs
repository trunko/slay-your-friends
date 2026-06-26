using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace SlayYourFriends.SlayYourFriendsCode.Powers;

public class RedirectPower() : SlayYourFriendsPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Single;

    public override async Task AfterEnergyReset(Player player)
    {
        await PowerCmd.Remove(this);
    }
    
    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        foreach (var target in CombatState.GetTeammatesOf(Owner))
        {
            if (target is { IsAlive: true, IsPlayer: true } && target != Owner)
            {
                await PowerCmd.Apply<RedirectVictimPower>(new ThrowingPlayerChoiceContext(), target, Amount, Owner, null);
            }
        }
    }
    
    public override Decimal ModifyDamageMultiplicative(
        Creature? target,
        Decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        // If user is the only one alive, whack 'em
        Decimal damageScale = CombatState.Players.Count(x => x.Creature.IsAlive) == 1 ? 10m : 0m;
        
        return target != Owner || !props.IsPoweredAttack() ? 1M : damageScale;
    }
}