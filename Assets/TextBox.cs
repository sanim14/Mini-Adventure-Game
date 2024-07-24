using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setEnabled(bool enable)
    {
        spriteRenderer.enabled = enable;
    }
}
