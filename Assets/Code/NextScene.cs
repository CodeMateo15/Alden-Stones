using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    
    public float transitionTime = 1f;
    private LevelLoader level;
    private int currentSceneIndex;

    void Start()
    {
        level = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetFloat("CurrentScore", GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().score);
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(3));
        var game = GameSettings.Instance;
        game.LoadNextScene();
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        level.transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        level.sliderTransition.SetTrigger("LoadStart");

        yield return new WaitForSeconds(0.5f);

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);

        yield return new WaitForSeconds(transitionTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        level.loadingScreen.SetActive(true);

        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            level.slider.value = progress;
            level.progressText.text = progress * 100f + "%";

            yield return null;
            level.sliderTransition.SetTrigger("LoadEnd");
        }
    }
}
