using System.Collections;
using System.Collections.Generic;
using HeroShip.Modules;
using UnityEngine;

namespace HeroShip.UI {
public class ModulesInventory : MonoBehaviour {
    [SerializeField] private Transform inventoryHolder;
    [SerializeField] private DragElement inventoryElementInstance;
    [SerializeField] private ModuleData[] modules;

    private void Start() {
        foreach (var m in modules) {
            var ie = Instantiate(inventoryElementInstance, inventoryHolder);
            ie.Init(m);
        }
    }
}
}