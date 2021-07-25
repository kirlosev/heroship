using UnityEngine;

namespace HeroShip.Modules {
public class SlotView : MonoBehaviour {
    [SerializeField] private SpriteRenderer rend;
    
    private ModuleSlot slot;
    
    public void Init(ModuleSlot slot) {
        this.slot = slot;
    }
    
    private void Update() {
        // TODO: please don't forget to optimise this
        if (slot.IsOccupied) {
            rend.color = Color.green;
        }
        else {
            rend.color = Color.white;
        }
    }
}
}