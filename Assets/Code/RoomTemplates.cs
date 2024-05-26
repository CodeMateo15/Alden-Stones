using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject ClosedbottomRooms;
    public GameObject ClosedtopRooms;
    public GameObject ClosedleftRooms;
    public GameObject ClosedrightRooms;

    public GameObject closedRooms;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedStairs;
    public GameObject stairs;

    void Update()
    {
        if(waitTime <= 0 && spawnedStairs == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    Instantiate(stairs, rooms[i].transform.position, Quaternion.identity);
                    spawnedStairs = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

}
