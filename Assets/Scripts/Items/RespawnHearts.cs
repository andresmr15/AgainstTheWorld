using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnHearts : MonoBehaviour
{
    public void Respawn()
    {
        gameObject.SetActive(true);

        foreach(Transform children in this.transform)
        {
            children.gameObject.SetActive(true);
        }
    }
}
