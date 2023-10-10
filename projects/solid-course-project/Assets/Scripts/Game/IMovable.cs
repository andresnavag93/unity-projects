using UnityEngine;
public interface IMovable
{
    void DoMove(float speed);

    void Configure(Animator animator, SpriteRenderer spriteRenderer, Transform transform);
}
