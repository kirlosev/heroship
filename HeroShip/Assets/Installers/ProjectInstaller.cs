using HeroShip.Ships;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ShipSpawner _shipSpawnerPrefab;

        public override void InstallBindings()
        {
            Container.Bind<ShipSpawner>().FromComponentInNewPrefab(_shipSpawnerPrefab).AsSingle();
        }
    }
}