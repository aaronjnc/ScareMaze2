using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    private string WinText;
    [SerializeField]
    private string LoseText;
    [SerializeField]
    private TextMeshProUGUI levelCondition;
    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private GameObject resetButton;
    [SerializeField]
    private GameObject credits;
    public void SetText(bool win)
    {
        levelCondition.text = win ? WinText : LoseText;
        if (SceneManager.GetActiveScene().buildIndex != 9)
        {
            resetButton.SetActive(!win);
            continueButton.SetActive(win);
        }
        else
        {
            resetButton.SetActive(false);
            continueButton.SetActive(false);
            credits.SetActive(true);
        }
    }

    public void ResetLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AdvanceLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
