using System;
using System.Collections.Generic;
using HeroShip.Modules;
using UnityEngine;

namespace HeroShip.GridMap {
[Serializable]
public class ModuleGrid : ISerializationCallbackReceiver {
    public event Action OnModuleAdded;

    #region Multi-Dimensional Array Serialization

    [SerializeField, HideInInspector] private List<Package> serializedSlots;

    [Serializable]
    struct Package {
        public int x;
        public int y;
        public ModuleSlot slot;

        public Package(int x, int y, ModuleSlot slot) {
            this.x = x;
            this.y = y;
            this.slot = slot;
        }
    }

    public void OnBeforeSerialize() {
        serializedSlots = new List<Package>();
        for (var x = 0; x < moduleSlots.GetLength(0); ++x) {
            for (var y = 0; y < moduleSlots.GetLength(1); ++y) {
                serializedSlots.Add(new Package(x, y, moduleSlots[x, y]));
            }
        }
    }

    public void OnAfterDeserialize() {
        moduleSlots = new ModuleSlot[width, height];
        foreach (var s in serializedSlots) {
            moduleSlots[s.x, s.y] = s.slot;
        }
    }

    #endregion

    public Vector2Int Size => new Vector2Int(width, height);
    public float CellSize => cellSize;

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    [SerializeField] private Vector3 originPosition;
    [SerializeField] private ModuleSlot[,] moduleSlots;
    [SerializeField] private List<ModuleOnShipInfo> modulesOnShip = new List<ModuleOnShipInfo>();

    public List<ModuleOnShipInfo> ModulesOnShip => modulesOnShip;

    [Serializable]
    public class ModuleOnShipInfo {
        public ModuleData moduleData;
        public Vector2Int[] slotsOccupied;
        public ModuleView moduleOnScene;

        public int x => slotsOccupied[0].x;
        public int y => slotsOccupied[0].y;
    }

    public ModuleGrid(int width, int height, float cellSize = 1f, Vector3 originPosition = default) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        moduleSlots = new ModuleSlot[width, height];

        for (var x = 0; x < moduleSlots.GetLength(0); ++x) {
            for (var y = 0; y < moduleSlots.GetLength(1); ++y) {
                moduleSlots[x, y] = new ModuleSlot(x, y);
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public (int x, int y) GetGridPosition(Vector3 worldPosition) {
        var x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        var y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        return (x, y);
    }

    public void SetModule(int x, int y, ModuleData moduleData) {
        if (!IsGridPositionCorrect(x, y)) return;

        var moduleForShip = new ModuleOnShipInfo();
        moduleForShip.moduleData = moduleData;
        moduleForShip.slotsOccupied = new Vector2Int[moduleData.Size.x * moduleData.Size.y];

        for (var xx = 0; xx < moduleData.Size.x; ++xx) {
            for (var yy = 0; yy < moduleData.Size.y; ++yy) {
                var xPos = x + xx;
                var yPos = y + yy;

                if (moduleSlots[xPos, yPos].IsOccupied) {
                    RemoveModule(xPos, yPos);
                }

                moduleSlots[xPos, yPos].Occupy();
                moduleForShip.slotsOccupied[yy + xx * moduleData.Size.y] = new Vector2Int(xPos, yPos);
            }
        }

        modulesOnShip.Add(moduleForShip);

        OnModuleAdded?.Invoke();
    }

    public void RemoveModule(int x, int y) {
        if (!IsGridPositionCorrect(x, y)) return;

        ModuleOnShipInfo moduleToClean = null;

        foreach (var m in modulesOnShip) {
            foreach (var s in m.slotsOccupied) {
                if (s.x == x && s.y == y) {
                    moduleToClean = m;
                    break;
                }
            }

            if (moduleToClean != null) {
                break;
            }
        }

        if (moduleToClean == null) return;

        foreach (var s in moduleToClean.slotsOccupied) {
            moduleSlots[s.x, s.y].Clear();
        }

        moduleToClean.moduleOnScene.Remove();
        modulesOnShip.Remove(moduleToClean);
    }

    public void SetModule(Vector3 worldPosition, ModuleData value) {
        var (x, y) = GetGridPosition(worldPosition);
        SetModule(x, y, value);
    }

    public ModuleSlot GetGridModuleSlot(int x, int y) {
        return IsGridPositionCorrect(x, y) ? moduleSlots[x, y] : null;
    }

    public ModuleSlot GetGridModuleSlot(Vector3 worldPosition) {
        var (x, y) = GetGridPosition(worldPosition);
        return GetGridModuleSlot(x, y);
    }

    private bool IsGridPositionCorrect(int x, int y) {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    public bool CheckFitting(Vector2Int gridPosition, Vector2Int size) {
        if (gridPosition.x + size.x > width || gridPosition.y + size.y > height)
            return false;

        for (var x = 0; x < size.x; ++x) {
            for (var y = 0; y < size.y; ++y) {
                var gridX = gridPosition.x + x;
                var gridY = gridPosition.y + y;

                if (!moduleSlots[gridX, gridY].IsActive) {
                    return false;
                }
            }
        }

        return true;
    }
}
}