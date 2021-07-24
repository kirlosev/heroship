using UnityEngine;

namespace HeroShip.Modules {
public class ModuleView : MonoBehaviour {
    [SerializeField] private ModuleData data;

    public void SetPosition(Vector3 position) {
        transform.position = position;
    }
}
}