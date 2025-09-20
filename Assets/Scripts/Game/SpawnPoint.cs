using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Transform spawnPoint;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
        }
    }
}
