using HeroShip.Camera;
using HeroShip.Modules;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HeroShip.UI {
public class DragElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private ScrollRect scrollRect;
    private ModuleData moduleData;

    private ModuleView moduleView;

    private void Awake() {
        scrollRect = GetComponentInParent<ScrollRect>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        scrollRect.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData) {
        if (moduleView != null) {
            var worldPosition = Cam.Instance.GetWorldPosition(eventData.position);
            moduleView.SetPosition(worldPosition);
            
            return;
        }
        
        scrollRect.OnDrag(eventData);

        if (!EventSystem.current.IsPointerOverGameObject() &&
            !RectTransformUtility.RectangleContainsScreenPoint(scrollRect.viewport, eventData.position) &&
            moduleView == null) {
            var worldPosition = Cam.Instance.GetWorldPosition(eventData.position);
            moduleView = Instantiate(moduleData.ModuleInst, worldPosition, Quaternion.identity);
            moduleView.SetPosition(worldPosition);
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        scrollRect.OnEndDrag(eventData);
        moduleView = null;
    }

    public void Init(ModuleData md) {
        moduleData = md;
    }
}
}