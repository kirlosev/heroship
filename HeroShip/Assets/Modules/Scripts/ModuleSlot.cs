using System;
using HeroShips.GridMap;
using UnityEngine;

namespace HeroShips.Modules {
[Serializable]
public class ModuleSlot {
    public bool isActive = true;
    public ModuleData module;
    
    [SerializeField] private int x;
    [SerializeField] private int y;
    // [SerializeField] private ModuleGrid grid;

    public ModuleSlot(ModuleGrid grid, int x, int y) {
        // this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public override string ToString() {
        return module?.name;
    }

    public void SetModuleData(ModuleData module) {
        this.module = module;
        // grid.TriggerGridObjectChangedEvent(x, y);
    }
}
}