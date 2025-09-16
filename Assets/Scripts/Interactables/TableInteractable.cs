using UnityEngine;

public class TableInteractable : MonoBehaviour, IInteractable {
    public string Prompt => "E — Check cosmetics";
    public MinigameController minigame;
    public DayController dayController;

    public void Interact(PlayerInteraction2D interactor) {
        if (dayController != null && dayController.CanUseMinigame()) {
            minigame.Open(dayController.CurrentSample);
        }
    }
}
