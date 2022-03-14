using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool _isPaused;

    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        _isPaused = false;
    }

    private void Update()
    {
        Pause();
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isPaused == false)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            _isPaused = true;
            gameObject.GetComponent<PlayerController>().enabled = false;
            gameObject.GetComponent<PlayerController>().audioS.clip = null;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPaused == true)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            _isPaused = false;
            gameObject.GetComponent<PlayerController>().enabled = true;
        }
    }

    public void NormalTime()
    {
        Time.timeScale = 1;
    }

}
