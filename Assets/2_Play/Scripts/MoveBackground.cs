using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // �w�i�J��
        transform.position += new Vector3(-5 * Time.deltaTime, 0);
        if(transform.position.x <= -38)
        {
            transform.position = Vector3.zero;
        }
    }
}
