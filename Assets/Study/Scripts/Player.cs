using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float ver = Input.GetAxis("Vertical");
        if (ver!=0)
        {
            anim.SetFloat("Speed", Mathf.Abs(ver));
        }
    }
}
