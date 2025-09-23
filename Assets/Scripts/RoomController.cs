using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour
{
    private bool canEnterLab = false;

    void Update()
    {
        // Если стоим у двери и нажали E
        if (canEnterLab && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Laboratory"); // Загружаем сцену лаборатории
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            canEnterLab = true;
            Debug.Log("Нажми E, чтобы войти в лабораторию");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            canEnterLab = false;
        }
    }
}
