using HeroShip.Ships;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace HeroShip.Editor {
[CustomEditor(typeof(ShipData))]
public class ShipEditorWindow : UnityEditor.Editor {
    private static readonly float GridScale = 4f;

    private ShipData shipData;

    private VisualElement rootElement;
    private VisualElement grid;

    private void OnEnable() {
        shipData = target as ShipData;

        rootElement = new VisualElement();
        var visualTree =
            AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Ships/Scripts/Editor/ShipEditorDocument.uxml");
        visualTree.CloneTree(rootElement);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Ships/Scripts/Editor/ShipEditorStyles.uss");
        rootElement.styleSheets.Add(styleSheet);
    }

    public override VisualElement CreateInspectorGUI() {
        var shipSprite = rootElement.Query<VisualElement>("shipSprite").First();
        shipSprite.style.backgroundImage = shipData.ShipSprite ? shipData.ShipSprite.texture : null;
        shipSprite.style.height = shipData.ShipSprite ? shipData.ShipSprite.texture.height * GridScale : 0;
        shipSprite.style.width = shipData.ShipSprite ? shipData.ShipSprite.texture.width * GridScale : 0;

        var shipSpriteField = rootElement.Query<ObjectField>("shipSpriteField").First();
        shipSpriteField.objectType = typeof(Sprite);
        shipSpriteField.value = shipData.ShipSprite;
        shipSpriteField.RegisterCallback<ChangeEvent<Object>>(
            e => {
                shipData.SetShipSprite((Sprite) e.newValue);
                shipSprite.style.backgroundImage = shipData.ShipSprite ? shipData.ShipSprite.texture : null;
                EditorUtility.SetDirty(shipData);
            }
        );

        var gridSizeField = rootElement.Query<Vector2IntField>("shipGridSize").First();
        gridSizeField.value = shipData.Size;
        gridSizeField.RegisterCallback<ChangeEvent<Vector2Int>>(
            e => {
                shipData.SetSize(e.newValue);
                LoadActiveSlotsGrid();
                EditorUtility.SetDirty(shipData);
            }
        );

        grid = rootElement.Query<VisualElement>("grid").First();
        LoadActiveSlotsGrid();

        return rootElement;
    }

    private void LoadActiveSlotsGrid() {
        grid.Clear();

        var modulesGrid = shipData.GetGrid();
        for (var x = 0; x < shipData.Size.x; ++x) {
            var column = new SlotsGridColumn();
            grid.Add(column);

            for (var y = 0; y < shipData.Size.y; ++y) {
                var btn = new ModuleSlotButton(x, y, modulesGrid.GetGridModuleSlot(x, y));
                btn.RegisterCallback<ClickEvent>(
                    e => {
                        btn.RevertActiveStatus();
                        EditorUtility.SetDirty(shipData);
                    }
                );
                column.Add(btn);
            }
        }
    }
}
}