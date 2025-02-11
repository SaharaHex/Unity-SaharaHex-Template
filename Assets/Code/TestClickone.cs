using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TestClickone : MonoBehaviour //, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler //not working
{
    private new Renderer renderer;
    public bool offMouseEnter = true;
    public Canvas CanvasUIOfSeletion;
    public string TextColor;
    public GameObject CanvasUITextItemZone;

    TestList TL;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        CanvasUIOfSeletion.enabled = false;
        CanvasUITextItemZone = CanvasUIOfSeletion.transform.GetChild(0).gameObject;
        TL = GameObject.FindGameObjectWithTag("CapuleTag").GetComponent<TestList>();
    }

    // Update is called once per frame
    void Update()
    {
        //maybe use this
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Pressed primary button.");

        if (Input.GetMouseButtonDown(1))
            Debug.Log("Pressed secondary button.");

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");
    }

    private void OnMouseEnter()
    {
        if (offMouseEnter)
        {
            renderer.material.color = SetColor(TextColor);
            CanvasUIOfSeletion.enabled = true;
            CanvasUITextItemZone.GetComponent<TMP_Text>().text = SetText(TextColor);
        }
    }

    private void OnMouseExit()
    {
        if (offMouseEnter)
        {
            renderer.material.color = Color.white;
            CanvasUIOfSeletion.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        TL.ClearObjects();
        renderer.material.color = Color.yellow;
        CanvasUIOfSeletion.enabled = true; 
        offMouseEnter = false;
        CanvasUITextItemZone.GetComponent<TMP_Text>().text = SetText(TextColor);
    }

    private Color SetColor(string c)
    {
        if (c == "red")
            return Color.red;
        else if (c == "green")
            return Color.green;
        else if (c == "blue")
            return Color.blue;
        else
            return new Color();
    }

    private string SetText(string c)
    {
        if (c == "red")
            return "this is red";
        else if (c == "green")
            return "this is green";
        else if (c == "blue")
            return "this is blue";
        else
            return "";
    }

    //not working
    //void IPointerEnterHandler.OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    //{
    //    Debug.Log("OnPointerEnter");
    //}

    //void IPointerExitHandler.OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData) 
    //{
    //    Debug.Log("OnPointerExit");
    //}

    //void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    //{
    //    Debug.Log("OnPointerDown");
    //}

}
