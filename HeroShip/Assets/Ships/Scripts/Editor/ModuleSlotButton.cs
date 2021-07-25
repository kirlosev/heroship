using System.Collections;
using System.Collections.Generic;
using HeroShip.Modules;
using UnityEngine;
using UnityEngine.UIElements;

namespace HeroShip.Editor {
public class ModuleSlotButton : Button {
    public new class UxmlFactory : UxmlFactory<ModuleSlotButton> {
    }

    private int x;
    private int y;
    private ModuleSlot slot;

    public ModuleSlotButton(int x, int y, ModuleSlot slot) {
        this.x = x;
        this.y = y;
        this.slot = slot;

        RefreshButton();
    }

    private void RefreshButton() {
        if (slot == null) {
            Debug.LogError($"No slot for {name}");
            return;
        }
        
        style.backgroundColor = slot.IsActive
            ? new StyleColor(new Color(0f, 1f, 0f, 0.4f))
            : new StyleColor(new Color(1f, 0f, 0f, 0.4f));
    }

    public ModuleSlotButton() {
    }

    public void RevertActiveStatus() {
        slot.FlipActiveStatus();
        RefreshButton();
    }
}
}