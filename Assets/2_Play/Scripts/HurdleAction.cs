using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdleAction : MonoBehaviour
{
    private enum STATE_HURDLE
    {
        ROCK, // ä‚
        WOOD_LOW,
        WOOD_MIDDLE, // ñÿ
    }
    [SerializeField] STATE_HURDLE state_hurdle;

    // Start is called before the first frame update
    void Start()
    {
        // èâä˙îzíu
        switch (state_hurdle)
        {
            case STATE_HURDLE.ROCK:
                transform.position = new Vector3(10, -3.5f);
                break;
            case STATE_HURDLE.WOOD_LOW:
                transform.position = new Vector3(10,-2.8f);
                break;
            case STATE_HURDLE.WOOD_MIDDLE:
                transform.position = new Vector3(10, 0);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -10)
        {
            // é©êgÇîjä¸Ç∑ÇÈ
            Destroy(gameObject);
        }
        else
        {
            // ç∂Ç…à⁄ìÆÇ∑ÇÈ
            transform.Translate(5 * -Time.deltaTime, 0, 0);
        }
    }
}
