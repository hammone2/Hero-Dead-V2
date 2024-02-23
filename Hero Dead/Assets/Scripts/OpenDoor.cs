using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        //anim.Play("door_3_open");
    }

    // Update is called once per frame
    void Update()
    {
        anim.Play("door_3_open");
    }
}
