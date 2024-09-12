using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float daySpeed = 2f;
    public float nightSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float speed;
        if(transform.position.y >= -1.4)
        {
            speed = daySpeed;
        }
        else
        {
            speed = nightSpeed;
        }
        transform.RotateAround(Vector3.zero, Vector3.right, speed * Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }
}
