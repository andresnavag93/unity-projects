 using UnityEngine;
 using System.Collections;
 
 public class EndOflevel : MonoBehaviour {
 
     void OnTriggerEnter(Collider other)
     {
         Application.Quit();
     }
 }