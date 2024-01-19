using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewLoadSaveUI : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button NewGameButton;
    [SerializeField] private Button LoadGameButton;

    //This is for cases where there is NO DATA AVAILABLE
    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            LoadGameButton.interactable = false;
        }
    }

    //For the buttons
    public void NewGameClick()
    {
        /* create new game
        DataPersistenceManager.instance.NewGame();
        // load the gameplay scene - which will in turn save the game because of
        // OnSceneUnloaded() in the DPM
        SceneManager.LoadSceneAsync("Hospital"); //relocate to the hub area
        !!! Old Script !!!
        */

        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    public void OnLoadGameClicked()
    {
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    public void ContinueGameClick()
    {
        // save the game anytime before loading a new scene
        DataPersistenceManager.instance.SaveGame();
        // load the gameplay scene - which will in turn save the game because of
        // OnSceneUnloaded() in the DPM
        SceneManager.LoadSceneAsync("Hospital");
        // modifiable with a script telling
        // before this line to load a certain scene with the data saved
        // i.e.: WhatLevel = data.WhatLevel OR WhatLevelName = data.WhatLevelName
        // Note that WhatLevel must be in scene order starting from 0
    }

    public void SaveGameClick()
    {
        DataPersistenceManager.instance.SaveGame();
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
