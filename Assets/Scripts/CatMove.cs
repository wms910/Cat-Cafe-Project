using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatMove : MonoBehaviour
{
    float xDirection;

    // Start is called before the first frame update
    void Start()
    {
        xDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= 776)
        {
            xDirection *= -1;
            transform.localScale += new Vector3(-4, 0, 0);
        }
        if (transform.position.x <= 538)
        {
            xDirection *= -1;
            transform.localScale += new Vector3(4, 0, 0);
        }

        transform.Translate(xDirection, 0, 0);
        //36 to 290 spawn height
    }
}
