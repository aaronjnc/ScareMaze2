using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        LoadScene(1);
    }
    public void LoadScene(int i)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(i);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void MainMenu()
    {
        LoadScene(0);
    }
    public void OpenObject(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void CloseObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}
