using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{

    public static int collectableQuantity = 0;
    //public Text collectableText; Este no es practico si se utilizan prefabs
    Text collectableText; //Este es la buena practica si se utilizan prefab y el objecto/componente se busca en start
    //GameObject  collectablePart;
    AudioSource collectableAudio;
    // Start is called before the first frame update
    void Start()
    {
        collectableQuantity = 0; //Hay que hacer esto debido a q es un varible estatica y cuando se cambia de escena no se estaba reiniciando
        // collectablePart = GameObject.Find("CollectableParticle").GetComponent<ParticleSystem>(); //Retorna un objeto de tipo game object 
        //collectablePart = GameObject.Find("CollectableParticle");
        //collectableAudio = GameObject.Find("Collectables").GetComponent<AudioSource>();
        collectableAudio = GetComponentInParent<AudioSource>();
        collectableText = GameObject.Find("CollectableQuantityText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //collectablePart.transform.position = transform.position;
            collectableAudio.Play();
            //collectablePart.SetActive(true);
            //collectablePart.playOnAwake();

            gameObject.SetActive(false);
            collectableQuantity++;
            if (collectableQuantity < 10)
            {
                collectableText.text = "0" + collectableQuantity.ToString();
            }
            else
            {
                collectableText.text = collectableQuantity.ToString();
            }
            //collectablePart.SetActive(false);
            //collectableText.text = collectableQuantity.ToString();
        }
    }
}
