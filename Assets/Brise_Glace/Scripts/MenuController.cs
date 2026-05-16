using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
    [Header("References UI")]
    [SerializeField] private TMP_InputField nameInput;

    [Header("Parametres Prefab")]
    [SerializeField] private GameObject playerTagPrefab;
    [SerializeField] private Transform contentContainer;

    [SerializeField] private List<GameObject> instantiatedTags = new List<GameObject>();

    public void AddPlayer()
    {
        string newName = nameInput.text.Trim();

        if (!string.IsNullOrWhiteSpace(newName) && !GameManager.Instance.Players.Contains(newName))
        {
            GameManager.Instance.Players.Add(newName);
            CreatePlayerTag(newName);

            nameInput.text = "";
            nameInput.ActivateInputField();
        }
    }

    private void CreatePlayerTag(string playerName)
    {
        GameObject newTag = Instantiate(playerTagPrefab, contentContainer);

        // On delegue la responsabilite totale au script du Prefab
        PlayerTag tagScript = newTag.GetComponent<PlayerTag>();
        if (tagScript != null)
        {
            tagScript.Setup(playerName, this);
        }

        instantiatedTags.Add(newTag);
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
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        if (GameManager.Instance.Players.Count >= 2)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}