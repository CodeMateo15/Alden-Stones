using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScene : MonoBehaviour
{
    private RoomMax dl;
    public float transitionTime = 1f;
    private LevelLoader level;
    private int currentSceneIndex;

    void Start()
    {
        dl = GameObject.FindWithTag("Rooms").GetComponent<RoomMax>();
        level = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
        var game = GameSettings.Instance;
        game.LoadExistingScene(game.CurrentSceneState.Id - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMenuLevel()
    {
        var game = GameSettings.Instance;
        if (game.CurrentSceneState.Id == 2)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 2));
            game.LoadExistingScene(game.CurrentSceneState.Id - 2);
        }
        if (game.CurrentSceneState.Id == 3)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 3));
            game.LoadExistingScene(game.CurrentSceneState.Id - 3);
        }
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
