using UnityEngine;

namespace HeroShip.Modules {
[CreateAssetMenu(fileName = "New Module Category", menuName = "HeroShip/Module/Category", order = 0)]
public class ModuleCategory : ScriptableObject {
    [SerializeField] private string title;
    public string Title => title;
    
    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;
    
    [SerializeField] private ModuleSubcategory[] subcategories;
    public ModuleSubcategory[] Subcategories => subcategories;
}
}