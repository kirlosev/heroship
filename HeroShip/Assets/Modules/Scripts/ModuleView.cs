using UnityEngine;

namespace HeroShip.Modules {
public class ModuleView : MonoBehaviour {
    [SerializeField] private ModuleData data;

    private int x;
    private int y;

    public void Init(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public void Remove() {
        Destroy(gameObject);
    }
}
}