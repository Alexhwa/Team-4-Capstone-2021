using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleNavigation : MonoBehaviour
{
    [SerializeField] Button button;
    InputMaster inputMaster;

    // Start is called before the first frame update
    void Start()
    {
        //inputMaster = InputController.Inst.inputMaster;
        //inputMaster.UI.Enable();
        button.Select();
        //EventSystem.current.SetSelectedGameObject(button.gameObject, null);
    }

    // Update is called once per frame
    void Update()
    {
        /*print(InputController.Inst.inputMaster.UI);
        if (InputController.Inst.inputMaster.UI.Interact.triggered)
        {
            print("navigation");
        }
        if (InputController.Inst.inputMaster.UI.Submit.triggered)
        {
            print("submitting");
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }*/
    }
}
