using HeroShip.GridMap;
using HeroShip.Modules;
using UnityEngine;

namespace HeroShip.Ships
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField] private ShipData shipDataRef;
        [SerializeField] private SlotView slotViewInst;
        [SerializeField] private Transform slotsParent;
        [SerializeField] private Transform modulesParent;
        [SerializeField] private ModuleView moduleViewInst;

        public ModuleGrid Grid => shipDataRef.grid;

        private void Start()
        {
            GenerateModulesView();
        }

        private void OnEnable()
        {
            Grid.OnModuleAdded += OnNewModuleAdded;
        }

        private void OnDisable()
        {
            Grid.OnModuleAdded -= OnNewModuleAdded;
        }

        private void OnNewModuleAdded()
        {
            GenerateModules();
        }

        private void GenerateModulesView()
        {
            GenerateSlots();
            GenerateModules();
        }

        private void GenerateSlots()
        {
            for (var x = 0; x < Grid.Size.x; ++x)
            {
                for (var y = 0; y < Grid.Size.y; ++y)
                {
                    var gridObj = Grid.GetGridModuleSlot(x, y);
                    if (gridObj == null)
                    {
                        Debug.LogError($"There's no Module Slot on grid at position(x: {x}, y: {y})");
                        continue;
                    }

                    if (!gridObj.IsActive) continue;

                    var worldPosition = Grid.GetWorldPosition(x, y);
                    worldPosition += (Vector3)Vector2.one * Grid.CellSize / 2f;
                    worldPosition += transform.position;

                    var s = Instantiate(slotViewInst, worldPosition, Quaternion.identity, slotsParent);
                    s.Init(gridObj);
                }
            }
        }

        private void GenerateModules()
        {
            var modules = Grid.ModulesOnShip;
            foreach (var m in modules)
            {
                if (m.moduleOnScene != null) continue;

                var modulePosition = Grid.GetWorldPosition(m.x, m.y);
                modulePosition += new Vector3(m.moduleData.Size.x, m.moduleData.Size.y) / 2f;
                var mi = Instantiate(moduleViewInst, modulesParent);
                mi.transform.localPosition = modulePosition;
                mi.Init(m.x, m.y, m.moduleData);
                m.moduleOnScene = mi;
            }
        }
    }
}