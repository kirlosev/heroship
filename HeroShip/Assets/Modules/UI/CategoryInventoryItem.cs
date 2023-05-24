using HeroShip.Modules;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HeroShip.UI
{
    public class CategoryInventoryItem : MonoBehaviour
    {
        public static event System.Action<ModuleCategory> OnSelectedEvent;

        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text title;

        private ModuleCategory moduleCategory;

        public void BTN_SELECT()
        {
            OnSelectedEvent?.Invoke(moduleCategory);
        }

        public void Init(ModuleCategory mc)
        {
            moduleCategory = mc;
            icon.sprite = moduleCategory.Icon;
            title.text = moduleCategory.Title;
        }
    }
}