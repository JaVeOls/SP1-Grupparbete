using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed2 = 2.0f;
    //[SerializeField] private float bounciness2 = 100;
    [SerializeField] private float knockbackForce2 = 200f;
    [SerializeField] private float upwardForce2 = 100f;
    [SerializeField] private int damageGiven2 = 1;
    [SerializeField] private GameObject player; 


    private SpriteRenderer rend2;
    private Animator anim2;
    private Animator playAnim2;

    private bool canMove2 = true;

    private bool inRange2;

    private void Start()
    {
        rend2 = GetComponent<SpriteRenderer>();
        anim2 = GetComponent<Animator>();
        playAnim2 = player.GetComponent<Animator>();


    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange2 == true)
        {

            TakeDamage2();
            playAnim2.SetTrigger("Attack");
        }
    }


    void FixedUpdate()
    {
        if (!canMove2)
            return;
        transform.Translate(new Vector2(moveSpeed2, 0) * Time.deltaTime);

        if (moveSpeed2 < 0)
        {
            rend2.flipX = false;  //
        }

        if (moveSpeed2 > 0)
        {
            rend2.flipX = true;  //
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock"))
        {
            moveSpeed2 = -moveSpeed2;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed2 = -moveSpeed2;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageGiven2);


            if (other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockBack(knockbackForce2, upwardForce2);
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockBack(-knockbackForce2, upwardForce2);
            }
        }
    }



    private void TakeDamage2()
    {
        anim2.SetTrigger("Hit");

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        // GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        Destroy(gameObject, 0.8f);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            inRange2 = true;
            Debug.Log("inRange");

            canMove2 = false;


        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange2 = false;
        }
    }
}

