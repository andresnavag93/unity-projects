using UnityEngine;
public class HealItem : AbstractItem
{
    [SerializeField] private int _healQuantity;

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.collider.GetComponent<IHealReceiver>();
        if (IsPlayer(player))
        {
            ApplyHeal(player);
        }

    }

    private void ApplyHeal(IHealReceiver player)
    {
        player.Heal(_healQuantity);
        Destroy(gameObject);
    }

    private bool IsPlayer(IHealReceiver player)
    {
        return player != null;
    }
}
