using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;

    public bool isPlaying = false;

    void Awake()
    {
        i = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReloadScene();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.i.curStage = 0;
            SceneManager.i.StartStage();
            PlayerManager.my.money = 500;
            PlayerManager.my.man_count = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.i.curStage = 1;
            SceneManager.i.StartStage();
            PlayerManager.my.money = 10000;
            PlayerManager.my.man_count = 128;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.i.curStage = 2;
            SceneManager.i.StartStage();
            PlayerManager.my.money = 50000;
            PlayerManager.my.man_count = 1467;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Cursor.visible = !Cursor.visible;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Screen.SetResolution(1080, 1350, FullScreenMode.Windowed);
        }
    }

    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        isPlaying = false;
        Time.timeScale = 0;
    }

    public void Continue()
    {
        isPlaying = true;
        Time.timeScale = 1;
    }
}
