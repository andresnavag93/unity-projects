using UnityEngine;

/* Set permanent speed power up */
public class SpeedItem : AbstractItem
{
    [SerializeField] private float _speedToAssign;

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.collider.GetComponent<MovementController>();
        if (IsPlayer(player))
        {
            ApplySpeed(player);
        }
    }

    private void ApplySpeed(MovementController player)
    {
        player.SetSpeed(_speedToAssign);
        Destroy(gameObject);
    }

    private bool IsPlayer(MovementController player)
    {
        return player != null;
    }
}
