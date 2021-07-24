using HeroShip.Modules;
using UnityEngine;

namespace HeroShip.Ships {
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
                var gridObj = shipDataRef.grid.GetGridModuleSlot(x, y);
                if (gridObj == null) {
                    Debug.LogError($"There's no Module Slot on grid at position(x: {x}, y: {y})");
                    continue;
                }
                if (!gridObj.isActive) continue;

                var worldPosition = shipDataRef.grid.GetWorldPosition(x, y);
                worldPosition += (Vector3) Vector2.one * shipDataRef.grid.CellSize / 2f;

                if (gridObj.module != null) {
                    Instantiate(gridObj.module.ModuleInst, worldPosition, Quaternion.identity, modulesParent);
                }
                else {
                    Instantiate(slotViewInst, worldPosition, Quaternion.identity, slotsParent);
                }
            }    
        }
    }
}
}