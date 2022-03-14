using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rango_Puke : MonoBehaviour
{
    public Animator _animator;
    public PukeController enemigo;
    public Projectile projectile;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animator.SetBool("attacking", true);
            enemigo.atacando = true;
        }
    }
}
