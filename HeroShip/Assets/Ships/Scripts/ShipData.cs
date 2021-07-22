using HeroShips.GridMap;
using HeroShips.Modules;
using UnityEngine;

namespace HeroShips.Ships {
[CreateAssetMenu(fileName = "New Ship", menuName = "HeroShip/Ship", order = 0)]
public class ShipData : ScriptableObject {
    [SerializeField] private Sprite shipSprite;
    [SerializeField] private Vector2Int size = Vector2Int.one;

    [SerializeField] private bool[,] activeSlots; // HideInInspector

    public Grid<ModuleSlot> grid;
    public Sprite ShipSprite => shipSprite;
    public Vector2Int Size => size;

    private void OnValidate() {
        if (size.x < 1 || size.y < 1) return;
        
        if (grid == null) {
            CreateGrid();
        }
        
        if (activeSlots == null || size.x != activeSlots.GetLength(0) || size.y != activeSlots.GetLength(1)) {
            activeSlots = new bool[size.x, size.y];
        }
    }

    private void CreateGrid() {
        var cellSize = 1f;
        var halfSize = new Vector3(size.x/2f, size.y/2f);
        grid = new Grid<ModuleSlot>(size.x, size.y, (Grid<ModuleSlot> g, int x, int y) => new ModuleSlot(g, x, y), cellSize, -halfSize);
    }

    public void SetShipSprite(Sprite s) {
        shipSprite = s;
    }

    public void SetSize(Vector2Int s) {
        size = s;
    }

    public Grid<ModuleSlot> GetGrid() {
        if (grid == null) {
            CreateGrid();
        }
        return grid;
    }
}
}