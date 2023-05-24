using System;
using HeroShip.Ships;
using UnityEngine;
using Zenject;

namespace HeroShip.UI
{
    public class ShipSelectScreen : BaseUIScreen
    {
        public static event Action<int> OnSwitchedShipEvent;
        public static event Action OnShipSelectedEvent;

        [SerializeField] private Transform switchPrevButton;
        [SerializeField] private Transform switchNextButton;

        [Inject] private readonly ShipSpawner _shipSpawner;

        protected override void TurnOnOffByDefault()
        {
            TurnOn();
        }

        public void BTN_SWITCH_SHIP(int dir)
        {
            OnSwitchedShipEvent?.Invoke(dir);
        }

        public void BTN_SELECT()
        {
            TurnOff();
            OnShipSelectedEvent?.Invoke();
        }

        private void Update()
        {
            switchPrevButton.gameObject.SetActive(!_shipSpawner.IsFirstShip);
            switchNextButton.gameObject.SetActive(!_shipSpawner.IsLastShip);
        }

        private void OnEnable()
        {
            ShipEditorScreen.OnConfirmEvent += OnShipEditConfirm;
        }

        private void OnDisable()
        {
            ShipEditorScreen.OnConfirmEvent -= OnShipEditConfirm;
        }

        private void OnShipEditConfirm()
        {
            TurnOn();
        }
    }
}