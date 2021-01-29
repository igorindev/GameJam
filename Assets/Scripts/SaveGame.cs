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

    [SerializeField] int[] scores;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Ja existe um SaveGame manager");
            return;
        }
        instance = this;

        save = new Save();

        scores = new int[SceneManager.sceneCountInBuildSettings - 1];
    }

    public void AddScoreToLevel(int value)
    {
        if (value > scores[SceneManager.GetActiveScene().buildIndex - 1])
        {
            scores[SceneManager.GetActiveScene().buildIndex - 1] = value;
        }
    }

    //Player
    public void Save()
    {
        StreamWriter fileWriter = new StreamWriter("Savegame.xml");
        XmlSerializer obj = new XmlSerializer(typeof(Save));

        save.scores = scores;

        Debug.Log("Jogo Salvo");

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

            fileReader.Close();
        }
    }
}
