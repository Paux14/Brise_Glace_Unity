using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
<<<<<<< Updated upstream
    [SerializeField] private TextMeshProUGUI playerListDisplay;
=======

    [Header("Parametres Prefab")]
    [SerializeField] private GameObject playerTagPrefab;
    [SerializeField] private Transform contentContainer;

    [SerializeField] private List<GameObject> instantiatedTags = new List<GameObject>();
>>>>>>> Stashed changes

    public void AddPlayer()
    {
        // Nettoyage des espaces avant et apres la saisie
        string newName = nameInput.text.Trim();

        // Verification de validite et d unicite
        if (!string.IsNullOrWhiteSpace(newName) && !GameManager.Instance.Players.Contains(newName))
        {
            GameManager.Instance.Players.Add(newName);
<<<<<<< Updated upstream
            UpdatePlayerListUI();

            // Cette ligne reinitialise le champ textuel
=======
            CreatePlayerTag(newName);

>>>>>>> Stashed changes
            nameInput.text = "";

            // Cette ligne force la selection du champ texte
            // Cela permet denchainer la frappe au clavier sans recliquer sur l input
            nameInput.ActivateInputField();
        }
    }

    private void UpdatePlayerListUI()
    {
<<<<<<< Updated upstream
        if (GameManager.Instance.Players.Count == 0)
        {
            playerListDisplay.text = "Aucun joueur";
            return;
=======
        GameObject newTag = Instantiate(playerTagPrefab, contentContainer);

        // On delegue la responsabilite totale au script du Prefab
        PlayerTag tagScript = newTag.GetComponent<PlayerTag>();
        if (tagScript != null)
        {
            tagScript.Setup(playerName, this);
>>>>>>> Stashed changes
        }

        // Le caractere \n cree un saut de ligne pour l affichage vertical
        playerListDisplay.text = "Joueurs inscrits :\n- " + string.Join("\n- ", GameManager.Instance.Players);
    }

    public void RemovePlayer(string playerName, GameObject tagVisual)
    {
        if (GameManager.Instance.Players.Contains(playerName))
        {
            GameManager.Instance.Players.Remove(playerName);
        }

        if (instantiatedTags.Contains(tagVisual))
        {
            instantiatedTags.Remove(tagVisual);
        }

        Destroy(tagVisual);
    }

    public void StartGame()
    {
<<<<<<< Updated upstream
        if (GameManager.Instance.Players.Count >= 2 && GameManager.Instance.RawQuestions.Count > 0)
=======
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        if (GameManager.Instance.Players.Count >= 2)
>>>>>>> Stashed changes
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            Debug.LogWarning("Il faut au minimum 2 joueurs pour lancer la partie.");
        }
    }
}