using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controll : MonoBehaviour
{
    Animator myanim;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody myrb;
        myanim = GetComponent<Animator>();
        myrb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"));
        {
            myanim.SetBool("agachar", true);
        }
        if (!Input.GetKey("w"))
        {
            myanim.SetBool("agachar", false);
        }
    }
}
