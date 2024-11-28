using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class Persistance : MonoBehaviour
{
    public static Persistance Instance;

    public static string playerName = "Player";
    public static int score = 0;
    public static string highScorePlayerName;
    public static int highScore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null)        
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void StartGame()
    {
        // Get the player TMP_InputField "Player Name" value
        TMP_InputField playerNameInputField = GameObject.Find("Player Name").GetComponent<TMP_InputField>();

        if (!string.IsNullOrWhiteSpace(playerNameInputField.text))
        {
            playerName = playerNameInputField.text;
            Debug.Log("Player Name: " + playerName);
        }

        SceneManager.LoadScene(1);
    }

    public void SaveHighScore(int score)
    {
        highScore = score;
        highScorePlayerName = playerName;

        // Save the high score
        SaveData data = new SaveData();
        data.playerName = highScorePlayerName;
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    private void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            highScorePlayerName = data.playerName;
        }
    }

    class SaveData
    {
        public string playerName;
        public int highScore;
    }
}
