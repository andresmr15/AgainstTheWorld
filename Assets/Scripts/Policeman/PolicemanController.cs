using UnityEngine;

public class PolicemanController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float visionRange;
    public float attackRange;
    public bool _attacking;
    public GameObject _hit;
    public GameObject _visionRange;

    private int _rutine;
    private float _time;
    private Animator _animator;
    private int _direction;
    private GameObject _target;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _target = GameObject.Find("Player");
    }

    public void FinishAnimation()
    {
        _animator.SetBool("attacking", false);
        _attacking = false;
        _visionRange.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponTrue()
    {
        _hit.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void ColliderWeaponFalse()
    {
        _hit.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Update()
    {
        Vector3 position = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
        transform.localPosition = position;
        Behaviours();
    }
    public void Behaviours()
    {

        if (Mathf.Abs(transform.position.x - _target.transform.position.x) > visionRange && _attacking == false)
        {
            _animator.SetBool("walking", false);
            _time += 1 * Time.deltaTime;
            if (_time >= 4)
            {
                _rutine = Random.Range(0, 2);
                _time = 0;
            }

            switch (_rutine)
            {
                case 0:
                    _animator.SetBool("walking", false); // cancelar la animacion de caminar
                    break;

                case 1:
                    _direction = Random.Range(0, 2);
                    _rutine++;
                    break;

                case 2:
                    switch (_direction)
                    {
                        case 0:
                            transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            break;

                        case 1:
                            transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            break;
                    }
                    _animator.SetBool("walking", true);
                    break;
            }
        }
        else
        {
            if (Mathf.Abs(transform.position.x - _target.transform.position.x) > attackRange && _attacking == false)
            {
                if (transform.position.x < _target.transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    _animator.SetBool("walking", true);
                    _animator.SetBool("attacking", false);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    _animator.SetBool("walking", true);
                    _animator.SetBool("attacking", false);
                }
            }
            else
            {
                if (_attacking == false)
                {
                    if (transform.position.x < _target.transform.position.x)
                    {
                        transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    _animator.SetBool("walking", true);
                }
            }

        }

    }
    public void OnDisable()
    {
        _attacking = false;
        _visionRange.GetComponent<Collider2D>().enabled = true;
        _hit.GetComponent<Collider2D>().enabled = false;
    }
}
