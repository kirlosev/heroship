using System;
using System.Collections.Generic;
using HeroShip.Modules;
using UnityEngine;

namespace HeroShip.GridMap {
[Serializable]
public class ModuleGrid : ISerializationCallbackReceiver {
    public event Action<OnGridObjectChangedEventArgs> OnGridObjectChanged;

    public class OnGridObjectChangedEventArgs : EventArgs {
        public int X;
        public int Y;
    }

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
        for (var x = 0; x < slotsArray.GetLength(0); ++x) {
            for (var y = 0; y < slotsArray.GetLength(1); ++y) {
                serializedSlots.Add(new Package(x,y,slotsArray[x,y]));
            }
        }
    }

    public void OnAfterDeserialize() {
        slotsArray = new ModuleSlot[width, height];
        foreach (var s in serializedSlots) {
            slotsArray[s.x, s.y] = s.slot;
        }
    }
    #endregion

    public Vector2Int Size => new Vector2Int(width, height);
    public float CellSize => cellSize;

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    [SerializeField] private Vector3 originPosition;
    [SerializeField] private ModuleSlot[,] slotsArray;

    public ModuleGrid(int width, int height, float cellSize = 1f, Vector3 originPosition = default) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        slotsArray = new ModuleSlot[width, height];

        for (var x = 0; x < slotsArray.GetLength(0); ++x) {
            for (var y = 0; y < slotsArray.GetLength(1); ++y) {
                slotsArray[x, y] = new ModuleSlot(this, x, y);
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

    public void SetGridModuleSlot(int x, int y, ModuleSlot value) {
        if (IsGridPositionCorrect(x, y)) {
            slotsArray[x, y] = value;
            TriggerGridModuleSlotChangedEvent(x, y);
        }
    }

    public void TriggerGridModuleSlotChangedEvent(int x, int y) {
        OnGridObjectChanged?.Invoke(new OnGridObjectChangedEventArgs() {X = x, Y = y});
    }

    public void SetGridModuleSlot(Vector3 worldPosition, ModuleSlot value) {
        var (x, y) = GetGridPosition(worldPosition);
        SetGridModuleSlot(x, y, value);
    }

    public ModuleSlot GetGridModuleSlot(int x, int y) {
        if (IsGridPositionCorrect(x, y)) {
            return slotsArray[x, y];
        }

        return null;
    }

    public ModuleSlot GetGridModuleSlot(Vector3 worldPosition) {
        var (x, y) = GetGridPosition(worldPosition);
        return GetGridModuleSlot(x, y);
    }

    private bool IsGridPositionCorrect(int x, int y) {
        return x >= 0 && y >= 0 && x < width && y < height;
    }
}
}