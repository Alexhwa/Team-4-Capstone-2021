using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialogueState : MonoBehaviour
{
    [SerializeField] private DialogueContainer[] dialogueContainer;

    [SerializeField] private Response[] responses;
    [SerializeField] private RectTransform responseContainer;
    [SerializeField] private RectTransform responseButtonTemplate;
    
    private TypewriterEffect typewriterEffect;
    private List<GameObject> tempResponseButtons;

    // Start is called before the first frame update
    void Start()
    {
        tempResponseButtons = new List<GameObject>();
    }

    public void ShowDialogue(TypewriterEffect typewriterEffect)
    {
        this.typewriterEffect = typewriterEffect;
        // loop through all the actors that contain dialogue
        for (int i=0; i< dialogueContainer.Length; i++)
        {
            StartCoroutine(StepThroughDialogue(dialogueContainer[i], typewriterEffect));
        }
        StartCoroutine(PressSpaceToContinue());
    }

    private IEnumerator StepThroughDialogue(DialogueContainer dialogueContainer, TypewriterEffect typewriterEffect)
    {
        foreach (string sentence in dialogueContainer.Dialogues.Sentences)
        {
            yield return typewriterEffect.Run(sentence, dialogueContainer.TMP);
            //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            yield return new WaitForSeconds(2);
        }
        dialogueContainer.TMP.text = string.Empty;
    }

    private IEnumerator PressSpaceToContinue()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        if (responses != null && responses.Length > 0)
        {
            ShowResponses();
        }
    }

    public void ShowResponses()
    {
        foreach (Response response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));

            tempResponseButtons.Add(responseButton);
        }
        responseContainer.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response)
    {
        responseContainer.gameObject.SetActive(false);
        foreach (GameObject tempButton in tempResponseButtons)
        {
            Destroy(tempButton);
        }
        tempResponseButtons.Clear();
        response.DialogueState.ShowDialogue(typewriterEffect);
    }
}
