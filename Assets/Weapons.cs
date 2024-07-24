using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
/*      GameObject bombObject = GameObject.FindWithTag("Bomb");

        if (bombObject != null)
        {
            SpriteRenderer spriteRenderer = bombObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
        }


        GameObject crossBowObject = GameObject.FindWithTag("CrossBow");

        if (crossBowObject != null)
        {
            SpriteRenderer spriteRenderer = crossBowObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
        }


        GameObject potionObject = GameObject.FindWithTag("Potion");

        if (potionObject != null)
        {
            SpriteRenderer spriteRenderer = potionObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
        }


        GameObject scrollObject = GameObject.FindWithTag("Scroll");

        if (scrollObject != null)
        {
            SpriteRenderer spriteRenderer = scrollObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //Find a way so that once you buy it has to show up in every scene
        if (gameObject.CompareTag("bomb"))
        {
            Player[] players = FindObjectsOfType<Player>();

            foreach (Player player in players)
            {
                if (player.getNumCoin() >= 5)
                {
                    player.setNumCoin(-5);
                    player.setBomb(true);
                }
            }

            GameObject bombObject = GameObject.FindWithTag("Bomb");

            if (bombObject != null)
            {
                SpriteRenderer spriteRenderer = bombObject.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = true;
                }
            }
        }

        if (gameObject.CompareTag("crossbow"))
        {
            Player[] players = FindObjectsOfType<Player>();

            foreach (Player player in players)
            {
                if (player.getNumCoin() >= 30)
                {
                    player.setNumCoin(-30);
                    player.setCrossbow(true);
                }
            }

            GameObject crossBowObject = GameObject.FindWithTag("CrossBow");

            if (crossBowObject != null)
            {
                SpriteRenderer spriteRenderer = crossBowObject.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = true;
                }
            }
        }

        if (gameObject.CompareTag("potion"))
        {
            Player[] players = FindObjectsOfType<Player>();

            foreach (Player player in players)
            {
                if (player.getNumCoin() >= 5)
                {
                    player.setNumCoin(-5);
                    player.setPotion(true);
                }
            }

            GameObject potionObject = GameObject.FindWithTag("Potion");

            if (potionObject != null)
            {
                SpriteRenderer spriteRenderer = potionObject.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = true;
                }
            }
        }


        if (gameObject.CompareTag("scroll"))
        {
            Player[] players = FindObjectsOfType<Player>();

            foreach (Player player in players)
            {
                if (player.getNumCoin() >= 9)
                {
                    player.setNumCoin(-9);
                    player.setScroll(true);
                }
            }

            GameObject scrollObject = GameObject.FindWithTag("Scroll");

            if (scrollObject != null)
            {
                SpriteRenderer spriteRenderer = scrollObject.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = true;
                }
            }
        }
    }
}
