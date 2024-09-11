using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    #region Events
    public static event Action<Vector2, float> OnStartTouch;
    public static event Action<Vector2, float> OnEndTouch;
    #endregion

    PlayerInput input;

    protected override void Awake()
    {
        base.Awake();
        input = new PlayerInput();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    // So have some world position ? Week 2 vid
    public Vector2 PrimaryPosition()
    {
        return ScreenToWorld(input.Touch.PrimaryPosition.ReadValue<Vector2>());
    }

    // For 2d
    Vector3 ScreenToWorld(Vector3 pos)
    {
        pos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(pos);
    }

    // OPTIONAL other option, less neat, not together. > Line 54 + 56

    //void StartTouchPrimary(InputAction.CallbackContext ctx)
    //{
    //    OnStartTouch.Invoke(ctx.ReadValue<Vector2>(), (float)ctx.time);
    //}


    // Start is called before the first frame update
    void Start()
    {
        input.Touch.PrimaryContact.started += (ctx) =>
            OnStartTouch?.Invoke(PrimaryPosition(), (float)ctx.time);

        input.Touch.PrimaryContact.canceled += (ctx) =>
            OnEndTouch?.Invoke(PrimaryPosition(), (float)ctx.time);
    }
}
