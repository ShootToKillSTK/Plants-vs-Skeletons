using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetect : MonoBehaviour
{
    public float minDist = 0.3f;
    public float maxTime = 0.0f;

    [Range(0, 1)]
    public float dirThreshold = 0.9f;

    Vector2 startPos;
    Vector2 endPos;
    float startTime;
    float endTime;


    private void OnEnable()
    {
        InputManager.OnStartTouch += SwipeStart;
        InputManager.OnEndTouch += SwipeEnd;
    }
    private void OnDisable()
    {
        InputManager.OnStartTouch -= SwipeStart;
        InputManager.OnEndTouch -= SwipeEnd;
    }



    void SwipeStart(Vector2 pos, float time)
    {
        startPos = pos;
        startTime = time;
    }
    void SwipeEnd(Vector2 pos, float time)
    {
        endPos = pos;
        endTime = time;
        CheckSwipe();
    }
    void CheckSwipe()
    {
        if (Vector2.Distance(startPos, endPos) >= minDist && (endTime - startTime) <= maxTime)
        {
            Vector2 dir = (endPos - startPos).normalized;
            SwipeDirection(dir);
        }
    }



    void SwipeDirection(Vector2 dir)
    {
        if (Vector2.Dot(Vector2.up, dir) >= dirThreshold)
        {
            Debug.Log("Swipe Up");
            return;
        }
        if (Vector2.Dot(Vector2.down, dir) >= dirThreshold)
        {
            Debug.Log("Swipe Down");
            return;
        }
        if (Vector2.Dot(Vector2.right, dir) >= dirThreshold)
        {
            Debug.Log("Swipe Right");
            return;
        }
        if (Vector2.Dot(Vector2.left, dir) >= dirThreshold)
        {
            Debug.Log("Swipe Left");
            return;
        }

        Debug.Log("No Valid Swipes");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
