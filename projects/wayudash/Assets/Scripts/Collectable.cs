using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CollectableType{
    money
}

public class Collectable : MonoBehaviour
{

    public CollectableType type = CollectableType.money;
    private SpriteRenderer sprite;
    private CircleCollider2D itemCollider;
    bool hasBeenCollected = false;
    public int value = 1;
    // Start is called before the first frame update
    
    private void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<CircleCollider2D>();
    }

    void Show(){
        sprite.enabled = true;
        itemCollider.enabled = true;
        hasBeenCollected = false;
    }
    
    void Hide(){
        sprite.enabled = false;
        itemCollider.enabled = true;
    }

    void Collect(){
        hasBeenCollected = true;
        Hide();

        switch(this.type){
            case CollectableType.money:
                GameManager.sharedInstance.CollectObject(this);
                GetComponent<AudioSource>().Play();
                
            break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            if (other.GetComponent<CapsuleCollider2D>().IsTouching(itemCollider)){
                if (!hasBeenCollected){
                    Collect();
                }
            }
        }
    }
}
