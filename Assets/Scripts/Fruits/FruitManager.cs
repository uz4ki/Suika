using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

namespace Fruits
{
    public class FruitManager : SingletonMonoBehaviour<FruitManager>
    {
        [SerializeField] public FruitBlueprint[] fruitBlueprints;
        [SerializeField] private Transform fruitsParent;

        private List<Fruit> _nowFruits = new List<Fruit>();
        public List<Fruit> NowFruits => _nowFruits;

        public UnityEvent OnFruitUpdated;

        public async UniTask WaitFruitCollision(Fruit fruit)
        {
            await UniTask.WaitUntil(() => fruit.hasCollided);
            _nowFruits.Add(fruit);
            OnFruitUpdated.Invoke();
        } 

        public Fruit ConvertFruit(Fruit fruit1, Fruit fruit2)
        {
            var index = fruit1.index + 1;
            var pos = (fruit1.transform.position + fruit2.transform.position) / 2f;
            RemoveFruit(fruit1);
            RemoveFruit(fruit2);

            OnFruitUpdated.Invoke();
            
            return InstantiateFruit(index, pos);
        }
        
        public Fruit InstantiateFruit(int index, Vector3 position)
        {
            var blueprint = fruitBlueprints[index];
            var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            var fruit = obj.AddComponent<Fruit>();
            obj.transform.parent = fruitsParent;
            obj.transform.position = position;
            obj.transform.localScale = Vector3.one * blueprint.radius;
            fruit.index = index;
            _nowFruits.Add(fruit);
            return fruit;
        }

        private void RemoveFruit(Fruit fruit)
        {
            _nowFruits.Remove(fruit);
            Destroy(fruit.gameObject);
        }
    }
}
