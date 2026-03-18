using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("References UI")]
    [SerializeField] private TextMeshProUGUI questionText;

    private void Start()
    {
        // On affiche une premiere question au chargement de la scene
        DisplayNextQuestion();
    }

    public void DisplayNextQuestion()
    {
        // Securite : on verifie si la liste de questions n est pas vide
        if (GameManager.Instance.RawQuestions.Count == 0)
        {
            Debug.LogWarning("La liste de questions est vide.");
            return;
        }

        // 1. Tirage aleatoire d un index dans la liste brute
        int randomIndex = Random.Range(0, GameManager.Instance.RawQuestions.Count);
        string rawLine = GameManager.Instance.RawQuestions[randomIndex];

        // 2. On demande au GameManager de remplacer les balises {joueur}
        // La logique de traitement est centralisee dans le GameManager
        string processedQuestion = GameManager.Instance.GetProcessedQuestion(rawLine);

        // 3. Mise a jour du composant texte
        questionText.text = processedQuestion;
    }
}