using UnityEngine;
using System.Collections;
public class WhaleMove : MonoBehaviour
{
    public Transform farEnd;
    public Vector3 x;
    public Vector3 y;
    private Vector3 turn;
    private float pass = 30f;
    bool towards = false;


    void Start()
    {
        Debug.Log(x = transform.position);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(x, y, Mathf.SmoothStep(0f, 1f, Mathf.PingPong(Time.time / pass, 1f)));
    }
}