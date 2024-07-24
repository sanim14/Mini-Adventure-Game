using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;

    private GameObject player;
    [SerializeField] private float speed = 2f;
    Rigidbody2D rb;
    private float spawnTimer = 0f;
    private static int monstersSpawned = 0;
    private float spawnInterval = 2f;
    private int maxMonsters = 20;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        player = GameObject.FindWithTag("Player");
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
                Instantiate(monsterPrefab, new Vector2(-14.9f, 5.6f), Quaternion.identity);
                monstersSpawned++;
                spawnTimer = 0f;
            }
        }
    }
}