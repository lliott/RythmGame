using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputObject : MonoBehaviour
{

    [SerializeField] private bool canBePressed;
    [SerializeField] private bool obtained = false;

    [SerializeField] private KeyCode keyCode;

    [SerializeField] private GameObject hitEffect, goodEffect, perfectEffect, missEffect;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (canBePressed)
            {
                //GameManager.instance.NoteHit();

                obtained = true;
                gameObject.SetActive(false);

                if (Mathf.Abs (transform.position.y) > 0.25) //Mathf.Abs : converts our number to a positive number so if its -0.25 it becomes 0.25
                {
                    GameManager.instance.NormalHit();
                    Debug.Log(" Normal Hit ~ !");
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);

                } else if (Mathf.Abs(transform.position.y) > 0.10f) { 

                    GameManager.instance.GoodHit();
                    Debug.Log("!! Good Hit ~~ !!");
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else {

                    GameManager.instance.PerfectHit();
                    Debug.Log("~!!!~ Perfect Hit ~!!!~ ");
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Activator")
        {
            canBePressed = false;

            if (!obtained)
            {
                GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
        }
    }

}
