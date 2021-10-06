using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using TMPro;

public class DialogueEvent : MonoBehaviour
{
    [SerializeField] GameObject playerBubble; 
    [SerializeField] GameObject npcBubble;
    [SerializeField] GameObject continueButton;
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] Button button;

    GameObject currBubble;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (InputController.Inst.inputMaster.Player.Interact.triggered)
        {
            button.onClick.Invoke();
            // dialogueUI.MarkLineComplete();
        }
    }

    [YarnCommand("SetSpeaker")]
    public void SetSpeaker(string speaker)
    {
        // figure out the position of the object 
        // 'destinationName' and start walking to it
        if (speaker == playerBubble.name)
        {
            currBubble = playerBubble;
        }
        else if (speaker == npcBubble.name)
        {
            currBubble = npcBubble;
        }
        else
        {
            Debug.LogError("Error: No speaker bubble found");
        }
    }

    public void OnLineStart()
    {
        currBubble.SetActive(true);
        //continueButton.SetActive(false);
    }

    public void OnLineFinishDisplaying()
    {
        //continueButton.SetActive(true);

    }

    public void OnLineUpdate(string dialogue)
    {
        currBubble.GetComponent<TextMeshProUGUI>().text = dialogue;
    }

    public void OnLineEnd()
    {
        currBubble.SetActive(false);
        //continueButton.SetActive(false);
    }
}
