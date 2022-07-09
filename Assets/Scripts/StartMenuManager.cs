using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    public string inputedName;
    public InputField nameField;

    public Text bestPlayer;

    public StartMenuManager menuManager;
    public MainManager mainManager;

    public string savedName;
    public int savedScore;


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
        bestPlayer = GameObject.Find("Best Player").GetComponent<Text>();
        LoadName();
        bestPlayer.text = "Best Score: " + savedName +" : "+ savedScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "StartMenuScene")
        {
            GetName();
        }
        else
        {

        }
    }

    public void GetName()
    {
        inputedName = nameField.text;
    }
    public void StartGame()
    {
        savedName = inputedName;
        InitialSaveName();
        SceneManager.LoadScene(0);
    }

    
    
    [System.Serializable]
    public class SaveData
    {
        public string playerName;
        public int bestScore;
    }

    public void InitialSaveName()
    {
        SaveData data = new SaveData();
        data.playerName = inputedName;
        data.bestScore = 0;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText("C:/Nick Stuff/GitHub/Data-Persistence-Project/savefile.json", json);
    }
    
    public void UpdateNameAndScore()
    {
        SaveData data = new SaveData();
        data.playerName = savedName;
        data.bestScore = savedScore;

       // string path = "C:/Nick Stuff/GitHub/Data - Persistence - Project/savefile.json";
        string json = JsonUtility.ToJson(data);

        File.WriteAllText("C:/Nick Stuff/GitHub/Data-Persistence-Project/savefile.json", json);
    }

   public void LoadName()
    {
        string path = "C:/Nick Stuff/GitHub/Data-Persistence-Project/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            savedName = data.playerName;
            savedScore = data.bestScore;

        }

    }


}
