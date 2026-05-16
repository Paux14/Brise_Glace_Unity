using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine.UI; // Necessaire pour manipuler le composant Image

public class GameController : MonoBehaviour
{
    [Header("References UI")]
    [SerializeField] private TextMeshProUGUI questionDisplay;
    [SerializeField] private Image backgroundImage; // Image de fond a colorer

    [Header("Couleurs des Categories")]
    [SerializeField] private Color colorAnecdote = Color.blue;
    [SerializeField] private Color colorChallenge = Color.green;
    [SerializeField] private Color colorDefault = Color.gray;

    private void Start()
    {
        DisplayNextQuestion();
    }

    public void DisplayNextQuestion()
    {
        // Verification de securite pour eviter les erreurs d index
        if (GameManager.Instance.RawQuestions.Count == 0) return;

        // Selection aleatoire de la ligne brute dans la liste du GameManager
        int randomIndex = Random.Range(0, GameManager.Instance.RawQuestions.Count);
        string selectedLine = GameManager.Instance.RawQuestions[randomIndex];

        // Separation du prefixe et de la question via le caractere de separation
        string[] parts = selectedLine.Split(';');

        if (parts.Length >= 2)
        {
            string category = parts[0].ToLower().Trim();
            string rawQuestion = parts[1];

            // 1. Mise a jour de la couleur de fond selon la categorie extracte
            ApplyCategoryColor(category);

            // 2. Traitement des balises de joueurs sur le texte de la question
            string formattedQuestion = ParseQuestionTags(rawQuestion);

            // 3. Affichage du texte final genere
            questionDisplay.text = formattedQuestion;
        }
        else
        {
            // Securite si la ligne du fichier utilise un format incorrect
            backgroundImage.color = colorDefault;
            questionDisplay.text = ParseQuestionTags(selectedLine);
        }
    }

    private void ApplyCategoryColor(string category)
    {
        // Met a jour le composant graphique selon le prefixe detecte
        switch (category)
        {
            case "question":
            case "anecdote":
                backgroundImage.color = colorAnecdote;
                break;
            case "challenge":
                backgroundImage.color = colorChallenge;
                break;
            case "event":
                backgroundImage.color = new Color(0.5f, 0f, 0.5f); // Violet
                break;
            default:
                backgroundImage.color = colorDefault;
                break;
        }
    }

    private string ParseQuestionTags(string rawText)
    {
        string result = rawText;
        string tag = "{Joueur}";

        // Melange de la liste des joueurs avec LINQ pour eviter la repetition du meme nom
        List<string> shuffledPlayers = GameManager.Instance.Players.OrderBy(x => Random.value).ToList();
        int playerIndex = 0;

        // Configuration du moteur Regex avec option pour ignorer la casse (minuscule/majuscule)
        Regex regex = new Regex(Regex.Escape(tag), RegexOptions.IgnoreCase);

        // Boucle de remplacement sequentiel basee sur le nombre de tags et de joueurs disponibles
        while (Regex.IsMatch(result, tag, RegexOptions.IgnoreCase) && playerIndex < shuffledPlayers.Count)
        {
            // Remplacement de la premiere occurrence uniquement (limite fixee a 1)
            result = regex.Replace(result, shuffledPlayers[playerIndex], 1);
            playerIndex++;
        }

        return result;
    }
}