using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(30f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 3.6f, 0f, Space.World);
    }
}
