using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    GameObject player;
    public GameObject pauseMenuUI;
    public GameObject fpsCamera;
    private AudioSource[] allAudioSources;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        player = GameObject.Find("Player");

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        player.GetComponent<Player_Controller>().movementLock = false;
        player.GetComponent<Player_Controller>().mouseLock = false;

        toggleMouse();
    }

    public void Pause()
    {
        player = GameObject.Find("Player");

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        player.GetComponent<Player_Controller>().movementLock = true;
        player.GetComponent<Player_Controller>().mouseLock = true;

        toggleMouse();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    private void toggleMouse()
    {
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


}
