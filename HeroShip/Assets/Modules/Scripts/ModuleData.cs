using UnityEngine;

namespace HeroShip.Modules {
[CreateAssetMenu(fileName = "New Module", menuName = "HeroShip/Module/Module", order = 0)]
public class ModuleData : ScriptableObject {
    [SerializeField] private string title;
    public string Title => title;
    
    [SerializeField] private Vector2Int size;
    public Vector2Int Size => size;

    [SerializeField] private ModuleView moduleInst;
    public ModuleView ModuleInst => moduleInst;

    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;

    [SerializeField] private ModuleCategory category;
    public ModuleCategory Category => category;

    [SerializeField] private ModuleSubcategory subcategory;
    public ModuleSubcategory Subcategory => subcategory;
}
}