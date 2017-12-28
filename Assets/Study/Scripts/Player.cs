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
    private int slideID = Animator.StringToHash("Slide");
    private int isCarryWoodID = Animator.StringToHash("IsCarryWood");
    private object _speedID, _isSpeedUpID, _horizontalID
    , _vaultID, _colliderID, _slideID, _isCarryWoodID;

    private Vector3 matchTarget;
    private bool isReadyVault;
    private bool isReadySlide;

    private CharacterController cc;
    [SerializeField]
    private GameObject wood;
    private Transform woodLeft;
    private Transform woodRight;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        woodLeft = wood.transform.Find("LeftHand");
        woodRight = wood.transform.Find("RightHand");
        Init();
    }

    private void Update()
    {
        Move();
    }

    public void Init()
    {
        if (isFirstMoveType)
        {
            _speedID = "Speed";
            _isSpeedUpID = "IsSpeedUp";
            _horizontalID = "Horizontal";
            _vaultID = "Vault";
            _colliderID = "Collider";
            _slideID = "Slide";
            _isCarryWoodID = "IsCarryWood";
        }
        else
        {
            _speedID = speedID;
            _isSpeedUpID = isSpeedUpID;
            _horizontalID = horizontalID;
            _vaultID = vaultID;
            _colliderID = colliderID;
            _slideID = slideID;
            _isCarryWoodID = isCarryWoodID;
        }
    }

    public void Move()
    {
        float ver = Input.GetAxis("Vertical") * 5;
        SetFloat(_speedID, ver);

        bool isSpeedUp = Input.GetKey(KeyCode.LeftShift);
        SetBool(_isSpeedUpID, isSpeedUp);

        float hor = Input.GetAxis("Horizontal");
        SetFloat(_horizontalID, hor);


        PlayVault();
        PlaySlide();




        cc.enabled = GetFloat(_colliderID) < 0.5f;

    }

    private void PlayVault()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run")
                && !isReadyVault)
            {
                if (GetFloat(_speedID) > 3)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + Vector3.up * 0.07f
                        , transform.forward, out hit, 4f))
                    {
                        if (hit.collider.CompareTag("Obstacle"))
                        {
                            if (hit.distance > 3 )
                            {
                                Vector3 hitPoint = hit.point;
                                hitPoint.y = hit.collider.transform.position.y
                                    + hit.collider.bounds.size.y + 0.1f;
                                matchTarget = hitPoint;
                                SetTrigger(_vaultID);
                                isReadyVault = true;
                            }
                        }
                    }
                }
            }
        }

        //MatchTargetWeightMask 里面的 vector3 是权重 1 是代表完全修改    0代表 不修改
        //里面的两个 float   第一个是要开始触摸的时间  第二个是 触摸点的时间  两个都是0-1
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Vault"))
        {
            if (isReadyVault)
            {
                ResteTrigger(_vaultID);
                isReadyVault = false;
            }
            anim.MatchTarget(matchTarget, Quaternion.identity, AvatarTarget.LeftHand, new MatchTargetWeightMask(Vector3.one, 0), 0.32f, 0.4f);
        }
    }

    private void PlaySlide()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run")
                && !isReadyVault)
            {
                if (GetFloat(_speedID) > 3)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + Vector3.up * 1.5f
                        , transform.forward, out hit, 5f))
                    {
                        if (hit.collider.CompareTag("Obstacle"))
                        {
                            if (hit.distance > 4)
                            {
                                Vector3 point = hit.point;
                                point.y = 0;
                                matchTarget= point + transform.forward * 2;
                                SetTrigger(_slideID);
                                isReadySlide = true;
                            }
                        }
                    }
                }
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            if (isReadySlide)
            {
                ResteTrigger(_slideID);
                isReadySlide = false;
            }
            anim.MatchTarget(matchTarget, Quaternion.identity, AvatarTarget.Root, new MatchTargetWeightMask(new Vector3(1,0,1), 0), 0.17f, 0.67f);
        }
    }

    public void CarryWood()
    {

        wood.SetActive(true);
        SetBool(_isCarryWoodID, true);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        //layerIndex 是指第几层动画（那一层的动画必须勾选IKPASS）
        if(layerIndex==1)
        {
            int weight = GetBool(_isCarryWoodID) ? 1 : 0;
            anim.SetIKPosition(AvatarIKGoal.LeftHand, woodLeft.position);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
            anim.SetIKPosition(AvatarIKGoal.RightHand, woodRight.position);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
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

    public bool GetBool(object obj)
    {
        if (obj is int)
        {
            return anim.GetBool((int)obj);
        }
        else if (obj is string)
        {
            return anim.GetBool((string)obj);
        }
        return false;
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
