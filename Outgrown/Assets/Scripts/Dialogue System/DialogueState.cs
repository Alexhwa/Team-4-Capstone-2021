using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueState : MonoBehaviour
{
    [SerializeField] private DialogueContainer[] dialogueContainer;

    [SerializeField] private Response[] responses;
    [SerializeField] private RectTransform responseContainer;
    [SerializeField] private RectTransform responseButtonTemplate;

    private TypewriterEffect typewriterEffect;

    // Start is called before the first frame update
    void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
    }

    public void ShowDialogue()
    {
        for (int i=0; i< dialogueContainer.Length; i++)
        {
            StartCoroutine(StepThroughDialogue(dialogueContainer[i]));
        }
    }

    private IEnumerator StepThroughDialogue(DialogueContainer dialogueContainer)
    {
        foreach (string sentence in dialogueContainer.Dialogues.Sentences)
        {
            yield return typewriterEffect.Run(sentence, dialogueContainer.TMP);
            //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            yield return new WaitForSeconds(2);
        }
        dialogueContainer.TMP.text = string.Empty;
    }

    public void ShowResponses()
    {
        foreach (Response response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));
        }
    }

    private void OnPickedResponse(Response response)
    {
        response.DialogueState.ShowDialogue();
    }
}
