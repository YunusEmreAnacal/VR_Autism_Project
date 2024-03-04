using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject levels;
    public GameObject options;


    [Header("Main Menu Buttons")]
    public Button startButton;

    public Button Level1;
    public Button Level2;
    public Button Level3;
    public Button Level4;


    public Button optionButton;
    public Button quitButton;

    public List<Button> returnButtons;

    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();

        //Hook events
        Level1.onClick.AddListener(() => StartSelectedLevel(1));
        Level2.onClick.AddListener(() => StartSelectedLevel(2));
        Level3.onClick.AddListener(() => StartSelectedLevel(3));
        Level4.onClick.AddListener(() => StartSelectedLevel(4));

        startButton.onClick.AddListener(EnableStart);
        optionButton.onClick.AddListener(EnableOption);
        quitButton.onClick.AddListener(QuitGame);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnableMainMenu);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartSelectedLevel(int sceneNumber)
    {
        HideAll();
        SceneManager.LoadScene(sceneNumber);
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        levels.SetActive(false);
        options.SetActive(false);
        
    }

    public void EnableStart()
    {
        mainMenu.SetActive(false);
        levels.SetActive(true);
        options.SetActive(false);
       
    }
    public void EnableOption()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
        
    }
   
}
