using UnityEngine;

public class Inventory : MonoBehaviour {
    public CosmeticSample heldSample;

    public bool HasSample => heldSample != null;

    public void Put(CosmeticSample s) => heldSample = s;
    public CosmeticSample Take() {
        var s = heldSample;
        heldSample = null;
        return s;
    }
}
