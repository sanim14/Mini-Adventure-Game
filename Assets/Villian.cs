using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Villian : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] GameObject bullet;
    private GameObject player;
    [SerializeField] private float speed = 2f;
    Rigidbody2D rb;
    private float spawnTimer = 0f;
    private static int monstersSpawned = 0;
    private float spawnInterval = 2f;
    private int maxMonsters = 4;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        player = GameObject.FindWithTag("Player");
        InvokeRepeating("Shoot", 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (monstersSpawned < maxMonsters)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                Instantiate(monsterPrefab, new Vector2(-25.3f, 8.4f), Quaternion.identity);
                monstersSpawned++;
                spawnTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.enabled == true)
        {
            GameObject b = Instantiate(bullet, transform.position + transform.right * 3f, transform.rotation);
            Rigidbody2D bulletRB = b.GetComponent<Rigidbody2D>();

            Player[] players = FindObjectsOfType<Player>();

            foreach (Player player in players)
            {
                if (player != null)
                {
                    Vector2 direction = (player.transform.position - transform.position).normalized;
                    bulletRB.velocity = direction * 5f;
                }
                else
                {
                    // Fallback if player is null (should not happen in normal circumstances)
                    bulletRB.velocity = transform.right * 5f;
                }
            }
        }
    }
}
