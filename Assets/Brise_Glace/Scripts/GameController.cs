using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

public class GameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionDisplay;

    private void Start()
    {
        DisplayNextQuestion();
    }

    public void DisplayNextQuestion()
    {
        if (GameManager.Instance.RawQuestions.Count == 0) return;

        int randomIndex = Random.Range(0, GameManager.Instance.RawQuestions.Count);
        string selectedQuestion = GameManager.Instance.RawQuestions[randomIndex];

        string formattedQuestion = ParseQuestionTags(selectedQuestion);

        questionDisplay.text = formattedQuestion;
    }

    private string ParseQuestionTags(string rawText)
    {
        string result = rawText;

        List<string> shuffledPlayers = GameManager.Instance.Players.OrderBy(x => Random.value).ToList();
        int playerIndex = 0;

        Regex regex = new Regex(Regex.Escape("{Joueur}"));

        while (result.Contains("{Joueur}") && playerIndex < shuffledPlayers.Count)
        {
<<<<<<< Updated upstream
            result = regex.Replace(result, shuffledPlayers[playerIndex], 1);
            playerIndex++;
=======
            case "question":
                backgroundImage.color = colorAnecdote;
                break;
            case "challenge":
                backgroundImage.color = colorChallenge;
                break;
            case "event":
                backgroundImage.color = new Color(0.5f, 0f, 0.5f); // Exemple violet
                break;
            default:
                backgroundImage.color = colorDefault;
                break;
>>>>>>> Stashed changes
        }

        return result;
    }
}
