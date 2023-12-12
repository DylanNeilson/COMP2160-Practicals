using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTime : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log("deltaTime = " + Time.deltaTime);*/
        Debug.Log("FPS = " + 1 / Time.deltaTime);
    }
}
