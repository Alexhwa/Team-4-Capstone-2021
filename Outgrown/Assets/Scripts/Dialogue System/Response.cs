using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueState dialogueState;

    public string ResponseText => responseText;

    public DialogueState DialogueState => dialogueState;
}
