using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    Rigidbody2D enemyRb;
    SpriteRenderer enemySpriteRend;
    Animator enemyAnim;
    float timeBeforeChange;
    public float delay = .5f;
    public float speed = .3f;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemySpriteRend = GetComponent<SpriteRenderer>();
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //enemyRb.velocity = Vector2.right * speed;
        enemyRb.velocity = new Vector2(speed, enemyRb.velocity.y);

        if(speed > 0)
            enemySpriteRend.flipX = false;
        else if (speed<0)
            enemySpriteRend.flipX = true;

        if(timeBeforeChange < Time.time){
            //speed = speed * -1;
            speed *= -1;
            timeBeforeChange = Time.time + delay;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")){
            if (transform.position.y + .3f < other.transform.position.y){
                enemyAnim.SetBool("isDead", true);
            }
        }
    }

    public void DisableEnemy(){
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
