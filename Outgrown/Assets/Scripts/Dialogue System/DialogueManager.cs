using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] DialogueState dialogueStates;
    private TypewriterEffect typewriterEffect;

    // Start is called before the first frame update
    void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        dialogueStates.ShowDialogue(typewriterEffect);
    }

    /*
    private IEnumerator StepThroughDialogueStates(DialogueState dialogueState)
    {
        
        foreach (DialogueState state in dialogueState)
        {
            state.ShowDialogue();
            //yield return new WaitWhile(state.ShowDialogue);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            print("moving to next state");
        }
        
    }
    */
}
