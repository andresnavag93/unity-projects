
public class Archer : AbstractHero
{

    private void DoRangeAttack(IDamageReceiver opponent)
    {
        opponent.ReceiveDamage(10);

    }
    public override void ReceiveDamage(int damage)
    {
    }

    public override void DoAttack(IDamageReceiver opponent)
    {
        DoRangeAttack(opponent);
    }
}

