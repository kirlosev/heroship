using HeroShips.GridMap;
using UnityEngine;

namespace HeroShips.Modules {
public class ModuleSlot {
    public bool isActive = true;
    public ModuleData module;
    
    private int x;
    private int y;
    private Grid<ModuleSlot> grid;

    public ModuleSlot(Grid<ModuleSlot> grid, int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public override string ToString() {
        return module?.name;
    }

    public void SetModuleData(ModuleData module) {
        this.module = module;
        grid.TriggerGridObjectChangedEvent(x, y);
    }
}
}