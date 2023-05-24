using System.Collections;
using System.Collections.Generic;
using HeroShip.Modules;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HeroShip.UI
{
    public class SubcategoryInventoryItem : MonoBehaviour
    {
        public static event System.Action<ModuleSubcategory> OnSelectedEvent;

        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text title;

        private ModuleSubcategory moduleSubcategory;

        public void BTN_SELECT()
        {
            OnSelectedEvent?.Invoke(moduleSubcategory);
        }

        public void Init(ModuleSubcategory ms)
        {
            moduleSubcategory = ms;
            icon.sprite = moduleSubcategory.Icon;
            title.text = moduleSubcategory.Title;
        }
    }
}