
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.5f; //Velocidad del jugador
    public float jumpForce = 2.5f; //Fuerza del salto del jugador

    public Transform groundCheck; //Punto para cuando el proyectil toque un collider con capa "Groung"
    public LayerMask groundLayer; //LayerMask para obtener la capa "Ground"
    public float groundCheckRadius; //Radio de las particulas que se envian desde el proyectil

    //Referencias
    private Rigidbody2D _rigidbody; //Variable para usar el Rigid Body del jugador
    private Animator _animator; //Variable para usar el animator del jugador

    //Movimiento
    public Vector2 _movement; // Vector para guardar el movimiento en x del jugador (-1, 0 ó 1)
    private bool _facingRight = true; //Boolean para ver si el jugador mira a la derecha o a la izquierda
    public bool _isGrounded; //Boolean para saber si el jugador está en el piso

    //Ataque
    public bool _isAttacking; //Boolean para saber si el jugador está atacando
    public int _combo; // Entero para saber el combo de ataque actual
    public AudioSource audioS; // Para permitirle escuchar lso sonidos de los ataques del jugador al audiolister de la camara
    public AudioClip[] sound; // Arreglo para guardar los sonidos del ataque


    void Awake()
    {
        _animator = GetComponent<Animator>(); //Se obtiene el componente Animator del jugador
        _rigidbody = GetComponent<Rigidbody2D>(); //Se obtiene el componente Rigidbody del jugador
        audioS = GetComponent<AudioSource>(); // Se obtiene el componente AudioSource del jugador
    }

    void FixedUpdate()
    {
        if (_isAttacking == false)
        {
            float horizontalVelocity = _movement.normalized.x * speed; //Se calcula la velocidad a la que se mueve el jugador (-1, 0, 1)
            _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y); //Se mueve al jugador con la velocidad obtenida mediante el rigidbody
        }
    }

    void Update()
    {
        if (_isAttacking == false)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal"); //Obtiene el input del teclado (w,a,s,d) o (flechas)
            _movement = new Vector2(horizontalInput, 0f); //Se mueve el objeto a una velocidad asignada, solo en el eje x

            if (horizontalInput < 0f && _facingRight == true) // El jugador intenta ir a la izquierda y esta mirando hacia la derecha ?
            {
                Flip();
            }
            else if (horizontalInput > 0f && _facingRight == false) // El jugador intenta ir a la derecha y esta mirando hacia la izquierda ?
            {
                Flip();
            }
        }
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); // Se utiliza un raycast para saber si el jugador está tocando el piso

        if (Input.GetButtonDown("Jump") && _isGrounded == true && _isAttacking == false) //Si se presiona el espacio, si está en el piso y si no está atacando
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //Se utiliza el rigidbody para producir una fuerza de impuslo al jugador
        }

        // Se llama al metodo Combos
        Combos();
        
    }

    private void LateUpdate() //Al finalizar cada cuadro:
    {
        //Se restablecen los parametros del animator del jugador
        _animator.SetBool("Idle", _movement == Vector2.zero);
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);
    }

    private void Flip()
    {
        _facingRight = !_facingRight; //Invierte la direccion hacia la que mira el jugador
        float localScaleX = transform.localScale.x; //Se obtiene la escala del componente TRansform del jugador
        localScaleX = localScaleX * -1f; //Girá al personaje sin importar si su escala en x es =1 o a =-1
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);// Se le aplica el valor obtenido en la linea anterior
    }

    public void Combos()
    {
        if (Input.GetButtonDown("Fire1") && _isGrounded == true && _isAttacking == false) //Si se presiona el click izquierdo, el jugador está en el piso y no está atacando
        {
            //Inmoviliza al jugador ***********
            _movement = Vector2.zero;
            _rigidbody.velocity = Vector2.zero; 
            //*********************************

            _isAttacking = true;
            _animator.SetTrigger("Attack" + _combo); //Cambia a un combo diferente dependiendo de la variable _combo
            audioS.clip = sound[_combo]; ////Cambia a un sonido diferente dependiendo de la variable _combo
            audioS.Play(); // Reproduce el sonido actual
        }
        
    }

    public void StartCombo()
    {
        _isAttacking = false;
        if (_combo < 3) //El combo máximo es 3
        {
            _combo++; //Se incremente _combo
        }
    }

    public void FinishCombo()
    {
        _isAttacking = false;
        _combo = 0; //Se reinicia el combo
    }
}



