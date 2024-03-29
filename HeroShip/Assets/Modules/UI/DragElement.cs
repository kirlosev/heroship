using HeroShip.Camera;
using HeroShip.Modules;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace HeroShip.UI
{
    public class DragElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text size;
        [SerializeField] private ModuleDrag moduleDragInst;

        [Inject] private readonly IInstantiator _instantiator;
        [Inject] private readonly Cam _cam;

        private ScrollRect scrollRect;
        private ModuleData moduleData;
        private ModuleDrag moduleDrag;

        private void Awake()
        {
            scrollRect = GetComponentInParent<ScrollRect>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            scrollRect.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (moduleDrag != null)
            {
                var worldPosition = _cam.GetWorldPosition(eventData.position);
                moduleDrag.DragToPosition(worldPosition);

                return;
            }

            scrollRect.OnDrag(eventData);

            if (!EventSystem.current.IsPointerOverGameObject() &&
                !RectTransformUtility.RectangleContainsScreenPoint(scrollRect.viewport, eventData.position) &&
                moduleDrag == null)
            {
                var worldPosition = _cam.GetWorldPosition(eventData.position);
                moduleDrag = _instantiator.InstantiatePrefabForComponent<ModuleDrag>(
                    moduleDragInst, worldPosition, Quaternion.identity, null);
                moduleDrag.Init(moduleData);
                moduleDrag.DragToPosition(worldPosition);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            scrollRect.OnEndDrag(eventData);
            if (moduleDrag != null) moduleDrag.AttemptToFit();
            moduleDrag = null;
        }

        public void Init(ModuleData md)
        {
            moduleData = md;
            icon.sprite = moduleData.Icon;
            title.text = moduleData.Title;
            size.text = $"{moduleData.Size.x}x{moduleData.Size.y}";
        }
    }
}