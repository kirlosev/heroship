using HeroShips.Ships;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


[CustomEditor(typeof(ShipData))]
public class ShipEditorWindow : Editor {
    public static float GRID_SCALE = 4f;
    // public void CreateGUI() {
    //     // Each editor window contains a root VisualElement object
    //     VisualElement root = rootVisualElement;
    //
    //     // Import UXML
    //     var visualTree =
    //         AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Ships/Scripts/Editor/ShipEditorDocument.uxml");
    //     visualTree.CloneTree(root);
    //
    //     // A stylesheet can be added to a VisualElement.
    //     // The style will be applied to the VisualElement and all of its children.
    //     var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Ships/Scripts/Editor/ShipEditorStyles.uss");
    //     root.styleSheets.Add(styleSheet);
    // }

    private ShipData shipData;

    private VisualElement rootElement;
    private VisualTreeAsset visualTree;
    private VisualElement grid;

    void OnEnable() {
        shipData = (ShipData) target;

        rootElement = new VisualElement();
        visualTree =
            AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Ships/Scripts/Editor/ShipEditorDocument.uxml");
        visualTree.CloneTree(rootElement);
        
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Ships/Scripts/Editor/ShipEditorStyles.uss");
        rootElement.styleSheets.Add(styleSheet);
    }

    public override VisualElement CreateInspectorGUI() {
        var shipSprite = rootElement.Query<VisualElement>("shipSprite").First();
        shipSprite.style.backgroundImage = shipData.ShipSprite ? shipData.ShipSprite.texture : null;
        shipSprite.style.height = shipData.ShipSprite.texture.height * GRID_SCALE;
        shipSprite.style.width = shipData.ShipSprite.texture.width * GRID_SCALE;
        
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

    void LoadActiveSlotsGrid() {
        this.grid.Clear();

        var modulesGrid = shipData.GetGrid();
        for (var x = 0; x < shipData.Size.x; ++x) {
            var column = new SlotsGridColumn();
            grid.Add(column);
            
            for (var y = 0; y < shipData.Size.y; ++y) {
                var btn = new ModuleSlotButton(x, y, modulesGrid.GetGridObject(x, y));
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