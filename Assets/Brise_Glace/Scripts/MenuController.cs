using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TextMeshProUGUI playerListDisplay;

    public void AddPlayer()
    {
        // Nettoyage des espaces avant et apres la saisie
        string newName = nameInput.text.Trim();

        // Verification de validite et d unicite
        if (!string.IsNullOrWhiteSpace(newName) && !GameManager.Instance.Players.Contains(newName))
        {
            GameManager.Instance.Players.Add(newName);
            UpdatePlayerListUI();

            // Cette ligne reinitialise le champ textuel
            nameInput.text = "";

            // Cette ligne force la selection du champ texte
            // Cela permet denchainer la frappe au clavier sans recliquer sur l input
            nameInput.ActivateInputField();
        }
    }

    private void UpdatePlayerListUI()
    {
        if (GameManager.Instance.Players.Count == 0)
        {
            playerListDisplay.text = "Aucun joueur";
            return;
        }

        // Le caractere \n cree un saut de ligne pour l affichage vertical
        playerListDisplay.text = "Joueurs inscrits :\n- " + string.Join("\n- ", GameManager.Instance.Players);
    }

    public void StartGame()
    {
        if (GameManager.Instance.Players.Count >= 2 && GameManager.Instance.RawQuestions.Count > 0)
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            Debug.LogWarning("Il faut au minimum 2 joueurs pour lancer la partie.");
        }
    }
}