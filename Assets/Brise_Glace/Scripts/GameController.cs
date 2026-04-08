using UnityEngine;
using UnityEngine.UI; // Necessaire pour manipuler le composant Image
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("References UI")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Image backgroundImage; // Glisser l image du fond ici

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
        if (GameManager.Instance.RawQuestions.Count == 0) return;

        // 1. Tirage de la ligne brute (ex: "challenge;Fais un duel avec {joueur}")
        int index = Random.Range(0, GameManager.Instance.RawQuestions.Count);
        string rawLine = GameManager.Instance.RawQuestions[index];

        // 2. Separation du prefixe et de la question
        // On separe la chaine au premier caractere ';' rencontre
        string[] parts = rawLine.Split(';');

        if (parts.Length >= 2)
        {
            string category = parts[0].ToLower().Trim();
            string rawQuestion = parts[1];

            // 3. Changement de couleur selon le prefixe
            ApplyCategoryColor(category);

            // 4. Traitement des balises {joueur} sur le texte uniquement
            questionText.text = GameManager.Instance.GetProcessedQuestion(rawQuestion);
        }
        else
        {
            // Cas de secours si la ligne n a pas de prefixe ou de ';'
            backgroundImage.color = colorDefault;
            questionText.text = GameManager.Instance.GetProcessedQuestion(rawLine);
        }
    }

    private void ApplyCategoryColor(string category)
    {
        switch (category)
        {
            case "anecdote":
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
        }
    }
}