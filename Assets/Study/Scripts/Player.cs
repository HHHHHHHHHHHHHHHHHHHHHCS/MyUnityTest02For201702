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
    private int colliderID = Animator.StringToHash("Collider");

    private Vector3 matchTarget;
    private bool isReadyVault;
    private bool isPlayVault;

    private CharacterController cc;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        object _speedID, _isSpeedUpID, _horizontalID, _vaultID,_colliderID;
        if (isFirstMoveType)
        {
            _speedID = "Speed";
            _isSpeedUpID = "IsSpeedUp";
            _horizontalID = "Horizontal";
            _vaultID = "Vault";
            _colliderID = "Collider";
        }
        else
        {
            _speedID = speedID;
            _isSpeedUpID = isSpeedUpID;
            _horizontalID = horizontalID;
            _vaultID = vaultID;
            _colliderID = colliderID;
        }


        float ver = Input.GetAxis("Vertical") * 5;
        SetFloat(_speedID, ver);

        bool isSpeedUp = Input.GetKey(KeyCode.LeftShift);
        SetBool(_isSpeedUpID, isSpeedUp);

        float hor = Input.GetAxis("Horizontal");
        SetFloat(_horizontalID, hor);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run")
                && !isReadyVault)
            {
                if(GetFloat(_speedID)>3)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + Vector3.up * 0.07f
                        , transform.forward, out hit, 4.5f))
                    {
                        if (hit.collider.CompareTag("Obstacle"))
                        {
                            if(hit.distance>3&&hit.distance<=4.5)
                            {
                                Vector3 hitPoint = hit.point;
                                hitPoint.y = hit.collider.transform.position.y
                                    + hit.collider.bounds.size.y+0.1f;
                                matchTarget = hitPoint;
                                SetTrigger(_vaultID);
                                isReadyVault = true;
                            }
                        }
                    }
                }

            }

        }


        //MatchTargetWeightMask 里面的两个 float   第一个是要开始触摸的时间  第二个是 触摸点的时间  两个都是0-1
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Vault"))
        {
            if(isReadyVault)
            {
                ResteTrigger(_vaultID);
                isReadyVault = false;
            }
            anim.MatchTarget(matchTarget, Quaternion.identity, AvatarTarget.LeftHand, new MatchTargetWeightMask(Vector3.one, 0), 0.32f, 0.4f);
        }


        cc.enabled = GetFloat(_colliderID) < 0.5f;

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

    public void ResteTrigger(object obj)
    {
        if (obj is int)
        {
            anim.ResetTrigger((int)obj);
        }
        else if (obj is string)
        {
            anim.ResetTrigger((string)obj);
        }
    }
}
