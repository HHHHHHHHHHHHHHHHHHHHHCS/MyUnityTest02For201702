using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biker : MonoBehaviour
{
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();

        anim.Play("Death",0,1f);

        Invoke("Stop",0.1f);

	}
	
    void Stop()
    {
        anim.speed = 0;
        anim.Play("Death", 0, 0.5f);
    }

}
