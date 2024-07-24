using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("HEY");
        Monster[] monsters = FindObjectsOfType<Monster>();

        foreach (Monster monster in monsters)
        {
            if (collision.gameObject == monster.gameObject)
            {
                Debug.Log("DESTROY");
                SpriteRenderer spriteRenderer = monster.gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = false;

                PolygonCollider2D polygonCollider = monster.gameObject.GetComponent<PolygonCollider2D>();
                if (polygonCollider != null)
                {
                    polygonCollider.enabled = false;
                }

                Player[] players = FindObjectsOfType<Player>();

                foreach (Player player in players)
                {
                    player.setNumCoin(1);
                }
            }
        }


        Villian[] villains = FindObjectsOfType<Villian>();

        foreach (Villian villain in villains)
        {
            if (collision.gameObject == villain.gameObject)
            {
                Debug.Log("DESTROY");
                SpriteRenderer spriteRenderer = villain.gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = false;

                PolygonCollider2D polygonCollider = villain.gameObject.GetComponent<PolygonCollider2D>();
                if (polygonCollider != null)
                {
                    polygonCollider.enabled = false;
                }

                Player[] players = FindObjectsOfType<Player>();

                foreach (Player player in players)
                {
                    player.setNumCoin(1);
                }
            }
        }
    }
}
