using UnityEngine;


public class PolicemanRange : MonoBehaviour
{
    public Animator _animator;
    public PolicemanController enemigo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animator.SetBool("walking", false);
            _animator.SetTrigger("attacking");
            enemigo._attacking = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}