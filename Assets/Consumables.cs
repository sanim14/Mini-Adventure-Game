using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Consumables : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    bool followBomb = false;
    float timer = 0f;
    bool animate = false;
    bool crossBowFollow = false;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void setCrossBowFollow(bool isCrossFollow)
    {
        crossBowFollow = isCrossFollow;
    }

    public void setFollowBomb(bool isFollow)
    {
        followBomb = isFollow;
    }

    // Update is called once per frame
    void Update()
    {
        if (followBomb == true)
        {
            GameObject useBomb = GameObject.FindWithTag("useBomb");
            Vector3 offset;
            offset = transform.up * 4f;


            Player[] players = FindObjectsOfType<Player>();

            foreach (Player player in players)
            {
                useBomb.transform.rotation = player.transform.rotation;
                useBomb.transform.position = player.transform.position + offset;
            }

            timer -= Time.deltaTime;
            Debug.Log("Follow: " + followBomb);
            Debug.Log(timer);
            Debug.Log(animate);
            if (timer <= 0 && animate == true)
            {
                Debug.Log("HEYYYY");
                useBomb = GameObject.FindWithTag("useBomb");
                Destroy(useBomb);
                animate = false;
                followBomb = false;
                timer = 0f;
            }

            if (timer <= 0)
            {
                GameObject bomb = GameObject.FindWithTag("Bomb");
                if (bomb != null)
                {
                    bomb.GetComponent<Consumables>().setFollowBomb(false);
                }
            }
        }

        if (crossBowFollow == true)
        {
            GameObject crossbow = GameObject.FindWithTag("useCrossBow");
            Vector3 offset = Vector3.zero;
            Player[] players = FindObjectsOfType<Player>();

            foreach (Player player in players)
            {
                player.setFollowSword(false);
                
                if (player.getRight())
                {
                    offset = player.transform.right * 1.5f;
                    //crossbow.transform.rotation = Quaternion.Euler(0, 0, 270);
                }
                else
                {
                    offset = player.transform.right * -1.5f;
                    crossbow.transform.rotation = player.transform.rotation;
                }

                crossbow.transform.position = player.transform.position + offset;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Instantiate the bullet slightly to the right or left of the crossbow
                Vector3 bulletOffset = offset.normalized * 0.5f; // Offset the bullet by 0.5 units
                GameObject b = Instantiate(bulletPrefab, crossbow.transform.position + bulletOffset, crossbow.transform.rotation);
                Rigidbody2D bulletRB = b.GetComponent<Rigidbody2D>();

                players = FindObjectsOfType<Player>();

                foreach (Player player in players)
                {
                    if (player.getRight())
                    {
                        bulletRB.velocity = crossbow.transform.right * 5f; // Shoot right
                    }
                    else
                    {
                        bulletRB.velocity = -crossbow.transform.right * 5f; // Shoot left
                    }
                }
                
            }
        }
    }

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("CrossBow"))
        {
            crossBowFollow = true;
            Player[] players = FindObjectsOfType<Player>();

            foreach (Player player in players)
            {
                player.setFollowSword(false);
            }
        }
        if (gameObject.CompareTag("Bomb"))
        {
            //MAKE IT DISAPPEAR
            followBomb = true;

            Player[] players = FindObjectsOfType<Player>();

            foreach (Player player in players)
            {
                player.setBomb(false);
            }
        }
        if (gameObject.CompareTag("Potion"))
        {
            Player[] players = FindObjectsOfType<Player>();

            foreach (Player player in players)
            {
                Debug.Log("SIZE UP");
                player.transform.localScale *= 1.15f;
                player.setPotion(false);
            }
        }
        if (gameObject.CompareTag("Scroll"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                Player[] players = FindObjectsOfType<Player>();

                foreach (Player player in players)
                {
                    player.setNumCoin(5);
                    player.setScroll(false);
                }
            }
        }
        if (gameObject.CompareTag("useBomb"))
        {
            followBomb = true;
            animator.SetBool("fire", true);
            animate = true;
            timer = 4f;
            GameObject useBomb = GameObject.FindWithTag("useBomb");
            useBomb.transform.localScale *= 1.75f;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (animate == true)
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
}
