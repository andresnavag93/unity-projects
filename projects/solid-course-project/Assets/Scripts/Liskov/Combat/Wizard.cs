using System;
using UnityEngine;
public enum ElementType
{
    Fire,
    Water,
    Earth,
    Air,
    ice
}
public class Element
{
    public ElementType type;
    public float multiplier;
    public float probability;
}
public class Wizard : AbstractHero
{
    private float _baseDamage;

    private Element _fireElement;
    private Element _waterElement;
    private Element _iceElement;

    public override void DoAttack(IDamageReceiver opponent)
    {
        //TODO: Update
    }

    private void DoMagicAttack(IDamageReceiver opponent, Element element)
    {
        //TODO: Update

        /*
        * Is important to group var declation with code invocation for a better undestand
        */

        var damage = Mathf.RoundToInt(_baseDamage * element.multiplier);
        opponent.ReceiveDamage(damage);
    }

    public override void ReceiveDamage(int damage)
    {
        //TODO: Update
    }
}
