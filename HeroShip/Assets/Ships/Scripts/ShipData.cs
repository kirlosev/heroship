using HeroShip.GridMap;
using HeroShip.Modules;
using UnityEngine;

namespace HeroShip.Ships {
[CreateAssetMenu(fileName = "New Ship", menuName = "HeroShip/Ship", order = 0)]
public class ShipData : ScriptableObject {
    [SerializeField] private Sprite shipSprite;
    [SerializeField] private Vector2Int size = Vector2Int.one;

    [SerializeField] public ModuleGrid grid;
    public Sprite ShipSprite => shipSprite;
    public Vector2Int Size => size;

    private void Awake() {
        if (grid == null) {
            CreateGrid();
        }
    }

    private void OnValidate() {
        if (size.x < 1) size.x = 1;
        if (size.y < 1) size.y = 1;
        
        if (grid == null || size.x != grid.Size.x || size.y != grid.Size.y) {
            CreateGrid();
        }
    }

    private void CreateGrid() {
        var cellSize = 1f;
        var halfSize = new Vector3(size.x/2f, size.y/2f);
        grid = new ModuleGrid(size.x, size.y, cellSize, -halfSize);
    }

    public void SetShipSprite(Sprite s) {
        shipSprite = s;
    }

    public void SetSize(Vector2Int s) {
        size = s;
        RegenerateGrid();
    }

    public ModuleGrid GetGrid() {
        if (grid == null) {
            RegenerateGrid();
        }
        return grid;
    }

    private void RegenerateGrid() {
        if (size.x < 1) size.x = 1;
        if (size.y < 1) size.y = 1;
        
        CreateGrid();
    }
}
}