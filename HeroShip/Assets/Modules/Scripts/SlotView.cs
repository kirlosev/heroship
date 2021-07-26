using UnityEngine;

// NOTE: it would be better to generate a mesh instead of spawning lots of GOs for slots.

namespace HeroShip.Modules {
public class SlotView : MonoBehaviour {
    [SerializeField] private SpriteRenderer rend;
    
    private ModuleSlot slot;
    
    public void Init(ModuleSlot slot) {
        this.slot = slot;
    }
    
    private void Update() {
        if (slot.IsOccupied) {
            rend.color = Color.green;
        }
        else {
            rend.color = Color.white;
        }
    }
}
}