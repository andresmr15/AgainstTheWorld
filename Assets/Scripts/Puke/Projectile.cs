using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed; //Velocidad del proyectil
    [SerializeField] private int enemyDamage = 1;
    [SerializeField] private int playerDamage = 1;
    [SerializeField] private Transform groundCheck; //Punto para cuando el proyectil toque un collider con capa "Groung"
    [SerializeField] private LayerMask groundLayer; //LayerMask para obtener la capa "Ground"
    [SerializeField] private float groundCheckRadius; //Radio de las particulas que se envian desde el proyectil

    private Transform _player; //Componente para obtener la posicion del jugador
    private Transform _puke; //Componente para obtener la posicion de Puke
    private EnemyHealth _pukeHealth;
    private Vector2 _targetPlayer; //Vector de dos dimensiones para obtener la posicion (x,y) del jugador
    private Vector2 _targetPuke; //Vector de dos dimensiones para obtener la posicion (x,y) del Puke
    private bool _isGrounded; //Booleano para saber si el projectil esta tocando el piso
    private GameObject _pukeObject; //Variable para obtener al objeto "Puke" y sus componentes
    private bool  _returning; //Booleano para saber si el jugador golpeo al proyectil

    public void Awake()
    {
        _pukeObject = GameObject.Find("Puke"); //Se obtiene el  objeto "Puke"
        _pukeHealth = _pukeObject.GetComponent<EnemyHealth>();
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform; //Se obtiene el componente transform del objeto con tang "Player"

        _targetPlayer = new Vector2(_player.position.x,_player.position.y); //Se guarda la posicion del jugador

        _puke = GameObject.FindGameObjectWithTag("Puke").transform; //Se obtiene el componente transform del objeto con tang "Puke"

        _targetPuke = new Vector2(_puke.position.x, _puke.position.y); //Se guarda la posicion de Puke
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); //Se utiliza raycast con circunferencias con radio groundCheckRadius

        if (_isGrounded || _pukeObject.activeInHierarchy == false) //Si toca el piso o Puke est[a inactivo
        {
            DestroyProjectile();
        }

        if (_returning == false) //Si el jugador no golpeo el proyectil
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPlayer, speed * Time.deltaTime);
        }
        else //Si el jugador golpeo el proyectil
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPuke, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Si interactua con un collider
    {
        if (collision.CompareTag("Ground")) //Si el tag del objeto es "Ground"
        {
            DestroyProjectile();
        }

        if (collision.CompareTag("Bat")) //Si el jugador golpea 
        {
            _returning = true;
        }
       
        if (_returning == false && collision.CompareTag("Hurt") ) //Si el jugador no golpeó al proyectil y este lo alcanza, le hace daño
        {
            collision.SendMessageUpwards("AddDamage", enemyDamage); //Le dice al metodo AddDamage que le haga daño al jugador
            DestroyProjectile();
        }
        if (_returning == true && collision.CompareTag("Enemy")) //Si el jugador golpeó al proyectil, le hace daño al enemigo
        {
            collision.SendMessageUpwards("AddDamage", playerDamage); //Le dice al metodo AddDamage que le haga daño al enemigo
            DestroyProjectile();
        }
    }
    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
