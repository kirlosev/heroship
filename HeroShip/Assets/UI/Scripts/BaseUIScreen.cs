using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroShip.UI
{
    public abstract class BaseUIScreen : MonoBehaviour
    {
        [SerializeField] protected Transform contentTr;

        protected virtual void Start()
        {
            TurnOnOffByDefault();
        }

        protected abstract void TurnOnOffByDefault();

        protected virtual void TurnOn()
        {
            contentTr.gameObject.SetActive(true);
        }

        protected virtual void TurnOff()
        {
            contentTr.gameObject.SetActive(false);
        }
    }
}