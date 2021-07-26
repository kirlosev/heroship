using HeroShip.Ships;
using UnityEngine;

namespace HeroShip.UI {
public class ShipEditorScreen : BaseUIScreen {
    public static event System.Action OnConfirmEvent;
    
    [SerializeField] private ErrorWindow errorWindow;
    
    protected override void TurnOnOffByDefault() {
        TurnOff();
    }

    private void OnEnable() {
        ShipSelectScreen.OnShipSelectedEvent += OnShipSelected;
    }

    private void OnDisable() {
        ShipSelectScreen.OnShipSelectedEvent -= OnShipSelected;
    }

    private void OnShipSelected() {
        TurnOn();
    }

    public void BTN_CONFIRM() {
        if (ShipSpawner.Instance.CurrShip.Grid.HasEmptySlots) {
            errorWindow.Show();
        }
        else {
            OnConfirmEvent?.Invoke();
            TurnOff();
        }
    }
}
}