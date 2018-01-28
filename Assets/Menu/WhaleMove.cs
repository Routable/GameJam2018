using UnityEngine;
using System.Collections;

public class WhaleMove : MonoBehaviour
{
    public RectTransform rect;
    public int width;
    public int height;
    public Vector2 start;
    public Vector2 end;
    private float pass = 30f;
    bool towards = false;

    public float speed;


    void Start()
    {
        rect = this.GetComponent<RectTransform>();
        width = Screen.width;
        height = Screen.height;
        speed = Random.Range(0.5f, 1.5f);
    }

    void Update()
    {
        rect.position = Vector2.Lerp(new Vector2(start.x * width, start.y * height), new Vector2(end.x * width, end.y * height), Mathf.SmoothStep(0.2f, 1f, Mathf.PingPong(Time.time / pass, speed)));
    }
}