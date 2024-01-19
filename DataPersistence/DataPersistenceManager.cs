using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool initializeDataIfNull = false;
    [SerializeField] private bool disableDPM = false;
    [SerializeField] private bool overrideSelectedProfileId = false;
    [SerializeField] private string testSelectedProfileId = "test";

    [Header("File Storage Config")]
    [SerializeField] private string fileName;


    private GameData gameData;
    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler dataHandler;
    private string selectedProfileId = "";

    public static DataPersistenceManager instance { get; private set; }

    public void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More DPM is in the scene, deleting the newest DPM/s");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (disableDPM)
        {
            Debug.LogWarning("Data Persistence Manager is currently disabled!");
        }

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        this.selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();

        if (overrideSelectedProfileId)
        {
            this.selectedProfileId = testSelectedProfileId;
            Debug.LogWarning("Overriding selected Profile Id with a test profile Id named: " + testSelectedProfileId);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded Called");
        this._dataPersistenceObjects = FindAllDataPersistenceObjects();
        Debug.Log("All Objects Found");
        LoadGame();
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        // update the profile to use for saving and loading
        this.selectedProfileId = newProfileId;

        // load the game, which will use that profile, updating our game data accordingly
        LoadGame();
    }

    public void SaveGame()
    {
        // return right away if data persistence is disabled
        if (disableDPM)
        {
            return;
        }
        // to check for problems, use breakpoints then press F10 to look for any missing data points


        // if we don't have any data to save, log a warning
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. A new game needs to be started");
            return;
        }

        // pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        // timestamp the data so we know when the game was saved
        gameData.lastUpdated = System.DateTime.Now.ToBinary();

        // save that data to a file using the file handler
        dataHandler.Save(gameData, selectedProfileId);
    }

    public void LoadGame()
    {
        // return right away if data persistence is disabled
        if (disableDPM)
        {
            return;
        }

        //Load any saved data from a file using a file handler
        this.gameData = dataHandler.Load(selectedProfileId);

        //start a new game IF the data is null and we've configured to initialize data for debugging purposes
        if (this.gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        //if no data to load, don't continue
        if (this.gameData == null)
        {
            Debug.Log("No data found. Try making a new game before data can load");
            return;
        }
        //push loaded data to all other scripts that need it

        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
            Debug.Log("Data Loaded");
        }
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    //Another Test Script
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        // FindObjectsofType has an optional boolean parameter to find inactive GameObjects
        IEnumerable<IDataPersistence> _dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();
        return new List<IDataPersistence>(_dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
}
