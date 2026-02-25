using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<string> Players { get; private set; } = new List<string>();
    public List<string> RawQuestions { get; private set; } = new List<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        LoadQuestionsFromCSV();
    }

    private void LoadQuestionsFromCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("questions");

        if (csvFile != null)
        {
            string[] separators = new string[] { "\r\n", "\n" };
            string[] lines = csvFile.text.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
            RawQuestions = new List<string>(lines);
        }
        else
        {
            Debug.LogError("Fichier introuvable dans Resources.");
        }
    }
}