using UnityEngine;

public class MainMenu : BaseMenu
{
    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.Main;
    }

    public override void EnterState()
    {
        base.EnterState();
        Time.timeScale = 0.0f;
    }

    public override void ExitState()
    {
        base.ExitState();
        Time.timeScale = 1.0f;
    }


    public void JumpToSettings()
    {
        context.SetActiveState(MenuController.MenuStates.Settings);
    }

    public void JumpToGame()
    {
        context.SetActiveState(MenuController.MenuStates.Gameplay);
    }
}
