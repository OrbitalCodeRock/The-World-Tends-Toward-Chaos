using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject GameManager;
    public GameObject overlay;
    // Start is called before the first frame update
    public void LoadMainMenu()
    {
        GameManager = GameObject.Find("GameManager");
        if (GameManager != null) { Destroy(GameManager); }
        SceneManager.LoadScene(0);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void RestartGame()
    {
        GameManager = GameObject.Find("GameManager");
        if (GameManager != null) { Destroy(GameManager); }
        SceneManager.LoadScene(1);
    }
    public void openOverlay()
    {
        overlay.SetActive(true);
    }
    public void closeOverlay()
    {
        overlay.SetActive(false);
    }
    public void quitGame()
    {
        Application.Quit();
    }

}
