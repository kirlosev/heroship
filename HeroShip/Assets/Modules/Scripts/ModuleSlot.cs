using System;
using HeroShip.GridMap;
using UnityEngine;

namespace HeroShip.Modules {
[Serializable]
public class ModuleSlot {
    public bool isActive = true;
    public ModuleData module;
    
    [SerializeField] private int x;
    [SerializeField] private int y;

    public ModuleSlot(ModuleGrid grid, int x, int y) {
        this.x = x;
        this.y = y;
    }

    public override string ToString() {
        return module?.name;
    }

    public void SetModuleData(ModuleData module) {
        this.module = module;
    }
}
}