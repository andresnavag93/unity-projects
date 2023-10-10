using UnityEngine;

public abstract class AbstractItem : MonoBehaviour
{
    [SerializeField] private string id;
    public string Id => id;

    protected abstract void OnCollisionEnter2D(Collision2D other);
}
