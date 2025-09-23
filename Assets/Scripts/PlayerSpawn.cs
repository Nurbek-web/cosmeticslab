using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        // Ищем игрока по тегу
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            // Перемещаем игрока в позицию PlayerSpawn
            player.transform.position = transform.position;
        }
    }
}
