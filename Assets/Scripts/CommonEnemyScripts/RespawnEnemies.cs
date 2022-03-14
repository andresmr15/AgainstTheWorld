using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemies : MonoBehaviour
{
    private EnemyHealth _enemyHealth;

    public void ActivateEnemies()
    {
        gameObject.SetActive(true);

        foreach (Transform children in this.transform)
        {
            children.gameObject.SetActive(true);
            if (children.gameObject.CompareTag("Puke"))
            {
                _enemyHealth = children.GetComponent<EnemyHealth>();
                children.transform.SetPositionAndRotation(_enemyHealth.respawnPoint.position, transform.rotation); //Mueve al enemigo a la posicion de reaparicion
                children.Find("HurtBox").gameObject.SetActive(true);
            }
             
            foreach (Transform grandson in children.transform)
            {
                if (grandson.gameObject.CompareTag("Policeman") || grandson.gameObject.CompareTag("ESMAD") || grandson.gameObject.CompareTag("Tankette") || grandson.gameObject.CompareTag("Policeman"))
                {
                    grandson.gameObject.SetActive(true);
                    _enemyHealth = grandson.GetComponent<EnemyHealth>();
                    grandson.transform.SetPositionAndRotation(_enemyHealth.respawnPoint.position, transform.rotation); //Mueve al enemigo a la posicion de reaparicion
                    grandson.Find("HurtBox").gameObject.SetActive(true);
                }
            }
        }
    }
}
