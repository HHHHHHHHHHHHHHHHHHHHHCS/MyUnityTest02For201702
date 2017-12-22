using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool firstMoveType;

    private Animator anim;
    private int speedID = Animator.StringToHash("Speed"); 


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float ver = Input.GetAxis("Vertical");
        if (ver!=0)
        {
            if(firstMoveType)
            {
                anim.SetFloat("Speed", Mathf.Abs(ver));
            }
            else
            {
                anim.SetFloat(speedID, Mathf.Abs(ver));
            }
        }
    }
}
