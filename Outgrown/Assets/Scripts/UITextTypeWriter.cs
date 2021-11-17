using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)

public class UITextTypeWriter : MonoBehaviour
{

    public float typeSpeed = 10;
    
    private Text txt;
    private string story;

    public UnityEvent onTextEnd = new UnityEvent();

    void Awake () 
    {
        txt = GetComponent<Text> ();
        story = txt.text;
        txt.text = "";

        // TODO: add optional delay when to start
        StartCoroutine ("PlayText");
    }

    IEnumerator PlayText()
    {
        foreach (char c in story) 
        {
            if (c == '~')
            {
                //do nothing
            }
            else
            {
                txt.text += c;
            }

            yield return new WaitForSeconds(1f / typeSpeed);
        }
        onTextEnd.Invoke();
    }

}