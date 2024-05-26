using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    private int currentSceneIndex;
    private LevelLoader level;
    public float transitionTime = 1f;
    public Image tmp;
    public GameObject character;
    Animator animator;
    public float characterType;

    public static bool knight = false;
    public static bool wizard = false;

    void Start()
    {
        level = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        tmp = GetComponent<Image>();
        animator = character.GetComponent<Animator>();
    }

    public void OnPointerEnter()
    {
        animator.SetBool("Move", true);
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 1f);
    }

    public void OnPointerExit()
    {
        animator.SetBool("Move", false);
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, .5f);
    }

    public void OnPointerDown()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

        if (characterType == 1f)
        {
            knight = true;
        }

        if (characterType == 2f)
        {
            wizard = true;
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        level.transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        level.sliderTransition.SetTrigger("LoadStart");

        yield return new WaitForSeconds(0.5f);

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

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

