using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Game.Domain
{
    public class EnemyFactory : IFactory<EnemyParameters, Unit>
    {
        private readonly DiContainer _container;
        private readonly Transform _parent;
        private readonly GameObject _prefab;

        public EnemyFactory(DiContainer container, Transform parent, GameObject prefab)
        {
            _container = container;
            _parent = parent;
            _prefab = prefab;
        }
        public Unit Create(EnemyParameters param)
        {
            var subContainer = _container.CreateSubContainer();
            subContainer.BindInstance(param);
            subContainer.InstantiatePrefab(_prefab, param.Position, Quaternion.identity, _parent);
            return Unit.Default;
        }
    }
}