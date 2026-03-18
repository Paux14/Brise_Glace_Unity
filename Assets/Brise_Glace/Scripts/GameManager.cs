using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
            RawQuestions = new List<string>(csvFile.text.Split(separators, System.StringSplitOptions.RemoveEmptyEntries));
        }
    }

    public string GetRandomPlayerName()
    {
        if (Players.Count == 0) return "Joueur A";
        return Players[Random.Range(0, Players.Count)];
    }

    // Cette fonction est l outil de transformation
    public string GetProcessedQuestion(string rawLine)
    {
        string result = rawLine;
        string tag = "{joueur}";

        // On boucle pour traiter chaque balise une par une
        while (Regex.IsMatch(result, tag, RegexOptions.IgnoreCase))
        {
            string pickedName = GetRandomPlayerName();

            // On remplace uniquement la premiere occurrence trouvee a chaque tour de boucle
            Regex regex = new Regex(Regex.Escape(tag), RegexOptions.IgnoreCase);
            result = regex.Replace(result, pickedName, 1);
        }
        return result;
    }
}