﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private DialogUI dialogUI;

    [SerializeField] GameObject itemWallet;
    [SerializeField] GameObject itemKey;
    [SerializeField] GameObject itemPhone;

    public int maxHealth = 8;
    public int currentHealth;
    public Rigidbody2D SpawnPoint;
    public DialogUI DialogUI => dialogUI;

    public IInteractable Interactable { get; set; }

    public int HealTimes = 3;
    public bool canHeal;
    public HealthBar healthBar;

    public float moveSpeed = 3f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //movement = new Vector2(directionX, directionY).normalized;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {

        if (!dialogUI.IsOpen)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        //rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);


        if (Input.GetKeyDown(KeyCode.E)) // INTERACTABLE
        {
            if (Interactable != null)
            {
                Interactable.Interact(this);
            }
        }


    }

    //COLLISION CHECK
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wallet")
        {
            itemWallet.SetActive(true);
        }

        if (collision.tag == "Key")
        {
            itemKey.SetActive(true);
        }

        if (collision.tag == "Phone")
        {
            itemPhone.SetActive(true);
        }
    }

    //PLAYER HIT HEALTH 
    public void PlayerHit(int _damage)
    {
        currentHealth -= _damage;
        Debug.Log("HP: " + currentHealth);
        healthBar.SetHealth(currentHealth);
        //playerAnim.SetTrigger("Damage");

        if (currentHealth <= 0)
        {
            //playerAnim.SetTrigger("Die");
            GameOver();
        }
    }

    public void Heal()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    //COLLISION CHECK
    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //if (collision.tag == "Enemy")
    //{
    //  SceneManager.LoadScene("DEMO");
    // }

    //}

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    public bool GameOverYesNo;
    public void GameOver()
    {
        SceneManager.LoadScene("GAME OVER");
    }

    public void MoveSpawn()
    {
        rb.MovePosition(SpawnPoint.position);
        Debug.Log("Moved");
    }

}
