using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
    [Header("References UI")]
    [SerializeField] private TMP_InputField nameInput;
    
    [Header("Parametres Prefab")]
    [SerializeField] private GameObject playerTagPrefab; // Le prefab du joueur
    [SerializeField] private Transform contentContainer; // Le parent (ex: un Vertical Layout Group)

    // Liste locale pour gerer les objets UI cree (utile pour la suppression)
    [SerializeField] private List<GameObject> instantiatedTags = new List<GameObject>();

    public void AddPlayer()
    {
        string newName = nameInput.text.Trim();

        if (!string.IsNullOrWhiteSpace(newName) && !GameManager.Instance.Players.Contains(newName))
        {
            // 1. Ajouter la donnee au GameManager
            GameManager.Instance.Players.Add(newName);

            // 2. Creer le visuel (Instanciation)
            CreatePlayerTag(newName);

            // 3. Reset de l input
            nameInput.text = "";
            nameInput.ActivateInputField();
        }
    }

    private void CreatePlayerTag(string playerName)
    {
        // On cree l objet dans le container
        GameObject newTag = Instantiate(playerTagPrefab, contentContainer);
        
        // On recupere le texte dans lenfant du prefab pour changer le nom
        // Methode : GetComponentInChildren cherche le premier TMP trouve
        TextMeshProUGUI textComp = newTag.GetComponentInChildren<TextMeshProUGUI>();
        
        if (textComp != null)
        {
            textComp.text = playerName;
        }

        instantiatedTags.Add(newTag);
    }

    public void StartGame()
    {
        if (GameManager.Instance.Players.Count >= 2)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}