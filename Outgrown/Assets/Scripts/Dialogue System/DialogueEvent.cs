using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueEvent : MonoBehaviour
{
    enum Speaker { PLAYER, NPC };

    [SerializeField] GameObject[] speechContainer;
    [SerializeField] GameObject[] speechBubble;
    [SerializeField] GameObject[] continueButton;
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] DialogueRunner dialogueRunner;

    Speaker speaker = Speaker.PLAYER;
    private int buttonIndex = 0;
    bool waitingForOptionSelection = false;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Camera mainCam = Camera.main;
        /*print(InputController.Inst.inputMaster.UI.Click.triggered);
        Vector3 worldPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        print(worldPos);*/
        if (dialogueRunner.IsDialogueRunning == true)
        {
            if (InputController.Inst.inputMaster.UI.Interact.triggered)
            {
                dialogueUI.MarkLineComplete();
                //dialogueUI.optionButtons[0].Select();
                // dialogueUI.optionButtons[buttonIndex];
                if (waitingForOptionSelection)
                    dialogueUI.SelectOption(buttonIndex);
            }
            /*if (InputController.Inst.inputMaster.Player.Selection.triggered)
            {
                if (InputController.Inst.inputMaster.Player.Selection.ReadValue<float>() < 0)
                {
                    print("pressed W");
                    if (buttonIndex - 1 < 0)
                        buttonIndex = dialogueUI.optionButtons.Count - 1;
                    else
                        buttonIndex = --buttonIndex % dialogueUI.optionButtons.Count;
                    dialogueUI.optionButtons[buttonIndex].Select();
                }
                else if (InputController.Inst.inputMaster.Player.Selection.ReadValue<float>() > 0)
                {
                    print("pressed S");
                    buttonIndex = ++buttonIndex % dialogueUI.optionButtons.Count;
                    dialogueUI.optionButtons[buttonIndex].Select();
                }
            }*/
        }
    }

    [YarnCommand("SetSpeaker")]
    public void SetSpeaker(string speakerID)
    {
        // figure out the position of the object 
        // 'destinationName' and start walking to it
        if (speakerID == speechBubble[0].name)
        {
            speaker = Speaker.PLAYER;
        }
        else if (speakerID == speechBubble[1].name)
        {
            speaker = Speaker.NPC;
        }
        else
        {
            Debug.LogError("Error: No speaker bubble found");
        }
    }

    public void OnLineStart()
    {
        switch (speaker)
        {
            case Speaker.PLAYER:
                speechBubble[0].SetActive(true);
                speechBubble[1].SetActive(false);
                continueButton[0].SetActive(false);
                break;
            case Speaker.NPC:
                speechBubble[0].SetActive(false);
                speechBubble[1].SetActive(true);
                continueButton[1].SetActive(false);
                break;
        }
    }

    public void OnLineFinishDisplaying()
    {
        switch (speaker)
        {
            case Speaker.PLAYER:
                continueButton[0].SetActive(true);
                continueButton[1].SetActive(false);
                break;
            case Speaker.NPC:
                continueButton[0].SetActive(false);
                continueButton[1].SetActive(true);
                break;
        }
    }

    public void OnLineUpdate(string dialogue)
    {
        switch (speaker)
        {
            case Speaker.PLAYER:
                speechBubble[0].GetComponent<TextMeshProUGUI>().text = dialogue;
                break;
            case Speaker.NPC:
                speechBubble[1].GetComponent<TextMeshProUGUI>().text = dialogue;
                break;
        }
    }

    public void OnLineEnd()
    {
        // currBubble.SetActive(false);
        //continueButton.SetActive(false);
        switch (speaker)
        {
            case Speaker.PLAYER:
                speechBubble[0].SetActive(false);
                continueButton[0].SetActive(false);
                break;
            case Speaker.NPC:
                speechBubble[1].SetActive(false);
                continueButton[1].SetActive(false);
                break;
        }
    }

    public void OnOptionStart()
    {
        buttonIndex = 0;
        dialogueUI.optionButtons[0].Select();
        
    }

    public void SetWaitingForOptionSelection(bool state)
    {
        waitingForOptionSelection = state;
    }
}
