using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public Button pauseButton;   // Reference to the Button component
    private Text buttonText;     // Reference to the Text component of the Button
    public bool isPaused = false;

    public BaseMenu[] allMenus;

    public enum MenuStates
    {
        Main, Settings, Gameplay, LevelOne, LevelTwo
    }

    private Dictionary<MenuStates, BaseMenu> menuDictionary = new Dictionary<MenuStates, BaseMenu>();
    private BaseMenu currentState;
    private Stack<MenuStates> menuStack = new Stack<MenuStates>();

    // Start is called before the first frame update
    void Start()
    {
        if (allMenus == null) return;

        foreach (BaseMenu menu in allMenus)
        {
            if (menu == null) { continue; }

            //initalize our menu - which will store the context (menucontroller) as a depenency of the state - we will use dependancy injection for this - call an initalze function on the menu
            menu.InitState(this);

            if (menuDictionary.ContainsKey(menu.state)) continue;

            menuDictionary.Add(menu.state, menu);
        }

        foreach (MenuStates state in menuDictionary.Keys)
        {
            menuDictionary[state].gameObject.SetActive(false);
        }

        //Set our inital menu state
        SetActiveState(MenuStates.Main);

        // Get the Text component attached to the button
        buttonText = pauseButton.GetComponentInChildren<Text>();

        // Set initial text
        buttonText.text = "Pause";
    }

    public void SetActiveState(MenuStates newState, bool isJumpingBack = false)
    {
        if (!menuDictionary.ContainsKey(newState)) return;

        if (currentState != null)
        {
            currentState.ExitState();
            currentState.gameObject.SetActive(false);
        }

        currentState = menuDictionary[newState];

        currentState.gameObject.SetActive(true);
        currentState.EnterState();

        if (!isJumpingBack)
        {
            menuStack.Push(newState);
        }
    }

    public void JumpBack()
    {
        if (menuStack.Count <= 1)
        {
            SetActiveState(MenuStates.Main);
        }
        else
        {
            menuStack.Pop();
            SetActiveState(menuStack.Peek(), true);
        }
    }

    public void PauseGame()
    {
        // Toggle the paused state
        if (isPaused)
        {
            // If the game is paused, resume it
            Time.timeScale = 1f;
            isPaused = false;
            buttonText.text = "Pause";  // Change text to "Pause"
        }
        else
        {
            // If the game is playing, pause it
            Time.timeScale = 0f;
            isPaused = true;
            buttonText.text = "Play";   // Change text to "Play"
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
