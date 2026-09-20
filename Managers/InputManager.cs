
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputManager : MonoBehaviour, OnePlayerInputs.IOnePlayerActions
{
    public static InputManager instance { get; private set; }

    [Header("Mode")]
    public int playerMode = 0;

    public OnePlayerInputs playerInputs { get; private set; }

    public Vector2 leftHand { get; private set; }
    public Vector2 rightHand { get; private set; }

    public bool leftTriggerPressed { get; private set; }
    public bool leftTriggerDown { get; private set; }
    public bool rightTriggerPressed { get; private set; }
    public bool rightTriggerDown { get; private set; }

    public bool optionsPressed { get; private set; }
    public bool optionsHoldPressed { get; private set; }
    public bool optionsBack { get; private set; }
    public bool moveWhenever { get; private set; }
    public bool anyKeyPressed { get; private set; } = false;

    private InputDevice player1Device;
    private InputDevice player2Device;

    private const string BINDINGS_PREFS_KEY = "CustomBindings";

    private InputAction anyButtonAction;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        leftHand = Vector2.zero;
        rightHand = Vector2.zero;

        playerInputs = new OnePlayerInputs();
        playerInputs.OnePlayer.SetCallbacks(this);

        anyButtonAction = new InputAction(binding: "/*/<button>");

        // Load any saved custom bindings before enabling
        LoadBindings();

        AssignControllers();
    }
    void Start()
    {
        optionsHoldPressed = false; 
        LockCursor();

      
    }

    //FOR DEMO
    private void Update()
    {
        anyKeyPressed = anyButtonAction.IsPressed();

     
        if ((Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame))
        {
            UnlockCursor();
        }

        if (Cursor.visible && Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            LockCursor();
        }
    }
    


    public void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Mouse.current.WarpCursorPosition(Vector2.zero);
    }


    private void AssignControllers()
    {
        var gamepads = Gamepad.all;

        player1Device = null;
        player2Device = null;

        if (gamepads.Count > 0)
            player1Device = gamepads[0];

        if (gamepads.Count > 1)
            player2Device = gamepads[1];
    }

    private void OnEnable()
    {
        if (this != instance) return;
        InputSystem.onDeviceChange += OnDeviceChange;
        playerInputs.OnePlayer.Enable();

        anyButtonAction.Enable();

    }

    private void OnDisable()
    {
        if (this != instance) return;
        InputSystem.onDeviceChange -= OnDeviceChange;
        playerInputs.OnePlayer.Disable();
        anyButtonAction.Disable();
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added ||
            change == InputDeviceChange.Removed ||
            change == InputDeviceChange.Reconnected ||
            change == InputDeviceChange.Disconnected)
        {
            AssignControllers();
        }
    }

   
    private void LateUpdate()
    {
        leftTriggerDown = false;
        rightTriggerDown = false;
        optionsPressed = false;
        optionsBack = false;
    }

    private bool IsKeyboard(InputAction.CallbackContext context)
    {
        return context.control.device is Keyboard;
    }

    private bool IsOnePlayerMode => playerMode == 0;
    private bool IsTwoPlayerNormal => playerMode == 1;
    private bool IsTwoPlayerCrossed => playerMode == 2;

    public void OnLeftJoystick(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            leftHand = Vector2.zero;
            return;
        }

        if (IsKeyboard(context) || IsOnePlayerMode)
        {
            leftHand = context.ReadValue<Vector2>();
            return;
        }

        if (context.control.device != player1Device)
            return;

        leftHand = context.ReadValue<Vector2>();
    }

    public void OnRightJoystick(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            rightHand = Vector2.zero;
            return;
        }

        if (IsKeyboard(context) || IsOnePlayerMode)
        {
            rightHand = context.ReadValue<Vector2>();
            return;
        }

        if (context.control.device != player2Device)
            return;

        rightHand = context.ReadValue<Vector2>();
    }

    public void OnHoldLeft(InputAction.CallbackContext context)
    {
        if (!IsKeyboard(context) && !IsOnePlayerMode)
        {
            if (IsTwoPlayerNormal && context.control.device != player1Device)
                return;
            if (IsTwoPlayerCrossed && context.control.device != player2Device)
                return;
        }

        HandleLeftTrigger(context);
    }

    public void OnHoldRight(InputAction.CallbackContext context)
    {
        if (!IsKeyboard(context) && !IsOnePlayerMode)
        {
            if (IsTwoPlayerNormal && context.control.device != player2Device)
                return;
            if (IsTwoPlayerCrossed && context.control.device != player1Device)
                return;
        }

        HandleRightTrigger(context);
    }

    private void HandleLeftTrigger(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            leftTriggerPressed = true;
            leftTriggerDown = true;
        }
        else if (context.performed)
        {
            leftTriggerPressed = true;
        }
        else if (context.canceled)
        {
            leftTriggerPressed = false;
        }
    }

    private void HandleRightTrigger(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            rightTriggerPressed = true;
            rightTriggerDown = true;
        }
        else if (context.performed)
        {
            rightTriggerPressed = true;
        }
        else if (context.canceled)
        {
            rightTriggerPressed = false;
        }
    }

    public void OnOptionsOpen(InputAction.CallbackContext context)
    {
       
        if (context.started)
        {
            optionsPressed = true;
            optionsHoldPressed = true;
        }
        else if (context.performed)
        {
            optionsHoldPressed = true;
        }
        else if (context.canceled)
        {
            
            optionsHoldPressed = false;
        }
    }

    public void OnBack(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        optionsBack = true;
    }


    public void OnMovewherever(InputAction.CallbackContext context)
    {
        
#if UNITY_EDITOR
        if (context.started)
        {
            moveWhenever = !moveWhenever;
        }
#endif
#if !UNITY_EDITOR
        moveWhenever = false;
        if(Debug.isDebugBuild)
        {
            if (context.started)
            {
                moveWhenever = !moveWhenever;
            }
        }
        
#endif
    }

    public void StartRebind(string actionName, int bindingIndex, Action onComplete = null, Action onCancel = null)
    {
        InputAction action = playerInputs.asset.FindAction(actionName);
        if (action == null)
        {
            Debug.LogWarning($"Action {actionName} not found.");
            return;
        }

        // Action must be disabled before rebinding
        action.Disable();

        var rebindOperation = action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("Mouse") // Optional: Prevents accidental mouse clicks from overriding buttons
            .OnComplete(operation =>
            {
                operation.Dispose();
                action.Enable();
                SaveBindings(); // Automatically save the new binding
                onComplete?.Invoke();
            })
            .OnCancel(operation =>
            {
                operation.Dispose();
                action.Enable();
                onCancel?.Invoke();
            })
            .Start();
    }

    public void SaveBindings()
    {
        string rebinds = playerInputs.asset.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(BINDINGS_PREFS_KEY, rebinds);
        PlayerPrefs.Save();
    }

    public void LoadBindings()
    {
        if (PlayerPrefs.HasKey(BINDINGS_PREFS_KEY))
        {
            string rebinds = PlayerPrefs.GetString(BINDINGS_PREFS_KEY);
            playerInputs.asset.LoadBindingOverridesFromJson(rebinds);
        }
    }


    public void ResetAllBindings()
    {
        playerInputs.asset.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey(BINDINGS_PREFS_KEY);
        PlayerPrefs.Save();
    }

}

