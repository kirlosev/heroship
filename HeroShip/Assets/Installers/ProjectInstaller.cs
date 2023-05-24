using HeroShip.Camera;
using HeroShip.Ships;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ShipSpawner _shipSpawnerPrefab;
        [SerializeField] private Cam _camRef;

        public override void InstallBindings()
        {
            Container.Bind<Cam>().FromComponentInNewPrefab(_camRef).AsSingle();
            Container.Bind<ShipSpawner>().FromComponentInNewPrefab(_shipSpawnerPrefab).AsSingle();
        }
    }
}