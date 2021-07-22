using System;
using UnityEngine;

namespace HeroShips.GridMap {
public class Grid<TGridObject> {
    public event Action<OnGridObjectChangedEventArgs> OnGridObjectChanged;

    public class OnGridObjectChangedEventArgs : EventArgs {
        public int X;
        public int Y;
    }

    public Vector2Int Size => new Vector2Int(width, height);
    public float CellSize => cellSize;

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;

    public Grid(int width, int height, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject, 
        float cellSize = 1f, Vector3 originPosition = default) {
        
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        for (var x = 0; x < gridArray.GetLength(0); ++x) {
            for (var y = 0; y < gridArray.GetLength(1); ++y) {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

        for (var x = 0; x < gridArray.GetLength(0); ++x) {
            for (var y = 0; y < gridArray.GetLength(1); ++y) {
                Debug.Log($"[{x},{y}] is {gridArray[x, y]}");

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

    public void SetGridObject(int x, int y, TGridObject value) {
        if (IsGridPosCorrect(x, y)) {
            gridArray[x, y] = value;
            TriggerGridObjectChangedEvent(x, y);
        }
    }

    public void TriggerGridObjectChangedEvent(int x, int y) {
        OnGridObjectChanged?.Invoke(new OnGridObjectChangedEventArgs() {X = x, Y = y});
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject value) {
        (var x, var y) = GetGridPosition(worldPosition);
        SetGridObject(x, y, value);
    }

    public TGridObject GetGridObject(int x, int y) {
        if (IsGridPosCorrect(x, y)) {
            return gridArray[x, y];
        }

        return default(TGridObject);
    }

    public TGridObject GetGridObject(Vector3 worldPosition) {
        (var x, var y) = GetGridPosition(worldPosition);
        return GetGridObject(x, y);
    }

    private bool IsGridPosCorrect(int x, int y) {
        return x >= 0 && y >= 0 && x < width && y < height;
    }
}
}