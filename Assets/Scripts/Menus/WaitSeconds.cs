using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitSeconds : MonoBehaviour
{
    public void Wait()
    {
        StartCoroutine("ShowText");
    }

    public IEnumerator ShowText()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
}
