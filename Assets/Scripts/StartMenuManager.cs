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

        //SaveName(inputedName);

        if (SceneManager.GetActiveScene().name != "Start Menu Scene")
        {


        }
    }

    // public void SaveName(string pName)
    //{
    // pName = nameField.text;
    //  inputedName = pName;
    //
    //}
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
    }

    void SetBestScore(string bestName, int currentBestScore)
    {

        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestName = data.playerName;
            currentBestScore = data.bestScore;
        }
         
        BestNameScoreText.text = "Best Score: " + bestName + " : " + currentBestScore;
    }
}
