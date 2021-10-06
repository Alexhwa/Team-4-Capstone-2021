using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueContainer
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject dialogues;

    public TMP_Text TMP => textLabel;
    public DialogueObject Dialogues => dialogues;
}
