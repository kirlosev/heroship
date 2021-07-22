using UnityEngine;

namespace HeroShips.Modules {
[CreateAssetMenu(fileName = "New Module", menuName = "HeroShip/Module", order = 0)]
public class ModuleData : ScriptableObject {
    [SerializeField] private Vector2Int size;

    [SerializeField] private ModuleView moduleInst;
    public ModuleView ModuleInst => moduleInst;
}
}