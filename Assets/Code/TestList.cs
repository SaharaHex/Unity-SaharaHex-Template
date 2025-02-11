using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TestList : MonoBehaviour
{
    public GameObject[] list;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearObjects ()
    {
        foreach (GameObject i in list)
        {
            i.GetComponent<Renderer>().material.color = Color.white;
            //TestClickone testClickone;
            TestClickone controlscript = i.GetComponent<Renderer>().GetComponent<TestClickone>();
            controlscript.offMouseEnter = true;
        }        
    }
}
