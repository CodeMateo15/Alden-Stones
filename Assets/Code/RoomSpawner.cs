using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [Tooltip("The seed that Room will be generated with. Set to a value %lt 0 to have the seed generated randomly on build/generation.")]
    public int seed = -1;

    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door

    private RoomTemplates templates;
    private AddRoom add;
    private int rand;
    public bool spawned = false;
    public bool fix = false;

    private float waitTime = 20f;
    private RoomMax nr;
    private SetSeed sSeed;

    void Start()
    {
        //sSeed = GameObject.FindGameObjectWithTag("MainRooms").GetComponent<SetSeed>();
        //UnityEngine.Random.InitState(sSeed.seed);
        //causing the EnemySpawner to have the same seed;
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("MainRooms").GetComponent<RoomTemplates>();
        add = gameObject.GetComponentInParent<AddRoom>();
        Invoke("Spawn", 1f);
        nr = GameObject.FindWithTag("MainRooms").GetComponent<RoomMax>();
    }

    void Spawn()
    {
        var rng = RandomUtil.CreateDeterministicRNG(seed);

        if (templates == null)
        {
            UnityEngine.Debug.Log("templates not been set");
        }
        if (spawned == false)
        {
            if (nr.numberRoom <= nr.maxRoom) 
            {
                if (openingDirection == 0)
                {
                }
                if (openingDirection == 1)
                {
                    rand = rng.Range(templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                    nr.numberRoom += 1f;
                }
                else if (openingDirection == 2)
                {
                    //rand = Random.Range(1, templates.topRooms.Length);
                    rand = rng.Range(templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                    nr.numberRoom += 1f;
                }
                else if (openingDirection == 3)
                {
                    rand = rng.Range(templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                    nr.numberRoom += 1f;
                }
                else if (openingDirection == 4)
                {
                    rand = rng.Range(templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                    nr.numberRoom += 1f;
                }
                spawned = true;
                UnityEngine.Debug.Log(rand + " " + transform.position);
            }
            if (nr.numberRoom > nr.maxRoom)
            {
                Invoke("SpawnClose", 1f);
            }
        }
    }

    void SpawnClose()
    {
        if (spawned == false)
        {
            if (nr.numberRoom > nr.maxRoom)
            {
                if (openingDirection == 1)
                {
                    Instantiate(templates.ClosedbottomRooms, transform.position, templates.ClosedbottomRooms.transform.rotation);
                }
                else if (openingDirection == 2)
                {
                    Instantiate(templates.ClosedtopRooms, transform.position, templates.ClosedtopRooms.transform.rotation);
                }
                else if (openingDirection == 3)
                {
                    Instantiate(templates.ClosedleftRooms, transform.position, templates.ClosedleftRooms.transform.rotation);
                }
                else if (openingDirection == 4)
                {
                    Instantiate(templates.ClosedrightRooms, transform.position, templates.ClosedrightRooms.transform.rotation);
                }
                spawned = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (spawned == false && other.GetComponent<RoomSpawner>().spawned == false)
            {
                Instantiate(templates.closedRooms, transform.position, templates.closedRooms.transform.rotation);
                Destroy(gameObject);
            }
            spawned = true;
        }
        /*if (other.CompareTag("FixPoint"))
        {
            if (fix == false) 
            {
                if (other.transform.GetChild(0) == transform.parent.position || other.transform.GetChild(1) == transform.parent.position)
                {

                }
                /*if (openingDirection == 1)
                {
                    Instantiate(templates.ClosedbottomRooms, transform.position, templates.ClosedbottomRooms.transform.rotation);
                }
                else if (openingDirection == 2)
                {
                    Instantiate(templates.ClosedtopRooms, transform.position, templates.ClosedtopRooms.transform.rotation);
                }
                else if (openingDirection == 3)
                {
                    Instantiate(templates.ClosedleftRooms, transform.position, templates.ClosedleftRooms.transform.rotation);
                }
                else if (openingDirection == 4)
                {
                    Instantiate(templates.ClosedrightRooms, transform.position, templates.ClosedrightRooms.transform.rotation);
                }
                Instantiate(templates.closedRooms, transform.parent.position, templates.closedRooms.transform.rotation);
                Destroy(transform.parent.gameObject);
            }
        }*/
    }
}