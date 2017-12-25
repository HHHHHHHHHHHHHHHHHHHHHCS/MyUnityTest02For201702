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
    private int vaultID = Animator.StringToHash("Vault");

    private Vector3 matchTarget;

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
        object _speedID, _isSpeedUpID, _horizontalID, _vaultID;
        if (isFirstMoveType)
        {
            _speedID = "Speed";
            _isSpeedUpID = "IsSpeedUp";
            _horizontalID = "Horizontal";
            _vaultID = "Vault";
        }
        else
        {
            _speedID = speedID;
            _isSpeedUpID = isSpeedUpID;
            _horizontalID = horizontalID;
            _vaultID = vaultID;
        }


        float ver = Input.GetAxis("Vertical") * 5;
        SetFloat(_speedID, ver);

        bool isSpeedUp = Input.GetKey(KeyCode.LeftShift);
        SetBool(_isSpeedUpID, isSpeedUp);

        float hor = Input.GetAxis("Horizontal");
        SetFloat(_horizontalID, hor);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                if(GetFloat(_speedID)>3)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + Vector3.up * 0.07f
                        , transform.forward, out hit, 4.5f))
                    {
                        if (hit.collider.CompareTag("Obstacle"))
                        {
                            if(hit.distance>3)
                            {
                                Vector3 hitPoint = hit.point;
                                hitPoint.y = hit.collider.transform.position.y
                                    + hit.collider.bounds.size.y+0.1f;
                                matchTarget = hitPoint;
                                SetTrigger(_vaultID);
                            }

                        }

                    }

                }

            }

        }


        //MatchTargetWeightMask 里面的两个 float   第一个是要开始触摸的时间  第二个是 触摸点的时间  两个都是0-1
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Vault"))
        {
            anim.MatchTarget(matchTarget, Quaternion.identity, AvatarTarget.LeftHand, new MatchTargetWeightMask(Vector3.one, 0), 0.32f, 0.4f);
        }
    }


    public float GetFloat(object obj)
    {
        if (obj is int)
        {
            return anim.GetFloat((int)obj);
        }
        else if (obj is string)
        {
            return anim.GetFloat((string)obj);
        }
        return 0;
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

    public void SetTrigger(object obj)
    {
        if (obj is int)
        {
            anim.SetTrigger((int)obj);
        }
        else if (obj is string)
        {
            anim.SetTrigger((string)obj);
        }
    }
}
