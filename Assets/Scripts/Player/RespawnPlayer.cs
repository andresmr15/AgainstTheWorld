using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;

    public void Respawn()
    {
        gameObject.transform.SetPositionAndRotation(respawnPoint.position, transform.rotation);
    }
}
