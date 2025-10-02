using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    // Перетащите сюда из иерархии (Hierarchy)
    public GameObject ePromptUI;            // Изображение кнопки 'E'
    public GameObject mainInteractionPanel; // ГЛАВНАЯ панель (Interaction Panel UI)

    private bool isInRange = false;         // Флаг, что персонаж в области

    void Start()
    {
        if (ePromptUI != null) ePromptUI.SetActive(false);
        if (mainInteractionPanel != null) mainInteractionPanel.SetActive(false);
    }

    void Update()
    {
        // Проверяем, находится ли персонаж в области и была ли нажата клавиша 'E'
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Переключаем видимость ГЛАВНОЙ панели
            bool isPanelOpen = mainInteractionPanel.activeSelf;
            mainInteractionPanel.SetActive(!isPanelOpen);

            // Скрываем кнопку 'E', пока открыта панель
            ePromptUI.SetActive(isPanelOpen); // Если панель открывалась, теперь ее закрываем, и кнопка E появляется

            // Если панель закрывается, убедитесь, что все дочерние панели косметики тоже закрыты.
            if (isPanelOpen)
            {
                // (Опционально) Закрыть все дочерние панели, если они были открыты, 
                // чтобы избежать ошибок при повторном открытии. 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            if (ePromptUI != null) ePromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (ePromptUI != null) ePromptUI.SetActive(false);

            // Закрываем главную панель, если персонаж ушел
            if (mainInteractionPanel.activeSelf)
            {
                mainInteractionPanel.SetActive(false);
            }
        }
    }
}