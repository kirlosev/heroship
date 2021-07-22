using HeroShips.Modules;
using UnityEngine;

namespace HeroShips.Ships {
public class ShipView : MonoBehaviour {
    [SerializeField] private ShipData shipDataRef;
    [SerializeField] private SlotView slotViewInst;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private Transform modulesParent;
    [SerializeField] private SpriteRenderer viewRend;

    private void Awake() {
        viewRend.sprite = shipDataRef.ShipSprite;
    }
    
    private void Start() {
        GenerateModulesView();
    }
    
    private void GenerateModulesView() {
        for (var x = 0; x < shipDataRef.grid.Size.x; ++x) {
            for (var y = 0; y < shipDataRef.grid.Size.y; ++y) {
                var gridObj = shipDataRef.grid.GetGridObject(x, y);
                if (!gridObj.isActive) continue;

                var worldPosition = shipDataRef.grid.GetWorldPosition(x, y);
                worldPosition += (Vector3) Vector2.one * shipDataRef.grid.CellSize / 2f;

                if (gridObj.module != null) {
                    var m = Instantiate(gridObj.module.ModuleInst, worldPosition, Quaternion.identity, modulesParent);
                }
                else {
                    var s = Instantiate(slotViewInst, worldPosition, Quaternion.identity, slotsParent);
                }
            }    
        }
    }
}
}