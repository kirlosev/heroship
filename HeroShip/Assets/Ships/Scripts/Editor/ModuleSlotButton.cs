using System.Collections;
using System.Collections.Generic;
using HeroShips.Modules;
using UnityEngine;
using UnityEngine.UIElements;

public class ModuleSlotButton : Button {
    public new class UxmlFactory : UxmlFactory<ModuleSlotButton> {}

    private int x;
    private int y;
    private ModuleSlot slot;
    private Label label;
    
    public ModuleSlotButton(int x, int y, ModuleSlot slot) {
        this.x = x;
        this.y = y;
        this.slot = slot;
        
        // label = new Label();
        // label.AddToClassList("slotButtonLabel");
        // contentContainer.Add(label);
        
        RefreshButton();
    }

    private void RefreshButton() {
        if (slot == null) {
            Debug.LogError($"No slot for {name}");
            return;
        }
        // label.text = slot.isActive ? "v" : "x";
        style.backgroundColor = slot.isActive 
            ? new StyleColor(new Color(0f, 1f, 0f, 0.4f)) 
            : new StyleColor(new Color(1f, 0f, 0f, 0.4f));
    }

    public ModuleSlotButton() {
    }

    public void RevertActiveStatus() {
        slot.isActive = !slot.isActive;
        RefreshButton();
    }
}