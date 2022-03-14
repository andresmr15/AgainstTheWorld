using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 1;
    public Transform respawnPoint;

    private bool _isAttacking;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            _isAttacking = true;
        }
        else
        {
            _isAttacking = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAttacking == true)
        {
            if(collision.CompareTag("Enemy") || collision.CompareTag("Projectile") && gameObject.CompareTag("Player"))
            {
                collision.SendMessageUpwards("AddDamage", damage);
            }
        }

        if (collision.CompareTag("Fall"))
        {
            gameObject.SetActive(false);
            gameObject.transform.SetPositionAndRotation(respawnPoint.position, transform.rotation);
        }
    }
}
