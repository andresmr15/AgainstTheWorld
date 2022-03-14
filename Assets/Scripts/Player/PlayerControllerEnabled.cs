using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEnabled : MonoBehaviour
{
    //Activate
    public void activateController()
    {
        StartCoroutine("WaitAndActivate");
    }

    private IEnumerator WaitAndActivate()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<PlayerController>().enabled = true;
    }
}
