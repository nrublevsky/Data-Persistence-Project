using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    public string inputedName;
    public InputField nameField;

    public StartMenuManager menuManager;

    public Text BestNameScoreText;

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

        

    }

    // Update is called once per frame
    void Update()
    {

        GetName(inputedName);

        if (SceneManager.GetActiveScene().name != "StartMenuScene")
        {
            BestNameScoreText = GameObject.Find("Best Score name").GetComponent<Text>();
            SetBestScore();
        }
    }

     public void GetName(string pName)
    {
     pName = nameField.text;
      inputedName = pName;
    
    }
    public void StartGame()
    {
        SavePlayerName(inputedName);

        SceneManager.LoadScene(0);
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int bestScore;
    }

    public void SavePlayerName(string inputName)
    {
        SaveData data = new SaveData();
        data.playerName = inputName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log("Saved Name" + data.playerName);
        Debug.Log("Current best score " + data.bestScore);
    }

    public void SaveBestScore(int bestScore)
    {
        SaveData data = new SaveData() { bestScore = bestScore };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void SetBestScore()
    {

        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);


        }


    }


}
