using HeroShip.Camera;
using HeroShip.UI;
using UnityEngine;

namespace HeroShip.Ships
{
    public class ShipSpawner : MonoBehaviour
    {
        public static ShipSpawner Instance;

        [SerializeField] private BoolVariable reachedFirstShipRef;
        [SerializeField] private BoolVariable reachedLastShipRef;
        [SerializeField] private Vector3Variable currentShipSizeRef;

        private ShipView prevShip;
        private ShipView currShip;
        public ShipView CurrShip => currShip;

        private ShipView[] ships;
        private int currentShipIndex;

        private void Awake()
        {
            Instance = this;

            ships = Resources.LoadAll<ShipView>("Ships/");
            currentShipIndex = 0;
            reachedFirstShipRef.value = true;
            reachedLastShipRef.value = false;
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
            if (prevShip != null)
            {
                DestroyPrevShip();
            }

            currentShipIndex += dir;

            CheckEdgeShipsLimits();

            if (currentShipIndex < 0)
            {
                currentShipIndex = 0;
                return;
            }

            if (currentShipIndex > ships.Length - 1)
            {
                currentShipIndex = ships.Length - 1;
                return;
            }

            prevShip = currShip;

            var moveAwayPosX = dir > 0 ? Cam.Instance.LeftEdge : Cam.Instance.RightEdge;
            moveAwayPosX += -Mathf.Sign(dir) * 5f;
            LeanTween.move(prevShip.gameObject, new Vector3(moveAwayPosX, prevShip.transform.position.y), 0.2f)
                .setOnComplete(DestroyPrevShip);

            var createShipPosX = dir > 0 ? Cam.Instance.RightEdge : Cam.Instance.LeftEdge;
            createShipPosX += Mathf.Sign(dir) * 5f;
            CreateCurrentShip(Vector3.right * createShipPosX);
        }

        private void CheckEdgeShipsLimits()
        {
            reachedFirstShipRef.value = currentShipIndex <= 0;
            reachedLastShipRef.value = currentShipIndex >= ships.Length - 1;
        }

        private void CreateCurrentShip(Vector3 pos)
        {
            currShip = Instantiate(ships[currentShipIndex], pos, Quaternion.identity);
            LeanTween.move(currShip.gameObject, Vector3.zero, 0.2f);

            currentShipSizeRef.value = currShip.Grid.WorldSize;

            CheckEdgeShipsLimits();
        }

        private void DestroyPrevShip()
        {
            Destroy(prevShip.gameObject);
            prevShip = null;
        }
    }
}