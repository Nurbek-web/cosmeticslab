using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TutorialStep
{
    [TextArea(3, 10)]
    public string mainText;
    public bool isSidePanelActive;
}

public class TutorialManager : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject mainPanel;
    public TMP_Text mainTextComponent;
    public Button nextButton;
    public Button previousButton;
    public GameObject sidePanel;

    [Header("Tutorial Data")]
    public List<TutorialStep> steps;

    [Header("Typewriter Settings")]
    public float typingSpeed = 0.03f;

    private int currentStepIndex = 0;
    private Coroutine typingCoroutine;
    private bool isTyping = false;         // ���� ������
    private string currentFullText = "";   // ������ ����� �������� ����

    void Start()
    {
        if (steps == null || steps.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        nextButton.onClick.RemoveAllListeners();
        previousButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(OnNextPressed);
        previousButton.onClick.AddListener(PreviousStep);

        if (sidePanel != null)
            sidePanel.SetActive(false);

        gameObject.SetActive(true);

        UpdateUI();
    }

    // ---------- ������ ������ ----------
    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        currentFullText = sentence;

        nextButton.interactable = true; // ��������� ������ Next �� ����� ������
        previousButton.interactable = false;
        mainTextComponent.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            mainTextComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        previousButton.interactable = currentStepIndex > 0;
        nextButton.interactable = true;
    }

    // ---------- ���������� ���� ----------
    private void UpdateUI()
    {
        if (steps == null || steps.Count == 0)
        {
            Debug.LogError("������: ������ ����� ����!");
            gameObject.SetActive(false);
            return;
        }

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        TutorialStep currentStep = steps[currentStepIndex];
        if (mainTextComponent != null)
            typingCoroutine = StartCoroutine(TypeSentence(currentStep.mainText));

        if (sidePanel != null)
            sidePanel.SetActive(currentStep.isSidePanelActive);

        previousButton.interactable = false;
        nextButton.interactable = true;
    }

    // ---------- ��������� ������� NEXT ----------
    private void OnNextPressed()
    {
        // ���� ������ ��� ��� ? �������� ���� ����� �����
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            mainTextComponent.text = currentFullText;
            isTyping = false;

            previousButton.interactable = currentStepIndex > 0;
            nextButton.interactable = true;
        }
        else
        {
            NextStep();
        }
    }

    // ---------- �������� ----------
    public void NextStep()
    {
        if (currentStepIndex < steps.Count - 1)
        {
            if (sidePanel != null && sidePanel.activeSelf)
                sidePanel.SetActive(false);

            currentStepIndex++;
            UpdateUI();
        }
        else if (currentStepIndex == steps.Count - 1)
        {
            Debug.Log("������ ��������.");
            if (mainTextComponent != null)
                mainTextComponent.text = "";

            if (mainPanel != null)
                mainPanel.SetActive(false);

            if (sidePanel != null)
                sidePanel.SetActive(false);
        }
    }

    public void PreviousStep()
    {
        if (currentStepIndex > 0)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            currentStepIndex--;
            UpdateUI();
        }
    }
}
