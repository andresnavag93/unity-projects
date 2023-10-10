using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeactiveAfterTime());
    }

    IEnumerator DeactiveAfterTime()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

} //class
