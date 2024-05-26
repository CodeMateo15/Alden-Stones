using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class TorchLight : MonoBehaviour
{
    UnityEngine.Rendering.Universal.Light2D fireLight;
    float lightInt;
    public float minInt = 0.7f, maxInt = 1f;
    public bool playerLight;

    void Start()
    {
        fireLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        InvokeRepeating("DimLight", 2f, 2f);
    }

    void Update()
    {
        lightInt = UnityEngine.Random.Range(minInt, maxInt);
        fireLight.intensity = lightInt;
    }

    void DimLight()
    {
        if (playerLight == true)
        {
            if (minInt > 0.25f)
            {
                minInt = minInt - 0.05f;
                maxInt = maxInt - 0.05f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (playerLight == true)
        {
            if (maxInt < 1)
            {
                if (other.gameObject.tag == "TorchLight")
                {
                    minInt = minInt + 0.5f;
                    maxInt = maxInt + 0.5f;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().score += 500f;
                }
            }
        }
    }
}
