using System;
using Cysharp.Threading.Tasks;
using InGame;
using UnityEngine;

namespace Fruits
{
    public class WonderFruit : Fruit
    {
        private void Awake()
        {
            index = -1;
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            hasCollided = true;
            WonderManager.Instance.WonderAnimation().Forget();
            Destroy(gameObject);
        }
    }
}