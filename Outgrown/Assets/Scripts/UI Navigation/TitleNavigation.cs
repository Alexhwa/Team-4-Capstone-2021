using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleNavigation : MonoBehaviour
{
    InputMaster inputMaster;

    // Start is called before the first frame update
    void Start()
    {
        inputMaster = InputController.Inst.inputMaster;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputController.Inst.inputMaster.UI.Interact.triggered)
        {
            print("navigation");
        }
    }
}
