using UnityEngine;

public class CheckDeskHandlerL7 : MonoBehaviour
{
    public GameObject ePromptUI;
    public GameObject checkPanel;
    public GameObject successImage;
    public GameObject failImage;

    bool inRange = false;

    void Start()
    {
        ePromptUI.SetActive(false);
        checkPanel.SetActive(false);
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            bool correct = ProductManagerL7.Instance.CheckAllDecisions();
            checkPanel.SetActive(!checkPanel.activeSelf);
            ePromptUI.SetActive(!checkPanel.activeSelf);
            successImage.SetActive(correct);
            failImage.SetActive(!correct);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) { inRange = true; ePromptUI.SetActive(true); }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) { inRange = false; ePromptUI.SetActive(false); checkPanel.SetActive(false); }
    }
}
