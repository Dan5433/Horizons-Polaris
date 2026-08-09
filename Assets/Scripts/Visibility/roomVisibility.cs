using UnityEngine;

public class cakeVisibility: MonoBehaviour
{
    [SerializeField] ButtonMenuManager buttonMenuManager;

    private string lastAns;

    void Start()
    {
        refreshAns();
    }

    void Update()
    {
        if (buttonMenuManager.SelectedAnswer != lastAns)
        {
            refreshAns();
        }
    }

    public void refreshAns()
    {
        lastAns = buttonMenuManager.SelectedAnswer;

        foreach (Transform child in transform)
        {
            // child's name must exactly match one of the answer keys
            // e.g. "The Horizon", "A Cake", "A Blahaj", "The Stars", "Music"
            bool shouldShow = child.name == lastAns;
            child.gameObject.SetActive(shouldShow);
        }
    }
}