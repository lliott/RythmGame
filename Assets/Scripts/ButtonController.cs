using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    [Header("Image")]
    [SerializeField] private Sprite defaultImage;
    [SerializeField] private Sprite pressedImage;
    [Header("Input Key")]
    [SerializeField] private KeyCode keyCode;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            spriteRenderer.sprite = pressedImage;
        }

        if (Input.GetKeyUp(keyCode))
        {
            spriteRenderer.sprite = defaultImage;
        }

    }
}
