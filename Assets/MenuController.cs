using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    //outlets
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject levelMenu;

    void Awake()
    {
        instance = this;
        Hide();
    }

    public void Show()
    {
        showMainMenu();
        gameObject.SetActive(true);
        Time.timeScale = 0;
        PlayerController.instance.isPaused = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        if (PlayerController.instance != null)
        {
            PlayerController.instance.isPaused = false;
        }
    }
 

    // Update is called once per frame
    void SwitchMenu(GameObject someMenu)
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        levelMenu.SetActive(false);

        someMenu.SetActive(true);
    }

    public void showMainMenu()
    {
        SwitchMenu(mainMenu);
    }

    public void showOptionsMenu()
    {
        SwitchMenu(optionsMenu);
    }

    public void showLevelMenu()
    {
        SwitchMenu(levelMenu);
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("Score");
        PlayerController.instance.score = 0;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Q4");
    }
}
