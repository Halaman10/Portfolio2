using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dSpeed = 2f; // day speed
    public float nSpeed = 4f; // night speed

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float speed;
        if(transform.position.y >= -1.4) // when the angle of the Directional Light is >= to 1.4 it will be day speed, otherwise it is night speed
        {
            speed = dSpeed;
        }
        else
        {
            speed = nSpeed;
        }
        transform.RotateAround(Vector3.zero, Vector3.right, speed * Time.deltaTime); // these 2 get called every frame allowing for smooth day night cycling
        transform.LookAt(Vector3.zero);
    }
}
