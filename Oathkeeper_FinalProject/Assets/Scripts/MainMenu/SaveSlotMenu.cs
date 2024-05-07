using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SaveSlotsMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;

    [Header("New Navigation")]
    [SerializeField] private Canvas newDialogue;
    [SerializeField] private TMP_InputField playerNameInput;

    [SerializeField] private int sceneIndex;


    [Header("Menu Buttons")]
    [SerializeField] private Button backButton;

    private SaveSlot[] saveSlots;

    private bool isLoadingGame = false;

    private void Awake() 
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot) 
    {
        // disable all buttons
        DisableMenuButtons();

        // update the selected profile id to be used for data persistence
        DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());

        if (!isLoadingGame) 
        {
            NewGameClicked();

        }else{
        //save gamebefore loading a new scene
        DataPersistenceManager.instance.SaveGame();        
        // load the scene - which will in turn save the game because of OnSceneUnloaded() in the DataPersistenceManager
        int sceneIndex = DataPersistenceManager.instance.GetIndex();
        //Debug.Log("" + sceneIndex);
        SceneManager.LoadSceneAsync(sceneIndex);
        }
    }

    public void OnBackClicked() 
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }
    public void onDeleteClicked(SaveSlot saveSlot)
    {
        DataPersistenceManager.instance.DeleteProfileData(saveSlot.GetProfileId());
        ActivateMenu(isLoadingGame);
    }

    public void ActivateMenu(bool isLoadingGame) 
    {
        // set this menu to be active
        this.gameObject.SetActive(true);

        // set mode
        this.isLoadingGame = isLoadingGame;

        // load all of the profiles that exist
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();

        // loop through each save slot in the UI and set the content appropriately
        GameObject firstSelected = backButton.gameObject;
        foreach (SaveSlot saveSlot in saveSlots) 
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            if (profileData == null && isLoadingGame) 
            {
                saveSlot.SetInteractable(false);
            }
            else 
            {
                saveSlot.SetInteractable(true);
                if (firstSelected.Equals(backButton.gameObject))
                {
                    firstSelected = saveSlot.gameObject;
                }
            }
        }

        // set the first selected button
        StartCoroutine(this.SetFirstSelected(firstSelected));
    }

    public void DeactivateMenu() 
    {
        this.gameObject.SetActive(false);
        mainMenu.ActivateMenu();
    }

    private void DisableMenuButtons() 
    {
        foreach (SaveSlot saveSlot in saveSlots) 
        {
            saveSlot.SetInteractable(false);
        }
        backButton.interactable = false;
    }
    public void NewGameClicked(){
        this.DeactivateMenu();
        mainMenu.gameObject.SetActive(false);
        newDialogue.gameObject.SetActive(true);
    }
    public void NewGameBack(){
        newDialogue.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);

    }
    public void OnConfirmNewGameClicked()
    {
        string playerName = playerNameInput.text;
        int sceneIndex = 1;
        // Check if the player name is not empty
        if (!string.IsNullOrEmpty(playerName))
        {
            // Game already started change player name
            // create a new game - which will initialize our data to a clean slate
            DataPersistenceManager.instance.NewGame(playerName, sceneIndex);
            DataPersistenceManager.instance.SaveGame();
            SceneManager.LoadSceneAsync(sceneIndex);
        }
        else
        {
            // Display a message to the player indicating that the player name cannot be empty
            Debug.Log("Player name cannot be empty.");
        }
    }
}