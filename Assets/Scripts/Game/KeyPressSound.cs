using UnityEngine;

public class KeyPressSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip pressSound;

    void Awake()
    {
        // Автоматически получаем компонент AudioSource
        audioSource = GetComponent<AudioSource>();

        // Если его нет — добавляем
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.spatialBlend = 0f; // 2D звук
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pressSound != null)
            {
                audioSource.PlayOneShot(pressSound);
            }
            else
            {
                Debug.LogWarning("Press sound not assigned!");
            }
        }
    }
}
