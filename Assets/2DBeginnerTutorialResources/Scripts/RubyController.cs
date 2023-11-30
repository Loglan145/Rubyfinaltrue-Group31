using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class RubyController : MonoBehaviour
{
    
    public float speed = 3.0f;
    public float timeInvincible = 2.0f;
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public bool gameOver; 
    public GameObject projectilePrefab;
    public AudioClip throwCogClip;
    public AudioClip playerHit;
    public AudioClip Win;
    public AudioClip Lose;
    public AudioClip textSound;
    public GameObject DamageParticlePrefab;
    int currentHealth;
    public bool isInvincible;
    float invincibleTimer;
    public bool soundplayed;
    Vector2 lookDirection = new Vector2(1, 0);
    Rigidbody2D rigidbody2d;
    Animator animator;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        currentHealth = maxHealth;


        score = 0;

        soundplayed = false;

        {
            gameOver = false;
        }


        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2d.position;
        position = position + move * speed * Time.fixedDeltaTime;

        rigidbody2d.MovePosition(position);

    } 
    
    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.5f, lookDirection, 2.0f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if(character != null)
                {
                    character.DisplayDialog();
                    audioSource.PlayOneShot(textSound);
                }
            }
        }

        if (currentHealth == 0) 
        {
            gameOver = true;

            speed = 0;

            if (!soundplayed)
            {
                GetComponent<AudioSource>().PlayOneShot(Lose);
                soundplayed = true;
            }
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "You lost! Press R to Restart!";
           
        }
        if (score == 2)
        {
            if (!soundplayed)
            {
                GetComponent<AudioSource>().PlayOneShot(Win);
                soundplayed = true;
            }
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "You win! Game Created by Group 31";
            
        }
        if (Input.GetKey(KeyCode.R))

        {

            if (gameOver == true) 

            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // this loads the currently active scene

            }

        }

    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            Instantiate(DamageParticlePrefab, transform.position, Quaternion.identity);

            animator.SetTrigger("Hit");
            audioSource.PlayOneShot(playerHit);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    
    public void ChangeScore(int scoreAmount)
    {
        score += scoreAmount;
        scoreText.text = "Fixed Robots:" + score;
    }

    public void ChangeSpeed(float speedAmount)
    {
        speed += speedAmount;

    }


    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        
        audioSource.PlayOneShot(throwCogClip);
        animator.SetTrigger("Launch");
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
