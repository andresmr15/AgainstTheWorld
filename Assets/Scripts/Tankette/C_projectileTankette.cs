using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_projectileTankette : MonoBehaviour
{
    public float speed;
    public int enemyDamage = 1;
    public int playerDamage = 1;
    private Transform player;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;


    private Transform tankette;
    private Vector2 targetPlayer;
    private Vector2 targetTankette;
    private bool _isGrounded;
    private GameObject tanketteObject;
    private bool  _returning;


    public void Awake()
    {
        tanketteObject = GameObject.Find("Tankette");
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        targetPlayer = new Vector2(player.position.x,player.position.y);

        tankette = GameObject.FindGameObjectWithTag("Tankette").transform;

        targetTankette = new Vector2(tankette.position.x, tankette.position.y); 
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (_isGrounded || tanketteObject.activeInHierarchy == false)
        {
            DestroyProjectile();
        }

        if (_returning == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetTankette, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tanketteObject.activeInHierarchy == true) {
            if (collision.CompareTag("Ground"))
            {
                DestroyProjectile();
            }

            if (collision.CompareTag("Bat")) {
                _returning = true;
            }

            if (_returning == false && collision.CompareTag("Hurt"))
            {
                collision.SendMessageUpwards("AddDamage", enemyDamage);
                DestroyProjectile();
            }
            if (_returning == true && collision.CompareTag("Enemy"))
            {
                collision.SendMessageUpwards("AddDamage", playerDamage);
                DestroyProjectile();
            }
        }
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
