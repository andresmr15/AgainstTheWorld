using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanketteController : MonoBehaviour
{
    private Transform Player1;
    private Animator _animator;
    private float nextFireTime;

    public float LineOfSite;
    public float speed;
    public GameObject Projectile;
    public GameObject ProjectileParent;
    public float ShootingRange;
    public float fireRate = 1;

    private void Start()
    {
        Player1 = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        comportamientos();

        float distanceFromPlayer = Vector2.Distance(Player1.position, transform.position);
        if (distanceFromPlayer < LineOfSite && distanceFromPlayer > ShootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Player1.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= ShootingRange && nextFireTime < Time.time)
        {
            Instantiate(Projectile, ProjectileParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }

        Vector3 direction = Player1.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(Player1.position.x - transform.position.x);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, LineOfSite);
        Gizmos.DrawWireSphere(transform.position, ShootingRange);

    }

    public void comportamientos()
    {
        if (Mathf.Abs((transform.position.x - Player1.transform.position.x)) > LineOfSite)//Si NO entra a rango que no haga ninguno
        {
            _animator.SetBool("Attacking", false);
            _animator.SetBool("Driving", false);
        }
        if (Mathf.Abs((transform.position.x - Player1.transform.position.x)) < LineOfSite) //Si es - del rango que SI haga la acc true
        {
            _animator.SetBool("Attacking", false);
            _animator.SetBool("Driving", true);
            if (Mathf.Abs((transform.position.x - Player1.transform.position.x)) < ShootingRange)//
            {
                _animator.SetBool("Attacking", true);
                _animator.SetBool("Driving", false);
            }
            if (Mathf.Abs((transform.position.x - Player1.transform.position.x)) > ShootingRange)
            {
                _animator.SetBool("Attacking", false);
                _animator.SetBool("Driving", true);
            }

        }
    }
}