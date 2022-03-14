using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hurt"))
        {
            //Busca el metodo en los hijos y si no lo encuentra, busca en el padre. Y le pasa los argumentos dentro del parentesis
            collision.SendMessageUpwards("AddDamage", damage);
        }
    }
}
