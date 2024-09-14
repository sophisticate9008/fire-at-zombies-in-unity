using System;
using R3;
using UnityEngine;
namespace MyComponents
{
    public class PenetrableComponent : IPenetrable
    {
        private readonly ReactiveProperty<int> _penetrationLevel = new(1);
        private GameObject _gameObject;
        public GameObject TheGameObject
        {
            get { return _gameObject; }
            set
            {
                _gameObject = value;
                _penetrationLevel.Subscribe(level =>
                {
                    if (level <= 0)
                    {
                        HandleDestruction();
                    }
                });
            }
        }
        public int PenetrationLevel
        {
            get => _penetrationLevel.Value;
            set
            {
                _penetrationLevel.Value = value;
            }
        }
        public void HandleDestruction()
        {
            MonoBehaviour.Destroy(_gameObject);
        }
    }

}
