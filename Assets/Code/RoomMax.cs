using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMax : MonoBehaviour
{
    public float numberRoom;
    public float maxRoom;
    public float DL = 1f;
    public float D = 1f;

    private SetSeed sSeed;

    void Start()
    {
        sSeed = GameObject.FindGameObjectWithTag("MainRooms").GetComponent<SetSeed>();
        //Random.InitState(sSeed.seed);
        numberRoom = 0f;
        maxRoom = Random.Range(9f + DL, (9f + DL) * (1.6f + (0.2f * D)));
    }
}
