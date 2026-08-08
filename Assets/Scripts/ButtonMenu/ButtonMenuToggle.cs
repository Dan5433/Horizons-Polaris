using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonMenuToggle : MonoBehaviour
{
    [SerializeField]
    public Button toggleButton;
    public GameObject buttonPanel; // parent object holding the 5 buttons
    public List<Button> optionButtons = new List<Button>(); // assign the 5 buttons in Inspector
    public List<string> optionLabels = new List<string> { "Option 1", "Option 2", "Option 3", "Option 4", "Option 5" };

    private bool isOpen = false;

    void Start()
    {
        // set each button's text from the labels list
        for (int i = 0; i < optionButtons.Count; i++)
        {
            if (i < optionLabels.Count)
            {
                TMP_Text buttonText = optionButtons[i].GetComponentInChildren<TMP_Text>();
                if (buttonText != null)
                    buttonText.text = optionLabels[i];
            }
        }

        // hide panel at start
        buttonPanel.SetActive(false);

        // define toggle button with function
        toggleButton.onClick.AddListener(ToggleMenu);
    }

    // toggle
    void ToggleMenu()
    {
        isOpen = !isOpen;
        buttonPanel.SetActive(isOpen);
    }
}
