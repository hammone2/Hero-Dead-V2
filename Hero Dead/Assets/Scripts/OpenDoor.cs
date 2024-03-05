using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Animator anim;
    public string open_anim;
    public string close_anim;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            anim.Play(open_anim);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player" || other.name == "Enemy")
        {
            anim.Play(close_anim);
        }
    }

}
