using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Transform respawnPoint; //Se pide el transform para usarlo como punto de reaparicion del enemigo
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject visionRange;
    [SerializeField] private int totalHealth = 3; //Salud del enemigo asignada
    [SerializeField] private GameObject hurtBox;
    private GameObject _returnMenu;

    private Animator _animator;
    private Collider2D _collider;
    private PlayerHealth _playerHealth; //Se declara una variable para usar PlayerHealth
    public int _health; //Salud interna del enemigo
    private SpriteRenderer _renderer; //Renderer del enemigo
    private AudioSource _audioSource;

    //Controllers
    private PolicemanController _policemanController;
    private ESMADController _esmadController;
    private TanketteController _tanketteController;
    private PukeController _pukeController;

    public void Awake()
    {
        //Obtener los controllers de cada tipo de enemigo
        _collider = GetComponent<Collider2D>();
        _policemanController = GetComponent<PolicemanController>();
        _esmadController = GetComponent<ESMADController>();
        _tanketteController = GetComponent<TanketteController>();
        _pukeController = GetComponent<PukeController>();

        _audioSource = GetComponent<AudioSource>();

        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>(); //Obtiene el componente Renderer del enemigo
        visionRange = transform.GetChild(0).gameObject; //Obtiene el primer objeto hijo del enemigo(VisionRange)
        _playerHealth = player.GetComponent<PlayerHealth>(); //Obtiene el componente PlayerHealth del jugador
        _returnMenu = GameObject.Find("Throne");
    }
    public void Start()
    {
        _health = totalHealth; //Igual la salud que se le asigna con la salud interna _health
    }

    public void AddDamage(int amount)
    {
        if (gameObject.activeInHierarchy == true)
        {
            _health = _health - amount; //Le resta un valor a la vida del enemigo

            StartCoroutine("VisualFeedBack"); //Se llama a la corrutina VisualFeedBack
            if (_health <= 0) //Si la salud es 0 o menos
            {
                _health = 0;
                StartCoroutine("SetActiveFalse");
                if (gameObject.CompareTag("Puke"))
                {
                    _returnMenu.GetComponentInChildren <GameObject>();
                }
            }
        }
    }

    private void OnEnable() //Cuando el enemigo está activo
    {
        _health = totalHealth;
        if (_playerHealth.getHealth() <= 0) //Si  la vida del jugador es 0 o menos
        {
            gameObject.transform.SetPositionAndRotation(respawnPoint.position, transform.rotation); //Mueve al enemigo a la posicion de reaparicion
        }
    }

    private IEnumerator SetActiveFalse()
    {

        _animator.SetTrigger("death");
        hurtBox.SetActive(false);

        if (gameObject.CompareTag("Policeman"))
        {
            _policemanController.enabled = false;
            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false); //Se desactiva al enemigo
            _policemanController.enabled = true;
            _policemanController._attacking = false;
        }

        if (gameObject.CompareTag("ESMAD"))
        {
            _esmadController.enabled = false;
            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false); //Se desactiva al enemigo
            _esmadController.enabled = true;
        }

        if (gameObject.CompareTag("Tankette"))
        {
            _tanketteController.enabled = false;
            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false); //Se desactiva al enemigo
            _tanketteController.enabled = true;
        }

        if (gameObject.CompareTag("Puke"))
        {
            yield return new WaitForSeconds(0.5f);
            _animator.SetTrigger("finallyDeath");
            _pukeController.enabled = false;
            _audioSource.Stop();
        }
        visionRange.GetComponent<Collider2D>().enabled = true; //Se activa el rango de vision por si se desactivo al momento de desactivar al enemigo
        _renderer.color = Color.white; //Cambia el color del enemigo a blanco por si murio de color rojo
        gameObject.transform.SetPositionAndRotation(respawnPoint.position, transform.rotation); //Mueve al enemigo a la posicion de reaparicion
    }

    private IEnumerator VisualFeedBack()
    {
        _renderer.color = Color.red; //Cambia el color del enemigo a rojo
        yield return new WaitForSeconds(0.1f);
        _renderer.color = Color.white; //Cambia el color del enemigo a blanco
    }

    public void activatePukeController()
    {
        if (gameObject.CompareTag("Puke"))
        {
            gameObject.GetComponent<PukeController>().enabled = true;
        }
    }


}
