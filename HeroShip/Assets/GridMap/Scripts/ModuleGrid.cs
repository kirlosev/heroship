using System;
using System.Collections.Generic;
using HeroShips.Modules;
using UnityEngine;

namespace HeroShips.GridMap {
[Serializable]
public class ModuleGrid : ISerializationCallbackReceiver {
    public event Action<OnGridObjectChangedEventArgs> OnGridObjectChanged;

    public class OnGridObjectChangedEventArgs : EventArgs {
        public int X;
        public int Y;
    }

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

        for (var x = 0; x < slotsArray.GetLength(0); ++x) {
            for (var y = 0; y < slotsArray.GetLength(1); ++y) {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    public Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public (int x, int y) GetGridPosition(Vector3 worldPosition) {
        var x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        var y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        return (x, y);
    }

    public void SetGridObject(int x, int y, ModuleSlot value) {
        if (IsGridPosCorrect(x, y)) {
            slotsArray[x, y] = value;
            TriggerGridObjectChangedEvent(x, y);
        }
    }

    public void TriggerGridObjectChangedEvent(int x, int y) {
        OnGridObjectChanged?.Invoke(new OnGridObjectChangedEventArgs() {X = x, Y = y});
    }

    public void SetGridObject(Vector3 worldPosition, ModuleSlot value) {
        (var x, var y) = GetGridPosition(worldPosition);
        SetGridObject(x, y, value);
    }

    public ModuleSlot GetGridObject(int x, int y) {
        if (IsGridPosCorrect(x, y)) {
            return slotsArray[x, y];
        }

        return default(ModuleSlot);
    }

    public ModuleSlot GetGridObject(Vector3 worldPosition) {
        (var x, var y) = GetGridPosition(worldPosition);
        return GetGridObject(x, y);
    }

    private bool IsGridPosCorrect(int x, int y) {
        return x >= 0 && y >= 0 && x < width && y < height;
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
}
}