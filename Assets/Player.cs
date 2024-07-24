using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.SceneManagement;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    bool up, right, left, down;
    private static GameObject original = null;
    [SerializeField] float speed;
    private Text textbox;
    private Text coinCounter;
    bool villager1 = false;
    int currentTextIndex = 0;
    int numCoin = 0;
    int hearts = 5;
    bool showText = false;
    bool addedCoin = true;
    string[] texts = 
    {"Villager: Your timing of visit is unfortunate as recently our town was plagued by evildoers who took everything from our people.",
    "Villager: Would you be willing to help us save our town and bring back prosperity for our people!",
    "Player: Sure",
    "Villager: Thank you!",
    "Villager: Make way to my friend near the forest to get some coins and begin your mission. I wish you the best of luck"};

    bool villager2 = false;
    int currentTextIndex2 = 0;
    bool showText2 = false;
    string[] texts2 =
    {"Villager: Here are some coins to help you on your journey.",
    "Villager: Make your way to the forest, but be warned there will be some obstacles along the way."};

    private GameObject sword;
    bool followSword = false;

    bool hasABomb = false;
    bool hasACrossBow = false;
    bool hasAPotion = false;
    bool hasAScroll = false;

    Rigidbody2D rb;
    // Start is called before the first frame update

    public void setBomb(bool bomb)
    {
        hasABomb = bomb;
    }

    public void setFollowSword(bool isFollow)
    {
        followSword = isFollow;
    }

    public bool getRight()
    {
        return right;
    }

    public void setCrossbow(bool crossbow)
    {
        hasACrossBow = crossbow;
    }

    public void setPotion(bool potion)
    {
        hasAPotion = potion;
    }

    public void setScroll(bool scroll)
    {
        hasAScroll = scroll;
    }

    public void setHearts()
    {
        hearts = 5;
    }


    void Start()
    {
        DontDestroyOnLoad(this);

        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Awake()
    {
        if (original == null)
            original = gameObject;
        if (gameObject != original)
            Destroy(gameObject);
    }

    void SetActiveHeart(string tag, bool active)
    {
        GameObject heart = GameObject.FindGameObjectWithTag(tag);
        if (heart != null)
        {
            heart.SetActive(active);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hearts == 0)
        {
            //FINISH
            hearts = 5;
            numCoin = 0;
            SceneManager.LoadScene(0);
        }
        else if (hearts == 1)
        {
            SetActiveHeart("heart2", false);
            SetActiveHeart("heart3", false);
            SetActiveHeart("heart4", false);
            SetActiveHeart("heart5", false);
        }
        else if (hearts == 2)
        {
            SetActiveHeart("heart3", false);
            SetActiveHeart("heart4", false);
            SetActiveHeart("heart5", false);
        }
        else if (hearts == 3)
        {
            SetActiveHeart("heart4", false);
            SetActiveHeart("heart5", false);
        }
        else if (hearts == 4)
        {
            SetActiveHeart("heart5", false);
        }

        if (SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 3)
        {
            TextBox[] textBoxes = FindObjectsOfType<TextBox>();

            foreach (TextBox textBox in textBoxes)
            {
                textBox.setEnabled(true);
            }

            textbox = GameObject.Find("TextBox").GetComponent<Text>();
            textbox.text = "Welcome! Click on the items you wish to purchase: ";
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            followSword = !followSword;
        }

        if (followSword)
        {
            GameObject crossBowObject = GameObject.FindWithTag("CrossBow");
            if (crossBowObject != null)
            {
                Consumables consumables = crossBowObject.GetComponent<Consumables>();
                if (consumables != null)
                {
                    consumables.setCrossBowFollow(false);
                    GameObject crossbow = GameObject.FindWithTag("useCrossBow");
                    if (crossbow != null)
                    {
                        crossbow.transform.position = new Vector2(30.4f, -0.8f);
                    }
                }
            }


            sword = GameObject.FindWithTag("Sword");
            Vector3 offset;
            if (right)
            {
                offset = transform.right * 1.6f;
                sword.transform.rotation = Quaternion.Euler(0, 0, 270);
            }
            else
            {
                offset = transform.right * -1.6f;
                sword.transform.rotation = transform.rotation;
            }
            
            sword.transform.position = transform.position + offset;
            
        }
        else
        {
            sword = GameObject.FindWithTag("Sword");
            sword.transform.position = new Vector2(27.5f, -2.7f);
        }


        coinCounter = GameObject.Find("Coin Counter").GetComponent<Text>();
        coinCounter.text = "Coin Counter: " + numCoin;

        if (hasABomb == true)
        {
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
        else
        {
            GameObject bombObject = GameObject.FindWithTag("Bomb");

            if (bombObject != null)
            {
                SpriteRenderer spriteRenderer = bombObject.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = false;
                }
            }
        }

        if (hasAPotion == true)
        {
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
        else
        {
            GameObject potionObject = GameObject.FindWithTag("Potion");

            if (potionObject != null)
            {
                SpriteRenderer spriteRenderer = potionObject.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = false;
                }
            }
        }


        if (hasAScroll == true)
        {
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
        else
        {
            GameObject scrollObject = GameObject.FindWithTag("Scroll");

            if (scrollObject != null)
            {
                SpriteRenderer spriteRenderer = scrollObject.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = false;
                }
            }
        }


        if (hasACrossBow == true)
        {
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
        else
        {
            GameObject crossBowObject = GameObject.FindWithTag("CrossBow");

            if (crossBowObject != null)
            {
                SpriteRenderer spriteRenderer = crossBowObject.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = false;
                }
            }
        }


        if (villager2 == false && villager1 == true && showText2 && currentTextIndex2 <= texts2.Length)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentTextIndex2 == texts2.Length)
                {
                    TextBox[] textBoxes = FindObjectsOfType<TextBox>();

                    foreach (TextBox textBox in textBoxes)
                    {
                        textBox.setEnabled(false);
                    }

                    textbox = GameObject.Find("TextBox").GetComponent<Text>();
                    textbox.text = "";

                    showText2 = false;
                    villager2 = true;
                }
                else
                {
                    textbox = GameObject.Find("TextBox").GetComponent<Text>();
                    textbox.text = texts2[currentTextIndex2];
                    currentTextIndex2++;

                    coinCounter = GameObject.Find("Coin Counter").GetComponent<Text>();
                    coinCounter.text = "Coin Counter: 40";
                    if (addedCoin)
                    {
                        numCoin += 40;
                        addedCoin = false;
                    }
                    
                }
            }
        }

        if (showText2 == true)
        {
            up = false;
            animator.SetBool("up", false);
            down = false;
            animator.SetBool("down", false);
            left = false;
            animator.SetBool("run", false);
            right = false;
            animator.SetBool("run", false);
            return;
        }





        if (villager1 == false && showText && currentTextIndex <= texts.Length)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentTextIndex == texts.Length)
                {
                    TextBox[] textBoxes = FindObjectsOfType<TextBox>();

                    foreach (TextBox textBox in textBoxes)
                    {
                        textBox.setEnabled(false);
                    }

                    textbox = GameObject.Find("TextBox").GetComponent<Text>();
                    textbox.text = "";

                    showText = false;
                    villager1 = true;
                }
                else
                {
                    textbox = GameObject.Find("TextBox").GetComponent<Text>();
                    textbox.text = texts[currentTextIndex];
                    currentTextIndex++;
                }
            }
        }

        if (showText == true)
        {
            up = false;
            animator.SetBool("up", false);
            down = false;
            animator.SetBool("down", false);
            left = false;
            animator.SetBool("run", false);
            right = false;
            animator.SetBool("run", false);
            return;
        }




        if (Input.GetKeyDown(KeyCode.W))
        {
            up = true;
            animator.SetBool("up", true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            down = true;
            animator.SetBool("down", true);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            left = true;
            animator.SetBool("run", true);
            spriteRenderer.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            right = true;
            animator.SetBool("run", true);
            spriteRenderer.flipX = true;
        }


        if (Input.GetKeyUp(KeyCode.W))
        {
            up = false;
            animator.SetBool("up", false);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            down = false;
            animator.SetBool("down", false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            left = false;
            animator.SetBool("run", false);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            right = false;
            animator.SetBool("run", false);
        }
    }

    private void FixedUpdate()
    {
        Vector2 dir = Vector2.zero;

        if (up)
        {
            dir += new Vector2(0, speed * Time.deltaTime);
        }
        if (down)
        {
            dir += new Vector2(0, -speed * Time.deltaTime);
        }
        if (left)
        {
            dir += new Vector2(-speed * Time.deltaTime, 0);
        }
        if (right)
        {
            dir += new Vector2(speed * Time.deltaTime, 0);
        }

        dir = Vector2.ClampMagnitude(dir, speed);

        rb.velocity = dir;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "block")
        {
            SceneManager.LoadScene(0);
            transform.position = new Vector2(17.563f, 4.71275f);
            up = false; down = false; right = false; left = false;
        }

        Bullet[] bullets = FindObjectsOfType<Bullet>();

        foreach (Bullet bullet in bullets)
        {
            if (collision.gameObject == bullet.gameObject)
            {
                hearts--;
                if (hearts == 0)
                {
                    //FINISH
                    hearts = 5;
                    numCoin = 0;
                    SceneManager.LoadScene(0);
                }
                else if (hearts == 1)
                {
                    SetActiveHeart("heart2", false);
                    SetActiveHeart("heart3", false);
                    SetActiveHeart("heart4", false);
                    SetActiveHeart("heart5", false);
                }
                else if (hearts == 2)
                {
                    SetActiveHeart("heart3", false);
                    SetActiveHeart("heart4", false);
                    SetActiveHeart("heart5", false);
                }
                else if (hearts == 3)
                {
                    SetActiveHeart("heart4", false);
                    SetActiveHeart("heart5", false);
                }
                else if (hearts == 4)
                {
                    SetActiveHeart("heart5", false);
                }
            }
                
        }


        Villian[] villains = FindObjectsOfType<Villian>();

        foreach (Villian villain in villains)
        {
            if (collision.gameObject == villain.gameObject)
            {
                hearts--;
                if (hearts == 0)
                {
                    //FINISH
                    hearts = 5;
                    numCoin = 0;
                    SceneManager.LoadScene(0);
                }
                else if (hearts == 1)
                {
                    SetActiveHeart("heart2", false);
                    SetActiveHeart("heart3", false);
                    SetActiveHeart("heart4", false);
                    SetActiveHeart("heart5", false);
                }
                else if (hearts == 2)
                {
                    SetActiveHeart("heart3", false);
                    SetActiveHeart("heart4", false);
                    SetActiveHeart("heart5", false);
                }
                else if (hearts == 3)
                {
                    SetActiveHeart("heart4", false);
                    SetActiveHeart("heart5", false);
                }
                else if (hearts == 4)
                {
                    SetActiveHeart("heart5", false);
                }
            }
        }


        Monster[] monsters = FindObjectsOfType<Monster>();

        foreach (Monster monster in monsters)
        {
            if (collision.gameObject == monster.gameObject)
            {
                hearts--;
                if (hearts == 0)
                {
                    //FINISH
                    hearts = 5;
                    numCoin = 0;
                    SceneManager.LoadScene(0);
                }
                else if (hearts == 1)
                {
                    SetActiveHeart("heart2", false);
                    SetActiveHeart("heart3", false);
                    SetActiveHeart("heart4", false);
                    SetActiveHeart("heart5", false);
                }
                else if (hearts == 2)
                {
                    SetActiveHeart("heart3", false);
                    SetActiveHeart("heart4", false);
                    SetActiveHeart("heart5", false);
                }
                else if (hearts == 3)
                {
                    SetActiveHeart("heart4", false);
                    SetActiveHeart("heart5", false);
                }
                else if (hearts == 4)
                {
                    SetActiveHeart("heart5", false);
                }
            }
        }

        if (collision.gameObject.tag == "trees")
        {
            SceneManager.LoadScene(1);
            transform.position = new Vector2(17.563f, 4.71275f);
            up = false;  down = false; right = false; left = false;

            TextBox[] textBoxes = FindObjectsOfType<TextBox>();

            foreach (TextBox textBox in textBoxes)
            {
                textBox.setEnabled(false);
            }
        }

        if (collision.gameObject.tag == "buildingthin1")
        {
            SceneManager.LoadScene(3);
            transform.position = new Vector2(5.48f, -1.69f);
            up = false; down = false; right = false; left = false;
        }

        if (collision.gameObject.tag == "house")
        {
            SceneManager.LoadScene(2);
            transform.position = new Vector2(5.48f, -1.69f);
            up = false; down = false; right = false; left = false;
        }

        if (collision.gameObject.tag == "buildingthin2")
        {
            SceneManager.LoadScene(4);
            transform.position = new Vector2(5.48f, -1.69f);
            up = false; down = false; right = false; left = false;
        }

        if (collision.gameObject.tag == "Villager1")
        {
            if (villager1 == false)
            {
                TextBox[] textBoxes = FindObjectsOfType<TextBox>();

                foreach (TextBox textBox in textBoxes)
                {
                    textBox.setEnabled(true);
                }

                showText = true;

                textbox = GameObject.Find("TextBox").GetComponent<Text>();
                textbox.text = "Villager: Hello fellow friend! Welcome to Kakariko Village.";
            }
        }

        if (collision.gameObject.tag == "Villager2")
        {
            if (villager1 == true && villager2 == false)
            {
                TextBox[] textBoxes = FindObjectsOfType<TextBox>();

                foreach (TextBox textBox in textBoxes)
                {
                    textBox.setEnabled(true);
                }

                showText2 = true;

                textbox = GameObject.Find("TextBox").GetComponent<Text>();
                textbox.text = "Villager: Hello hero! Thank you for your willingness to help save our village.";
            }
        }

        if (collision.gameObject.tag == "chest")
        {
            Chest chest = collision.gameObject.GetComponent<Chest>();

            chest.openChestAnimate();
            numCoin += 5;

            coinCounter = GameObject.Find("Coin Counter").GetComponent<Text>();
            coinCounter.text = "Coin Counter: " + numCoin;
        }
    }

    public int getNumCoin()
    {
        return numCoin;
    }

    public void setNumCoin(int newNumCoin)
    {
        numCoin += newNumCoin;
    }
}
