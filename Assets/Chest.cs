using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    private Animator animator;
    private bool isOpened = false;
    private float timer = 0f;
    private bool destroyAfterAnimation = false;
    SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    Text textbox;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("IN SPACE METHOD");
            TextBox[] textBoxes = FindObjectsOfType<TextBox>();

            foreach (TextBox textBox in textBoxes)
            {
                Debug.Log("IN FOR EACH");
                textBox.setEnabled(false);
            }

            textbox = GameObject.Find("TextBox").GetComponent<Text>();
            textbox.text = "";
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }

        if (destroyAfterAnimation)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                destroyAfterAnimation = false;

                spriteRenderer.enabled = false;
                boxCollider.enabled = false;

                TextBox[] textBoxes = FindObjectsOfType<TextBox>();

                foreach (TextBox textBox in textBoxes)
                {
                    textBox.setEnabled(true);
                }

                textbox = GameObject.Find("TextBox").GetComponent<Text>();
                textbox.text = "You got 5 more coins";
            }
        }
    }

    public void openChestAnimate()
    {
        if (!isOpened)
        {
            isOpened = true;
            animator.SetBool("openChest", true);
            destroyAfterAnimation = true;
        }
    }
}
