using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isFirstMoveType;

    private Animator anim;
    private int speedID = Animator.StringToHash("Speed");
    private int isSpeedUpID = Animator.StringToHash("IsSpeedUp");
    private int horizontalID = Animator.StringToHash("Horizontal");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        object _speedID, _isSpeedUpID, _horizontalID;
        if (isFirstMoveType)
        {
            _speedID = "Speed";
            _isSpeedUpID = "IsSpeedUp";
            _horizontalID = "Horizontal";
        }
        else
        {
            _speedID = speedID;
            _isSpeedUpID = isSpeedUpID;
            _horizontalID = horizontalID;
        }


        float ver = Input.GetAxis("Vertical");
        SetFloat(_speedID, ver);

        bool isSpeedUp = Input.GetKey(KeyCode.LeftShift);
        Debug.Log(isSpeedUp);
        SetBool(_isSpeedUpID, isSpeedUp);

        float hor = Input.GetAxis("Horizontal");
        SetFloat(_horizontalID, hor);
    }



    public void SetFloat(object obj, float value)
    {
        if (obj is int)
        {
            anim.SetFloat((int)obj, value);
        }
        else if (obj is string)
        {
            anim.SetFloat((string)obj, value);
        }
    }

    public void SetBool(object obj, bool value)
    {
        if (obj is int)
        {
            anim.SetBool((int)obj, value);
        }
        else if (obj is string)
        {
            anim.SetBool((string)obj, value);
        }
    }
}
