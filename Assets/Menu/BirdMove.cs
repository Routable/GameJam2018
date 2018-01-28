﻿using UnityEngine;
using System.Collections;
public class BirdMove : MonoBehaviour
{

    public float min = 0f;
    public float max = 200f;
    // Use this for initialization
    void Start()
    {

        min = transform.position.x;
        max = transform.position.x + 450;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * 10, max - min) + min, transform.position.y, transform.position.z);
    }
}