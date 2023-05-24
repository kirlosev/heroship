using System.Collections;
using System.Collections.Generic;
using HeroShip.Modules;
using TMPro;
using UnityEngine;

namespace HeroShip.UI
{
    public class ModulesInventory : MonoBehaviour
    {
        [SerializeField] private Transform inventoryHolder;
        [SerializeField] private CategoryInventoryItem categoryItemInstance;
        [SerializeField] private SubcategoryInventoryItem subcategoryItemInstance;
        [SerializeField] private DragElement inventoryElementInstance;

        [SerializeField] private Transform backButton;
        [SerializeField] private TMP_Text backButtonText;

        private ModuleCategory[] categories;
        private ModuleData[] allModules;

        private enum InventoryItemsType
        {
            category,
            subcategory,
            modules
        }

        private InventoryItemsType currentInventoryItemType;
        private List<ModuleData> latestModules = new List<ModuleData>();

        private void Awake()
        {
            categories = Resources.LoadAll<ModuleCategory>("Categories Datas/");
            allModules = Resources.LoadAll<ModuleData>("Module Datas/");
        }

        private void Start()
        {
            GenerateCategories();
        }

        private void CleanUpHolder()
        {
            foreach (Transform t in inventoryHolder)
            {
                Destroy(t.gameObject);
            }
        }

        private void GenerateCategories()
        {
            CleanUpHolder();
            foreach (var m in categories)
            {
                var ie = Instantiate(categoryItemInstance, inventoryHolder);
                ie.Init(m);
            }

            backButton.gameObject.SetActive(false);

            currentInventoryItemType = InventoryItemsType.category;
        }

        private void GenerateSubcategories(ModuleSubcategory[] subcategories)
        {
            CleanUpHolder();

            foreach (var m in subcategories)
            {
                var ie = Instantiate(subcategoryItemInstance, inventoryHolder);
                ie.Init(m);
            }

            backButtonText.text = "To Categories";
            backButton.gameObject.SetActive(true);

            currentInventoryItemType = InventoryItemsType.subcategory;
        }

        private void GenerateModules(ModuleData[] modules)
        {
            CleanUpHolder();
            foreach (var m in modules)
            {
                var ie = Instantiate(inventoryElementInstance, inventoryHolder);
                ie.Init(m);
            }

            if (modules.Length > 0)
                backButtonText.text = $"{modules[0].Subcategory.Title}";
            else
                backButtonText.text = "To Categories";

            backButton.gameObject.SetActive(true);

            currentInventoryItemType = InventoryItemsType.modules;
        }

        private void OnEnable()
        {
            CategoryInventoryItem.OnSelectedEvent += OnSelectedCategory;
            SubcategoryInventoryItem.OnSelectedEvent += OnSelectedSubcategory;
        }

        private void OnDisable()
        {
            CategoryInventoryItem.OnSelectedEvent -= OnSelectedCategory;
            SubcategoryInventoryItem.OnSelectedEvent -= OnSelectedSubcategory;
        }

        private void OnSelectedCategory(ModuleCategory category)
        {
            GenerateSubcategories(category.Subcategories);
        }

        private void OnSelectedSubcategory(ModuleSubcategory subcategory)
        {
            latestModules.Clear();
            foreach (var m in allModules)
            {
                if (m.Subcategory == subcategory)
                {
                    latestModules.Add(m);
                }
            }

            GenerateModules(latestModules.ToArray());
        }

        public void BTN_BACK()
        {
            if (currentInventoryItemType == InventoryItemsType.category)
            {
            }
            else if (currentInventoryItemType == InventoryItemsType.subcategory)
            {
                GenerateCategories();
            }
            else if (currentInventoryItemType == InventoryItemsType.modules)
            {
                if (latestModules.Count == 0) GenerateCategories();
                else GenerateSubcategories(latestModules[0].Category.Subcategories);
            }
        }
    }
}