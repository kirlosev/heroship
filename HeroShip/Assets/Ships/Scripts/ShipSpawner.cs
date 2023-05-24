using HeroShip.Camera;
using HeroShip.UI;
using UnityEngine;
using Zenject;

namespace HeroShip.Ships
{
    public class ShipSpawner : MonoBehaviour
    {
        [Inject] private readonly Cam _cam;

        public bool IsFirstShip { get; private set; }
        public bool IsLastShip { get; private set; }
        public Vector3 ShipSize { get; private set; }
        public ShipView CurrShip { get; private set; }

        private ShipView _prevShip;
        private ShipView[] _ships;
        private int _currentShipIndex;

        private void Awake()
        {
            _ships = Resources.LoadAll<ShipView>("Ships/");
            _currentShipIndex = 0;
            IsFirstShip = true;
            IsLastShip = false;
        }

        private void Start()
        {
            CreateCurrentShip(Vector3.zero);
        }

        private void OnEnable()
        {
            ShipSelectScreen.OnSwitchedShipEvent += OnSwitchedShip;
        }

        private void OnDisable()
        {
            ShipSelectScreen.OnSwitchedShipEvent -= OnSwitchedShip;
        }

        private void OnSwitchedShip(int dir)
        {
            if (_prevShip != null)
            {
                DestroyPrevShip();
            }

            _currentShipIndex += dir;

            CheckEdgeShipsLimits();

            if (_currentShipIndex < 0)
            {
                _currentShipIndex = 0;
                return;
            }

            if (_currentShipIndex > _ships.Length - 1)
            {
                _currentShipIndex = _ships.Length - 1;
                return;
            }

            _prevShip = CurrShip;

            var moveAwayPosX = dir > 0 ? _cam.LeftEdge : _cam.RightEdge;
            moveAwayPosX += -Mathf.Sign(dir) * 5f;
            LeanTween
                .move(_prevShip.gameObject, new Vector3(moveAwayPosX, _prevShip.transform.position.y), 0.2f)
                .setOnComplete(DestroyPrevShip);

            var createShipPosX = dir > 0 ? _cam.RightEdge : _cam.LeftEdge;
            createShipPosX += Mathf.Sign(dir) * 5f;
            CreateCurrentShip(Vector3.right * createShipPosX);
        }

        private void CheckEdgeShipsLimits()
        {
            IsFirstShip = _currentShipIndex <= 0;
            IsLastShip = _currentShipIndex >= _ships.Length - 1;
        }

        private void CreateCurrentShip(Vector3 pos)
        {
            CurrShip = Instantiate(_ships[_currentShipIndex], pos, Quaternion.identity);
            LeanTween.move(CurrShip.gameObject, Vector3.zero, 0.2f);

            ShipSize = CurrShip.Grid.WorldSize;

            CheckEdgeShipsLimits();
        }

        private void DestroyPrevShip()
        {
            Destroy(_prevShip.gameObject);
            _prevShip = null;
        }
    }
}