using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoToOptionsScene()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("MenuV2Scene");
    }

    public void GoToGamePlayScene()
    {
        Cursor.visible = false ;
        SceneManager.LoadScene("GamePlayScene");
        Time.timeScale = 1.0f;
    }

    public void GoToStartMenuScene()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("StartMenuScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
