using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // ”wŒi‘JˆÚ
        transform.Translate(-5 * Time.deltaTime, 0, 0);
        if(transform.position.x <= -38)
        {
            transform.position = Vector3.zero;
        }
    }
}
