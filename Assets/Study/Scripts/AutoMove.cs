using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public enum MoveType
    {
        lerp,
        slerp,
        smoothDamp,
        repeat,
        pingpong
    }
    public MoveType moveType;
    public float targetPosX = 10f;
    private Vector3 targetPos = new Vector3(10f, 10f, 10f);
    private float realTime = -1;
    private bool isShow = true;

    private void Awake()
    {
        targetPos = new Vector3(targetPosX, transform.position.y, transform.position.z);
    }

    private void Start()
    {
        realTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        switch (moveType)
        {
            case MoveType.lerp:
                MoveLerp();
                break;
            case MoveType.slerp:
                MoveSlerp();
                break;
            case MoveType.smoothDamp:
                MoveSmoothDamp();
                break;
            case MoveType.repeat:
                MoveRepeat();
                break;
            case MoveType.pingpong:
                MovePingpong();
                break;
            default:
                break;
        }

        if (isShow && transform.position.x > 9.9)
        {
            isShow = false;
            Debug.Log(transform.name + ":" + (Time.realtimeSinceStartup - realTime));
        }
    }


    float time1 = 0;
    Vector3 startPos1;
    void MoveLerp()
    {
        if (time1 <= 0)
        {
            startPos1 = transform.position;
            time1 = 1 * Time.deltaTime;
        }
        else
        {
            time1 += 1 * Time.deltaTime;
        }
        transform.position = Vector3.Lerp(startPos1, targetPos
            , time1);


    }

    float time2 = 0;
    Vector3 startPos2;
    void MoveSlerp()
    {
        if (time2 <= 0)
        {
            startPos2 = transform.position;
            time2 = 1 * Time.deltaTime;
        }
        else
        {
            time2 += 1 * Time.deltaTime;
        }
        transform.position = Vector3.Slerp(startPos2, targetPos
            , time2);
    }

    Vector3 current = Vector3.zero;
    void MoveSmoothDamp()
    {
        //平滑时间 多半是不准的  基本是 1秒=0.3f
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref current, 0.3f);
    }


    void MoveRepeat()
    {
        Vector3 pos = targetPos;
        pos.x = Mathf.Repeat(Time.time, targetPos.x);
        transform.position = pos;
    }

    void MovePingpong()
    {
        Vector3 pos = targetPos;
        pos.x = Mathf.PingPong(Time.time, targetPos.x);
        transform.position = pos;
    }
}
