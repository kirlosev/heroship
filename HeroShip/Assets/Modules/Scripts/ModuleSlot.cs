using System;
using System.Collections.Generic;
using UnityEngine;

namespace HeroShip.Modules {
[Serializable]
public class ModuleSlot {
    [SerializeField] private bool isActive = true;
    [SerializeField] private bool isOccupied;
    [SerializeField] private int x;
    [SerializeField] private int y;

    public bool IsActive => isActive;
    public bool IsOccupied => isOccupied;
    public Vector2Int GridPosition => new Vector2Int(x, y);

    public ModuleSlot(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public void Occupy() {
        isOccupied = true;
    }

    public void Clear() {
        isOccupied = false;
    }

    public void FlipActiveStatus() {
        isActive = !isActive;
    }
}
}