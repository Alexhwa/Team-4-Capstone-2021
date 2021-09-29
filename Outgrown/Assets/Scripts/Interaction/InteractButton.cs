using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using TMPro;


public class InteractButton : MonoBehaviour
{
    [SerializeField] TMP_Text KeyPressText;
    [SerializeField] TMP_Text ActionText;
    [SerializeField] GameObject Canvas;

    UnityEvent m_MyEvent = new UnityEvent();

    public string newKeyPress;
    public string newAction;

    // Start is called before the first frame update
    void Start()
    {
        if (newKeyPress.Length != 0)
        {
            KeyPressText.text = newKeyPress;
        }
        if (newAction.Length != 0)
        {
            ActionText.text = newAction;
        }
        Canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("hi");
        Canvas.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Canvas.SetActive(false);
    }
}
