using HeroShip.GridMap;
using HeroShip.Ships;
using UnityEngine;

namespace HeroShip.Modules
{
    public class ModuleDrag : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer icon;

        private ModuleData moduleData;
        private ModuleSlot latestFittableSlot;

        public void Init(ModuleData moduleData)
        {
            this.moduleData = moduleData;
            icon.sprite = this.moduleData.Icon;
        }

        public void DragToPosition(Vector3 position)
        {
            var grid = ShipSpawner.Instance.CurrShip.Grid;
            var slotUnderPointer = grid.GetGridModuleSlot(position);
            if (slotUnderPointer != null)
            {
                var canFit = grid.CheckFitting(slotUnderPointer.GridPosition, moduleData.Size);
                icon.color = canFit ? Color.green : Color.red;

                if (canFit) latestFittableSlot = slotUnderPointer;
            }
            else
            {
                latestFittableSlot = null;
            }

            transform.position = position +
                                 new Vector3(moduleData.Size.x - grid.CellSize / 2f,
                                     moduleData.Size.y - grid.CellSize / 2f) / 2f;
        }

        public void AttemptToFit()
        {
            if (latestFittableSlot != null)
            {
                var grid = ShipSpawner.Instance.CurrShip.Grid;
                grid.SetModule(latestFittableSlot.GridPosition.x, latestFittableSlot.GridPosition.y, moduleData);
            }

            Destroy(gameObject);
        }
    }
}