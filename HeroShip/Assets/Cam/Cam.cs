using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroShip.Camera {
public class Cam : MonoBehaviour {
    public static Cam Instance;

    [SerializeField] private UnityEngine.Camera cam;

    public float Height => cam.orthographicSize;
    public float Width => Screen.width * Height / Screen.height;

    private void Awake() {
        Instance = this;
    }

    public float BottomEdge => transform.position.y - Height;
    public float TopEdge => transform.position.y + Height;
    public float LeftEdge => transform.position.x - Width;
    public float RightEdge => transform.position.x + Width;

    public Vector3 GetWorldPosition(Vector2 screenPos) {
        var p = new Vector3(screenPos.x, screenPos.y, -cam.transform.localPosition.z);
        return cam.ScreenToWorldPoint(p);
    }

    public Vector3 GetScreenPosition(Vector3 worldPos) {
        return cam.WorldToViewportPoint(worldPos);
    }

    public bool CheckVisibility(Vector3 pos, Bounds boundingBox) {
        return pos.x + boundingBox.extents.x > LeftEdge
               && pos.x - boundingBox.extents.x < RightEdge
               && pos.y + boundingBox.extents.y > BottomEdge
               && pos.y - boundingBox.extents.y < TopEdge;
    }

    public bool IsPointWithinCamera(Vector3 p) {
        return p.x > LeftEdge && p.x < RightEdge && p.y > BottomEdge && p.y < TopEdge;
    }

    public Vector3 BottomLeftScreenCornerInWorld => GetWorldPosition(Vector2.zero);
}
}