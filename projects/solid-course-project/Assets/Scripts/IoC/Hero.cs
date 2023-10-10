using UnityEngine;
public class Hero : MonoBehaviour, IHero
{
    [SerializeField] private float _speed;
    [SerializeField] private Movement _movement;

    private void Awake()
    {
        _movement.Configure(this, _speed);
    }

    public void DoMovement(float x)
    {
        transform.Translate(x, 0.0f, 0.0f);
    }
}

