using UnityEngine;
using TMPro;

public class PlayerTag : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameTextDisplay;

    private MenuController myController;
    private string myPlayerName;

    public void Setup(string playerName, MenuController controller)
    {
        myPlayerName = playerName;
        myController = controller;

        // Mise a jour du texte localement dans le Prefab
        if (nameTextDisplay != null)
        {
            nameTextDisplay.text = playerName;
        }
    }

    public void OnCrossButtonClicked()
    {
        if (myController != null)
        {
            myController.RemovePlayer(myPlayerName, this.gameObject);
        }
    }
}