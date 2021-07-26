using UnityEngine;

namespace HeroShip.Modules {
[CreateAssetMenu(fileName = "New Module Subcategory", menuName = "HeroShip/Module/Subcategory", order = 0)]
public class ModuleSubcategory : ScriptableObject {
    [SerializeField] private string title;
    public string Title => title;
    
    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;
}
}