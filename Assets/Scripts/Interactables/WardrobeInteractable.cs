using UnityEngine;

public class WardrobeInteractable : MonoBehaviour, IInteractable {
    public string Prompt => _wearing ? "E — Take off your robe" : "E — Change clothes";
    public DayController dayController;

    bool _wearing = false;

    public void Interact(PlayerInteraction2D interactor) {
        if (!_wearing) {
            _wearing = true;
            dayController?.OnWoreCoat();
        } else {
            dayController?.OnTryUndress();
        }
    }
}

