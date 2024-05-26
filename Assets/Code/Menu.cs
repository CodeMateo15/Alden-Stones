using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public float highScore;
    public bool startingMenu;
    TextMeshProUGUI StartscoreText;
    public GameObject Startscore_Text;

    TextMeshProUGUI CurrentscoreText;
    public GameObject currentscore_Text;

    public float beforeMenuScore;

    void Awake()
    {
        if (startingMenu == true)
        {
            PlayerPrefs.SetFloat("CurrentScore", 0f);
            highScore = PlayerPrefs.GetFloat("Score");
            StartscoreText = Startscore_Text.GetComponent<TextMeshProUGUI>();
            StartscoreText.text = "High Score:\n" + highScore.ToString();
        }
    }

    void Update()
    {
        if (startingMenu == false)
        {
            CurrentscoreText = currentscore_Text.GetComponent<TextMeshProUGUI>();
            CurrentscoreText.text = "Score:\n" + GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().score.ToString();
        }
    }

    public void QuitMenu()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        beforeMenuScore = PlayerPrefs.GetFloat("Score");
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().score >= beforeMenuScore)
        {
            PlayerPrefs.SetFloat("Score", GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().score);
        }
        Application.Quit();
    }
}
