using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Four : MonoBehaviour
{
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 position = transform.position;  
        position.x = position.x + 0.05f * horizontal;    
        transform.position = position;

        float vertical = Input.GetAxis("Vertical");
        Vector2 position2 = transform.position;
        position2.y = position2.y + 0.05f * vertical;
        transform.position = position2;
    }
}
