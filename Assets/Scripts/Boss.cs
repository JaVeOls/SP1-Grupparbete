using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider easeHealthSlider;

    [SerializeField] private Transform firePoint, firePoint1, firePoint2;
    [SerializeField] private Transform healthSpawn, healthSpawn1, healthSpawn2;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private GameObject healthbar;

    [SerializeField] private BoxCollider2D battleStart;
    [SerializeField] private CapsuleCollider2D barrier;

    [SerializeField] private float maxHealth = 150.0f;
    [SerializeField] private float health;
    [SerializeField] private float lerpSpeed = 0.05f;

    private GameObject instHealth;
    private Transform PowerUpSpawn;
    private BoxCollider2D coll;
    private Animator anim;

    private bool inRange;
    public bool inBattle;

    private float lastAttack;
    private float lastSpawn;
    private float cooldown = 0.5f;
    private float spawnCooldown = 3f;
    private float _time;

    private int count = 0;
    private int counted = 1;
    private int waveCount;
    private int spawn;

    //Ideas of what to add
    //
    // - Maybe a start up animation
    // - A way off telling when the phase changes
    // - If so, animation should play in the CollisionEnable function

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        health = maxHealth;
        inRange = false;
        playerMovement.battleStarted = false;
        coll.enabled = false;
        _time = 0f;
    }

    void Update()
    {
        if (playerMovement.battleStarted == false)
        {
            //Battle hasn't started
            inBattle = false;
            barrier.enabled = false;
            healthbar.SetActive(false);
            RestartBattle();
        }

        if(playerMovement.battleStarted == true)
        {
            //Battle has started
            barrier.enabled = true;

            healthbar.SetActive(true);
            inBattle = true;

            //Managing boss health
            if (healthSlider.value != health)
            {
                healthSlider.value = health;
            }

            if (Input.GetKeyDown(KeyCode.E) && inRange == true)
            {
                TakeDamage(5);
            }

            if (healthSlider.value != easeHealthSlider.value)
            {
                easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
            }

            //What battle phase are we in?
            if (counted == 1)
            {
                Phase(1);
            }
            if (counted == 2)
            {
                Phase(2);
            }
            if (counted == 3)
            {
                Phase(3);
            }

            //if the boss can be hit
            if (coll.enabled == true)
            {
                //if the boss has taken 1/3 of damage
                if (health == 100 && count == 1)
                {
                    CollisionDissable();
                    StartCoroutine(CollisionEnable(2f, 2));
                }

                //if the boss has taken 2/3 of damage
                if (health == 50 && count == 2)
                {
                    CollisionDissable();
                    StartCoroutine(CollisionEnable(2f, 3));
                }

                //if the boss has taken 3/3 of damage
                if (health == 0 && healthSlider.value == 0 && count == 3)
                {
                    Death();
                }
            }

            //if the players health is under 4, start spawning heals
            if(playerMovement.currentHealth < 4 && playerMovement.currentHealth > 0)
            {
                SpawningHealth();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Can the player hit the boss? Yes
            inRange = true;  
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Can the player hit the boss? No
            inRange = false;
        }
    }

    private void SpawningHealth()
    {
        //Generates a random number between 0 and 2
        //Which then will determine the spawn postion of the health power-up
        int ranHealthPosition = Random.Range(0, 3);

        if (ranHealthPosition == 0)
        {
            //if 0 = spawn in the first spawn position
            PowerUpSpawn = healthSpawn;
        }

        if (ranHealthPosition == 1)
        {
            //if 1 = spawn in the second spawn position
            PowerUpSpawn = healthSpawn1;
        }

        if (ranHealthPosition == 2)
        {
            //if 2 = spawn in the third spawn position
            PowerUpSpawn = healthSpawn2;
        }
        //Spawns the power-up
        CreatPowerUp(healthSpawn);
    }

    private void CreatPowerUp(Transform spawn)
    {
        //Making sure the power-up spawns every amount of seconds the spawnCooldown equals
        if (Time.time - lastSpawn < spawnCooldown)
        {
            return;
        }
        lastSpawn = Time.time;

        //Making sure that power-ups can't stack
        //by making sure every spawn +osition only has one power-up as a child
        if(spawn.childCount == 0)
        {
            //Creating the power-up
            instHealth = Instantiate(healthPrefab, spawn.position, spawn.rotation);
            //Setting it as a parent to the spawn position
            instHealth.transform.SetParent(spawn);
            //Make it dissapear within 4 seconds
            Destroy(instHealth, 4f);
        }
        //if the spawn position has more than 0 cildren, don't spawn
        if(spawn.childCount > 0)
        {
            return;
        }

    }

    private void TakeDamage(float damage)
    {
        //Making sure the player can't spam click the attack button
        if (Time.time - lastAttack < cooldown)
        {
            return;
        }
        lastAttack = Time.time;
        //Boss takes damage
        anim.SetTrigger("TakeHit");
        anim.SetBool("ReadyHit", false);
        health -= damage;
    }

    private void Shoot(Transform pointOfFrie)
    {
        //Create the projectile
        Instantiate(projectilePrefab, pointOfFrie.position, pointOfFrie.rotation).transform.SetParent(pointOfFrie);
    }
    private void Attack()
    {
        //Spawn the projectile in a randomly generated speed
        StartCoroutine(AttackDelay(firePoint2, Random.Range(0, 0.5f)));
        StartCoroutine(AttackDelay(firePoint1, Random.Range(0, 0.5f)));
        StartCoroutine(AttackDelay(firePoint, Random.Range(0, 0.5f)));
        //Add attack animation
    }

    IEnumerator AttackDelay(Transform pointOfFrie, float speed)
    {
        //Ctreating the speed delays
        yield return new WaitForSeconds(speed);
        Shoot(pointOfFrie);
    }

    private void Phase(int phaseCount)
    {
        //Creating the different phases
        //Speeding up the attacks in each new wave
        if (health <= maxHealth && health > 100 && phaseCount == 1)
        {
            count = 1;
            Wave(4f);
        }

        if (health <= 100 && health > 50 && phaseCount == 2)
        {
            count = 2;
            Wave(2f);
        }

        if (health <= 50 && health >= 0 && phaseCount == 3)
        {
            count = 3;
            Wave(1f);
        }
    }

    private void Wave(float _interval)
    {
        //Spawn new waves as long as the boss can't be attacked
        if(coll.enabled == false)
        {
            _time += Time.deltaTime;
            while (_time >= _interval && waveCount <= 2)
            {
                Attack();
                anim.SetTrigger("Attack");
                _time -= _interval;
                waveCount++;
            }
        }
        
        //When the waves are done the player can attack the boss
        if(waveCount == 2)
        {
            StartCoroutine(WaveState(4));
        }
    }

    IEnumerator WaveState(float time)
    {
        //Wait time amount of seconds before enabling the boss's collider
        yield return new WaitForSeconds(time);
        coll.enabled = true;
        anim.SetBool("ReadyHit", true);
        //Debug.Log("ENABLE");
        yield return new WaitForSeconds(time);
        //Wait time amount of seconds before dissabling the boss's collider
        coll.enabled = false;
        anim.SetBool("ReadyHit", false);
        //Debug.Log("DISSABLE");
        //Restart the waves
        waveCount = 0;
    }

    IEnumerator CollisionEnable(float time, int nextLevel)
    {
        //Continue to the next phase
        yield return new WaitForSeconds(time);
        coll.enabled = true;
        counted = nextLevel;
        anim.SetBool("ReadyHit", false);
        //Debug.Log("ENABLE");
    }
    private void CollisionDissable()
    {
        //Disable the collider
        coll.enabled = false;
        //Debug.Log("DISSABLE");
    }

    private void Death()
    {
        //What happens when the boss dies
        healthSlider.value = 0f;
        easeHealthSlider.value = 0f;
        playerMovement.battleStarted = false;
        Destroy(gameObject, 0.4f);
        healthbar.SetActive(false);
        anim.SetTrigger("Death");
    }

    private void RestartBattle()
    {
        //Restarts the battle
        playerMovement.battleStarted = false;
        inBattle = false;
        battleStart.enabled = true;
        health = maxHealth;
        counted = 1;
        count = 0;
        waveCount = 0;
        _time = 0f;
    }

}
