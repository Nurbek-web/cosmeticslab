using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("SoundClick")]
    public AudioClip clickSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClick()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogWarning("No clickSound assigned to SFXPlayer!");
        }
    }
}
