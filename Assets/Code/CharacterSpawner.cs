using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterKnight;
    public GameObject characterWizard;
    public bool cameraTarget;

    void Start()
    {
        if (CharacterSelection.knight == true)
        {
            StartCoroutine("Knight");
        }
        if (CharacterSelection.wizard == true)
        {
            StartCoroutine("Wizard");
        }
    }

    IEnumerator Knight()
    {
        Instantiate(characterKnight, transform.position, characterKnight.transform.rotation);
        cameraTarget = true;
        yield return new WaitForSeconds(4f);
        Destroy(this);
    }

    IEnumerator Wizard()
    {
        Instantiate(characterWizard, transform.position, characterWizard.transform.rotation);
        cameraTarget = true;
        yield return new WaitForSeconds(4f);
        Destroy(this);
    }
}
