using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScale : MonoBehaviour {
    public void DoTween() {
        LeanTween.scale(gameObject, Vector3.one * 1.1f, 0.1f).setEaseOutBack().setOnComplete(() => {
            LeanTween.scale(gameObject, Vector3.one, 0.2f).setEaseOutBack();
        });
    }
}