using UnityEngine;

namespace HeroShip.Modules {
[CreateAssetMenu(fileName = "New Module", menuName = "HeroShip/Module", order = 0)]
public class ModuleData : ScriptableObject {
    [SerializeField] private Vector2Int size;
    public Vector2Int Size => size;

    [SerializeField] private ModuleView moduleInst;
    public ModuleView ModuleInst => moduleInst;

    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;
}
}