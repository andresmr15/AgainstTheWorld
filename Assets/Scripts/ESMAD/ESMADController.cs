using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESMADController : MonoBehaviour
{
    private Transform Player;
    private Animator _animator;

    public float RunView;
    public float LineOfSite;//deteccion
    public float speed;
    public bool walking;
    public float HitRange;
    public float fireRate = 1;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        comportamientos();
        if (_animator.GetBool("weak") != true) {

            float distanceFromPlayer = Vector2.Distance(Player.position, transform.position);
            if (distanceFromPlayer < LineOfSite && distanceFromPlayer > HitRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, Player.position, speed * Time.deltaTime);
            }

            Vector3 direction = Player.position - transform.position;
            if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

            float distance = Mathf.Abs(Player.position.x - transform.position.x);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, LineOfSite);
        Gizmos.DrawWireSphere(transform.position, HitRange);
        Gizmos.DrawWireSphere(transform.position, RunView);
    }

    public void comportamientos()
    {
        if (Mathf.Abs((transform.position.x - Player.transform.position.x)) < LineOfSite)
        {
            _animator.SetBool("attacking", false);
            _animator.SetBool("running", false);
            _animator.SetBool("walking", true);
            if (Mathf.Abs((transform.position.x - Player.transform.position.x)) < RunView)
            {
                _animator.SetBool("attacking", false);
                _animator.SetBool("running", true);

                if (Mathf.Abs((transform.position.x - Player.transform.position.x)) < HitRange)
                {
                    _animator.SetBool("attacking", true);

                    StartCoroutine("Weak4Seconds");
                }
            }
        }
        if (Mathf.Abs((transform.position.x - Player.transform.position.x)) > LineOfSite)
        {
            _animator.SetBool("attacking", false);
            _animator.SetBool("running", false);
            _animator.SetBool("walking", false);

        }
    }
    public IEnumerator Weak4Seconds()
    {
        if (_animator.GetBool("attacking") == true){

            _animator.SetBool("weak", true);
            yield return new WaitForSeconds(1.5f);
            _animator.SetBool("weak", false);
        }
    }
}