using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biker : MonoBehaviour
{
    Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        //Test1();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Pose");
        }
        bool isMove = false;
        float hor = 0, ver = 0;
        if (Input.GetKey(KeyCode.W))
        {
            isMove = true;
            ver += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            isMove = true;
            ver -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            isMove = true;
            hor -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            isMove = true;
            hor += 1;
        }
        if (isMove && (hor != 0 || ver != 0))
        {
            transform.Translate(new Vector3(hor, 0, ver) * Time.deltaTime);
        }

        anim.SetBool("IsMove", isMove);
        anim.SetFloat("Horizontal", hor);
        anim.SetFloat("Vertical", ver);
    }


    void Test1()
    {
        anim.Play("Death", 0, 1f);
        Invoke("Test1_1", 0.1f);

    }

    void Test1_1()
    {
        anim.speed = 0;
        anim.Play("Death", 0, 0.5f);
    }

}
