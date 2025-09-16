using System.Collections.Generic;

[System.Serializable]
public class CosmeticSample {
    public string displayName;
    public List<string> declaredIngredients = new List<string>();
    public bool hasCertificate;

    // Для следующих дней (заготовки):
    public bool containsHydroquinone;
    public bool containsHeavyMetals;
    public bool containsHormoneLike;
    public bool containsSolventIssues;
}
