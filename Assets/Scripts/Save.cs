using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class Save : MonoBehaviour
{
    public static Save Instance;

    public TextMeshProUGUI highscoreText;
    public int m_PointsOld;
    public TextMeshProUGUI playerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadGame();
    }
    // Start is called before the first frame update
    void Start()
    {
       if (highscoreText.text == "Highscore: 0")
       {
        highscoreText.text = "No highscore yet :(";
       }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (playerName.text.Length > 0)
        {
            SceneManager.LoadScene(1);
        }
    }
    
    [System.Serializable]
    class SaveData
    {
        public string h;
        public int m_PointsOld;
    }

    public void SaveGame()
    {
    SaveData data = new SaveData();
    data.h = playerName.text + "'s " + highscoreText.text;
    data.m_PointsOld = m_PointsOld;

    string json = JsonUtility.ToJson(data);
  
    File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
            {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            m_PointsOld = data.m_PointsOld;
            highscoreText.text = data.h;
            }
        }
}
