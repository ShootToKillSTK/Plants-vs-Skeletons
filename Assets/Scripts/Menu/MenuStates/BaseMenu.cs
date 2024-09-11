using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMenu : MonoBehaviour
{
    public MenuController.MenuStates state;
    protected MenuController context;

    //We pass in our state machine controller context in order to allow all states to know what is contolling them.
    public virtual void InitState(MenuController ctx)
    {
        context = ctx;
    }

    public virtual void EnterState()
    {
        Debug.Log("Entering State: " + state.ToString());
    }

    public virtual void ExitState() 
    {
        Debug.Log("Exiting State: " + state.ToString());
    }

    public void JumpBack()
    {
        context.JumpBack();
    }
}
