using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using TMPro;
using Yarn.Unity;
using Yarn.Unity.Example;


public class InteractButton : MonoBehaviour
{
    [SerializeField] TMP_Text KeyPressText;
    [SerializeField] TMP_Text ActionText;
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject dialogueEvent;
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] NPC npc;

    UnityEvent m_MyEvent = new UnityEvent();

    public string newKeyPressText;
    public string newActionText;

    // Start is called before the first frame update
    void Start()
    {
        if (newKeyPressText.Length != 0)
        {
            KeyPressText.text = newKeyPressText;
        }
        if (newActionText.Length != 0)
        {
            ActionText.text = newActionText;
        }
        Canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Canvas.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Canvas.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if ((InputController.Inst.inputMaster.Player.Interact.triggered || InputController.Inst.inputMaster.Player.Interact.triggered) && !dialogueRunner.IsDialogueRunning)
        {
            dialogueEvent.SetActive(true);
            dialogueRunner.StartDialogue(npc.talkToNode);
        }
    }
}
