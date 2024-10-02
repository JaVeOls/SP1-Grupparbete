using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private AudioClip pickupSound, healthSound, playerHitSound, enemyHitSound, jumpSounds;
    //[SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private GameObject smallParticles, dustParticles;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillColor;
    [SerializeField] private Color greenHealth, redHealth;
    [SerializeField] private TMP_Text cherriesText;

    private float horizontalValue;
    private float rayDistance = 0.25f;
    private bool isGrounded;
    private bool canMove;
    public bool battleStarted;
    private int startingHealth = 5;
    private int currentHealth = 0;
    public int cherriesCollected = 0;
    
    private Rigidbody2D rb;
    private SpriteRenderer rend;
    private Animator anim;
    private AudioSource audioSource;

    public Vector2 checkpointPos;

    void Start()
    {
        checkpointPos = transform.position;

        canMove = true;
        currentHealth = startingHealth;
        cherriesText.text = "" + cherriesCollected;

        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");

        if(horizontalValue < 0)
        {
            FlipSprite(true);
        }

        if(horizontalValue > 0)
        {
            FlipSprite(false);
        }

        if(Input.GetButtonDown("Jump") && CheckIfGrounded() == true)
        {
            Jump();
        }

        anim.SetFloat("MoveSpeed", Mathf.Abs (rb.velocity.x));
        anim.SetFloat("VerticalSpeed", rb.velocity.y);
        anim.SetBool("IsGrounded", CheckIfGrounded());
    }

    private void FixedUpdate()
    {
        if(!canMove)
        {
            return;
        }

        rb.velocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Cherry"))
        {
            Destroy(other.gameObject);
            cherriesCollected++;
            cherriesText.text = "" + cherriesCollected;
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(pickupSound, 0.5f);
            Instantiate(smallParticles, other.transform.position, Quaternion.identity);
        }

        if(other.CompareTag("Health"))
        {
            RestoreHealth(other.gameObject);
        }

        if (other.CompareTag("Projectile"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("BattleStart"))
        {
            battleStarted = true;
            other.enabled = false;
        }

    }

    private void FlipSprite(bool direction)
    {
        rend.flipX = direction;
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));
        //int randomValue = Random.Range(0, jumpSounds.Length);
        //audioSource.PlayOneShot(jumpSounds[randomValue], 0.5f);
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(jumpSounds, 0.5f);
        Instantiate(dustParticles, transform.position, dustParticles.transform.localRotation);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(playerHitSound, 0.5f);
        UpdateHealthBar();

        //print(currentHealth);

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    public void TakeKnockBack(float knockbackForce, float upwards)
    {
        canMove = false;
        rb.AddForce(new Vector2(knockbackForce, upwards));
        Invoke("CanMoveAgain", 0.25f);
    }

    private void CanMoveAgain()
    {
        canMove = true;
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    private void Respawn()
    {
        currentHealth = startingHealth;
        UpdateHealthBar();
        transform.position = spawnPosition.position;
        rb.velocity = Vector2.zero;
    }

    private void RestoreHealth(GameObject healthPickup)
    {
        if(currentHealth >= startingHealth)
        {
            return;
        }
        else
        {   
            currentHealth += 3;
            UpdateHealthBar();
            Destroy (healthPickup);

            if (currentHealth >= startingHealth)
            {
                currentHealth = startingHealth;
            }
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;

        if(currentHealth >= 1)
        {
            fillColor.color = greenHealth;
        }
        else
        {
            fillColor.color = redHealth;
        }
    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, whatIsGround);

        //Debug.DrawRay(leftFoot.position, Vector2.down * rayDistance, Color.blue, 0.25f);
        //Debug.DrawRay(rightFoot.position, Vector2.down * rayDistance, Color.red, 0.25f);

        if(leftHit.collider != null && leftHit.collider.CompareTag("Ground") || rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
