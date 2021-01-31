using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    public static SaveGame instance;

    private Save save;
    public Save SaveState { get => save; }
    public int[] Scores { get => scores; }
    public bool[] Unlocked { get => unlocked; }

    [SerializeField] int[] scores;
    [SerializeField] bool[] unlocked;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Ja existe um SaveGame manager");
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        save = new Save();

        scores = new int[SceneManager.sceneCountInBuildSettings - 1];
        unlocked = new bool[SceneManager.sceneCountInBuildSettings - 1];
        unlocked[0] = true;

        Load();

        DontDestroyOnLoad(gameObject);
    }

    public void AddScoreToLevel(int value)
    {
        if (value > Scores[SceneManager.GetActiveScene().buildIndex - 1])
        {
            Scores[SceneManager.GetActiveScene().buildIndex - 1] = value;
        }
    }

    public void AddNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            return;
        }

        unlocked[SceneManager.GetActiveScene().buildIndex] = true;
    }

    //Player
    public void Save()
    {
        StreamWriter fileWriter = new StreamWriter("Savegame.xml");
        XmlSerializer obj = new XmlSerializer(typeof(Save));

        save.scores = Scores;
        save.unlocked = Unlocked;

        obj.Serialize(fileWriter.BaseStream, save);
        fileWriter.Close();

        StreamReader fileReader = new StreamReader("Savegame.xml");
        string text = fileReader.ReadToEnd();
        fileReader.Close();
    }

    public void Load(string fileName = "Savegame.xml")
    {
        if (File.Exists(fileName))
        {
            StreamReader fileReader = new StreamReader(fileName);
            XmlSerializer obj = new XmlSerializer(typeof(Save));
            string text = fileReader.ReadToEnd();
            fileReader.Close();

            fileReader = new StreamReader(fileName);

            save = obj.Deserialize(fileReader.BaseStream) as Save;

            scores = save.scores;
            unlocked = save.unlocked;

            fileReader.Close();
        }
    }
}
