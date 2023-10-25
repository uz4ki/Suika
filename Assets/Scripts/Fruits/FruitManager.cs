using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private Fruit prefab;
        public Fruit wonderPrefab;

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
            GameManager.Instance.score += fruitBlueprints[index].score;
            var pos = (fruit1.transform.position + fruit2.transform.position) / 2f;
            RemoveFruit(fruit1);
            RemoveFruit(fruit2);

            return InstantiateFruit(index, pos);
        }
        
        public Fruit InstantiateFruit(int index, Vector3 position)
        {
            if (index == -1)
            {
                var wonderFruit = Instantiate(wonderPrefab) as Fruit;
                wonderFruit.transform.position = position;
                return wonderFruit;
            }
            
            var blueprint = fruitBlueprints[index];
            var fruit = Instantiate(prefab);
            var transform1 = fruit.transform;
            transform1.parent = fruitsParent;
            transform1.position = position;
            transform1.localScale = Vector3.one * blueprint.radius;
            fruit.index = index;
            fruit.sprite.sprite = blueprint.texture;
            _nowFruits.Add(fruit);
            
            OnFruitUpdated.Invoke();
            return fruit;
        }

        private void RemoveFruit(Fruit fruit)
        {
            _nowFruits.Remove(fruit);
            Destroy(fruit.gameObject);
        }
        
    }
}
