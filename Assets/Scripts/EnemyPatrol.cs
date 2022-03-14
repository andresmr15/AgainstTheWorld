using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public float _time;
    public float waitingTime;
    
    private int _direction;
    private int _rutine;
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine("RutineEvery4Sec");
    }

    void Update()
    {
        Behaviours();
    }

    public void Behaviours()
    {
        if (_rutine == 0)
        {
            _animator.SetBool("Idle", true);
        }
        else
        {
            if (_direction == 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(Vector3.right * speed * Time.deltaTime);

            }
            _animator.SetBool("Idle", false);
        }
    }

    public IEnumerator RutineEvery4Sec()
    {
        _rutine = Random.Range(0, 2);
        _direction = Random.Range(0, 2);

        transform.Translate(0, 0, 0);
        _animator.SetBool("Idle", true);

        yield return new WaitForSeconds(waitingTime);

        StartCoroutine("RutineEvery4Sec");
    }

}