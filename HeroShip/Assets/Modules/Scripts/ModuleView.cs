using UnityEngine;

namespace HeroShip.Modules {
public class ModuleView : MonoBehaviour {
    [SerializeField] private SpriteRenderer rend;

    private ModuleData moduleData;
    private int x;
    private int y;

    public void Init(int x, int y, ModuleData moduleData) {
        this.x = x;
        this.y = y;
        this.moduleData = moduleData;
        rend.sprite = this.moduleData.Icon;
    }

    public void Remove() {
        Destroy(gameObject);
    }
}
}