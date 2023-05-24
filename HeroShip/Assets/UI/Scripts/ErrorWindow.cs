using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroShip.UI
{
    public class ErrorWindow : MonoBehaviour
    {
        [SerializeField] private Transform contentTr;

        public void Show()
        {
            contentTr.gameObject.SetActive(true);
        }

        public void BTN_OK()
        {
            Hide();
        }

        public void Hide()
        {
            contentTr.gameObject.SetActive(false);
        }
    }
}