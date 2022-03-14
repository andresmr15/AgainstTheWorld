using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PukeController : MonoBehaviour
{
    public float startTimeBtwShots;
    public float LineOfSound;
    private float timeBtwShots;

    private Animator _animator;
    public float Rango_Ataque;
    public GameObject shot;
    private Transform player;
    private Transform firePoint;
    public GameObject rango;

    public bool atacando;
    public PukeController enemigo;

    public GameObject proyectile;
    private GameObject _player;
    private AudioSource _audioSource;

    private void Awake()
    {
        firePoint = transform.Find("Point");
        _player = GameObject.FindGameObjectWithTag("Player");
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        timeBtwShots = startTimeBtwShots;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        comportamientos();
        Sound();
        Disparar();
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, LineOfSound);
    }

    public void Sound()
    {
        if (Mathf.Abs(transform.position.x - player.transform.position.x) > LineOfSound)
        {
            _audioSource.Play();
        }
    }

    public void comportamientos()
    {
        if (Mathf.Abs((transform.position.x - player.transform.position.x)) > Rango_Ataque)
        {
            _animator.SetBool("attacking", false);
        }

        if (Mathf.Abs(transform.position.x - player.transform.position.x) > Rango_Ataque)
        {
            _animator.SetBool("attacking", false);
        }

        if (Mathf.Abs(transform.position.x - player.transform.position.x) < Rango_Ataque)
        {
            _animator.SetBool("attacking", true);
        }

    }
    public void Disparar()
    {
        if (_animator.GetBool("attacking") == true)
        {
            if (timeBtwShots <= 0)
            {
                Instantiate(proyectile, firePoint.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;

            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }
}
