using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private CharacterSpawner spawner;

    public float smoothSpeed = 10f;
    public Vector3 offset;

    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<CharacterSpawner>();
    }

    void FixedUpdate ()
    {
        if (spawner.cameraTarget == true)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }

}
