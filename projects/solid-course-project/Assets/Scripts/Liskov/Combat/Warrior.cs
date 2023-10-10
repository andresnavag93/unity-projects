
public class Warrior : AbstractHero
{
    private void DoMeleeAttack(IDamageReceiver opponent)
    {
        opponent.ReceiveDamage(10);
    }

    public override void ReceiveDamage(int damage)
    {

    }

    public override void DoAttack(IDamageReceiver opponent)
    {
        DoMeleeAttack(opponent);
    }

}

