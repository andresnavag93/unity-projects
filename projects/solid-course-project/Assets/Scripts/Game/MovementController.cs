using Unity.Mathematics;
using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _initialSpeed = 1;

    private float _currentSpeed;

    private bool _isDrunk;

    private IMovable _regularMovable;
    private IMovable _drunkMovable;
    private IMovable _currentMovable;

    private void Awake()
    {
        Reset();
    }

    public void Configure(IMovable regularMovable, IMovable drunkMovable)
    {
        _regularMovable = regularMovable;
        _regularMovable.Configure(_animator, _spriteRenderer, transform);
        _drunkMovable = drunkMovable;
        _drunkMovable.Configure(_animator, _spriteRenderer, transform);
    }

    private void FixedUpdate()
    {
        _currentMovable.DoMove(_currentSpeed);
    }

    public void Reset()
    {
        _currentMovable = _regularMovable;
        _currentSpeed = _initialSpeed;
        _animator.SetBool("IsDeath", false);
    }

    public void SetSpeed(float newSpeed)
    {
        _currentSpeed = newSpeed;
    }

    public void SetDrunk(float drunkDuration)
    {
        _currentMovable = _drunkMovable;
        StartCoroutine(DisableDrunk(drunkDuration));
    }

    private IEnumerator DisableDrunk(float drunkDuration)
    {

        yield return new WaitForSeconds(drunkDuration);
        _currentMovable = _regularMovable;
    }
}
