using Unity.Mathematics;
using UnityEngine;

public class RegularMovable : IMovable
{

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Transform _transform;

    public void DoMove(float speed)
    {
        var horizontal = Input.GetAxis("Horizontal");
        SetAnimantio(horizontal);
        MovePlayer(speed, horizontal);
    }

    private void MovePlayer(float speed, float horizontal)
    {
        var x = horizontal * speed * Time.deltaTime;
        _transform.Translate(x, 0.0f, 0.0f);
    }

    private void SetAnimantio(float horizontal)
    {
        _animator.SetFloat("Horizontal", math.abs(horizontal));
        _spriteRenderer.flipX = horizontal < 0;
    }

    public void Configure(Animator animator, SpriteRenderer spriteRenderer, Transform transform)
    {
        _animator = animator;
        _spriteRenderer = spriteRenderer;
        _transform = transform;
    }
}
