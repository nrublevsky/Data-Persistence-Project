using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    public string inputedName;
    public InputField nameField;

    public Text bestPlayerText;

    public StartMenuManager menuManager;
    public MainManager mainManager;

    public string savedName;
    public string bestPlayerName;
    public int savedScore;
    public int bestScore;

    public int numberOfGames;


    // Start is called before the first frame update
    private void Awake()
    {
        if (menuManager != null)
        {
            Destroy(gameObject);
        }

        menuManager = this;
        DontDestroyOnLoad(gameObject); 
    }

    void Start()
    {      
        bestPlayerText = GameObject.Find("Best Player").GetComponent<Text>();
        LoadFromFile();
        bestPlayerText.text = "Best Score: " + bestPlayerName +" : "+ bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "StartMenuScene")
        {
            GetName();
            bestPlayerText.text = "Best Score: " + bestPlayerName + " : " + bestScore;
        }
        
    }

    public void GetName()
    {
        inputedName = nameField.text;
    }
    public void StartGame()
    {
        savedName = inputedName;
        savedScore = 0;
        InitialSaveName();
        SceneManager.LoadScene(0);
        numberOfGames++;
    }

    public void ClearBestScore()
    {
        ClearScore();
    }

    public void KillGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else   
        Application.Quit();
#endif
    }

    
    
    [System.Serializable]
    public class SaveData
    {
        public string bestPlayer;
        public int bestScore;
        public string currentName;
        public int currentScore;
    }

    public void InitialSaveName()
    {
        SaveData data = new SaveData();
        if (savedName != data.currentName)
        {
            data.currentName = savedName;
        }
        data.currentScore = 0;
        data.bestPlayer = bestPlayerName;
        data.bestScore = bestScore;
              

        string json = JsonUtility.ToJson(data);

        File.WriteAllText("C:/Nick Stuff/GitHub/Data-Persistence-Project/savefile.json", json);
    }
    
    public void ClearScore()
    {
        SaveData data = new SaveData();
        
        bestScore = data.bestScore = 0;
        bestPlayerName = data.bestPlayer = null;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText("C:/Nick Stuff/GitHub/Data-Persistence-Project/savefile.json", json);
    }
    public void UpdateWhenBeaten()
    {
        
        SaveData data = new SaveData();
        bestPlayerName = savedName;
        data.bestPlayer = bestPlayerName;
        data.currentName = data.bestPlayer;
        bestScore = savedScore;
        data.bestScore = bestScore;
        data.currentScore = data.bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText("C:/Nick Stuff/GitHub/Data-Persistence-Project/savefile.json", json);
    }

    public void UpdateNormal()
    {
        SaveData data = new SaveData();
        data.currentName = savedName;
        data.currentScore = savedScore;
        data.bestPlayer = bestPlayerName;
        data.bestScore = bestScore;

        // string path = "C:/Nick Stuff/GitHub/Data - Persistence - Project/savefile.json";
        string json = JsonUtility.ToJson(data);

        File.WriteAllText("C:/Nick Stuff/GitHub/Data-Persistence-Project/savefile.json", json);
    }

    public void LoadFromFile()
    {
        string path = "C:/Nick Stuff/GitHub/Data-Persistence-Project/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            savedName = data.currentName;
            savedScore = data.currentScore;
            bestPlayerName = data.bestPlayer;
            bestScore = data.bestScore; 
        }
    }


}
