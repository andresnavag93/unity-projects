using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMove : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float cO = transform.position.x - target.position.x;
        float cA = target.position.y - transform.position.y;
        float angle = Mathf.Atan2(cO, cA) * Mathf.Rad2Deg;

        rb.rotation = angle;
        rb.velocity = (target.position - transform.position).normalized * speed;
    }
}
