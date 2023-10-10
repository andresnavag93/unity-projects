
public abstract class AbstractHero : IAttacker, IDamageReceiver
{
    public abstract void ReceiveDamage(int damage);

    public abstract void DoAttack(IDamageReceiver opponent);
}

