using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

//Defining a unity event class that takes in a Vector2
[System.Serializable] public class Vec2Event : UnityEvent<Vector2> { }
[System.Serializable] public class Vec1Event : UnityEvent<float> { }


public class InputController : MonoBehaviour, InputMaster.IPlayerActions
{
    //Singleton setup
    private static InputController _instance;
    public static InputController Inst { get { return _instance; } }
    
    public InputMaster inputMaster;
    public UnityEvent onFire;
    public UnityEvent onInteract;
    public Vec2Event onLook;
    public Vec2Event onMove;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Set up input wrapper to the actual listener
        inputMaster = new InputMaster();
        inputMaster.Player.SetCallbacks(this);
        inputMaster.Player.Enable();
        
        //Set up events for alternate input option
        onFire = new UnityEvent();
        onInteract = new UnityEvent();
        onLook = new Vec2Event();
        onMove = new Vec2Event();
    }

    public void OnDisable()
    {
        inputMaster.Player.Disable();
        inputMaster.UI.Enable();
    }
    public void OnEnable()
    {
        if (inputMaster != null)
        {
            inputMaster.Player.Enable();
            inputMaster.UI.Disable();
        }

    }
    void InputMaster.IPlayerActions.OnFire(InputAction.CallbackContext context)
    { 
        onFire.Invoke();
    }

    void InputMaster.IPlayerActions.OnInteract(InputAction.CallbackContext context)
    {
        onInteract.Invoke();
    }

    void InputMaster.IPlayerActions.OnLook(InputAction.CallbackContext context)
    {
        onLook.Invoke(context.ReadValue<Vector2>());
    }

    void InputMaster.IPlayerActions.OnMove(InputAction.CallbackContext context)
    {
        onMove.Invoke(context.ReadValue<Vector2>());
    }
}
