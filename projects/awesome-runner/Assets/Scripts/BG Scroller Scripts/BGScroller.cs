using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

    public float offsetSpeed = -0.006f;
    private Renderer myRenderer;

    [HideInInspector]
    public bool canScroll;

    void Awake() {
        myRenderer = GetComponent<MeshRenderer>();
    }

    void Update() {
        if (canScroll) {
            //Acces texture object material
            myRenderer.material.mainTextureOffset -= new Vector2(offsetSpeed, 0); 
        }
    }

}//class
