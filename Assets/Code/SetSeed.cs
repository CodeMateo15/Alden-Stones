using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSeed : MonoBehaviour
{
    public string stringSeed;
    public int Sseed;
    public bool randomizeSeed;
    public bool manualSeed;

    //Sseed temporary, set seed back to normal

    void Awake()
    {
        if (randomizeSeed == false && manualSeed == false)
        {
            Sseed = stringSeed.GetHashCode();
        }

        if (randomizeSeed == true)
        {
            Sseed = Random.Range(0, 99999);
        }

        if (manualSeed == true && randomizeSeed == false)
        {
            Sseed = Sseed;
        }

        /*UnityEngine.Random.InitState(seed);

        UnityEngine.Debug.Log(Random.Range(0, 99999));
        UnityEngine.Debug.Log(Random.Range(0, 99999));
        UnityEngine.Debug.Log(Random.Range(0, 99999));*/
    }
}
