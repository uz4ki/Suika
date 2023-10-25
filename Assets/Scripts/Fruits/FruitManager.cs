using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using InGame;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Fruits
{
    public class FruitManager : SingletonMonoBehaviour<FruitManager>
    {
        [SerializeField] public FruitBlueprint[] fruitBlueprints;
        [SerializeField] private Image[] images;
        [SerializeField] private Transform fruitsParent;
        [SerializeField] private Fruit prefab;
        public Fruit wonderPrefab;

        private List<Fruit> _nowFruits = new List<Fruit>();
        private int[] _shuffleList;
        public List<Fruit> NowFruits => _nowFruits;

        public UnityEvent OnFruitUpdated;

        [HideInInspector] public bool isDriving => WonderManager.Instance.isWonder && WonderManager.Instance.state == WonderState.Driving;
        [HideInInspector] public bool isTopView => WonderManager.Instance.isWonder && WonderManager.Instance.state == WonderState.TopView;
        
        [HideInInspector] public bool noConvert => WonderManager.Instance.isWonder && WonderManager.Instance.state == WonderState.NoConvert;
        [HideInInspector] public bool isShuffle => WonderManager.Instance.isWonder && WonderManager.Instance.state == WonderState.EvolutionShuffle;

        
        
        public async UniTask WaitFruitCollision(Fruit fruit)
        {
            await UniTask.WaitUntil(() => fruit.hasCollided);
            _nowFruits.Add(fruit);
            OnFruitUpdated.Invoke();
        }

        private void Shuffle()
        {
            _shuffleList = new int[fruitBlueprints.Length];
            for (var i = 0; i < _shuffleList.Length; i++)
            {
                _shuffleList[i] = i;
            }
            _shuffleList = _shuffleList.OrderBy(a => Guid.NewGuid()).ToArray();
        }

        public void RefreshEvoCircle()
        {
            if (!isShuffle)
            {
                Shuffle();
                for (var i = 0; i < fruitBlueprints.Length; i++)
                {
                    images[i].sprite = fruitBlueprints[_shuffleList[i]].texture;
                }
            }
            else
            {
                for (var i = 0; i < fruitBlueprints.Length; i++)
                {
                    images[i].sprite = fruitBlueprints[i].texture;
                }
            }
        }

        public Fruit ConvertFruit(Fruit fruit1, Fruit fruit2)
        {
            var index = 0;
            if (isShuffle)
            {
                var i = Array.IndexOf(_shuffleList, fruit1.index) + 1;
                index = i < _shuffleList.Length ? _shuffleList[i] : i;
            }
            else index = fruit1.index + 1;

            if (index < fruitBlueprints.Length)
            {
                GameManager.Instance.score += fruitBlueprints[index].score;
                var pos = (fruit1.transform.position + fruit2.transform.position) / 2f;
                RemoveFruit(fruit1);
                RemoveFruit(fruit2);

                return InstantiateFruit(index, pos);
            }
            else
            {
                RemoveFruit(fruit1);
                RemoveFruit(fruit2);
                return null;
            }
        }
        
        public Fruit InstantiateFruit(int index, Vector3 position)
        {
            if (index == -1)
            {
                var wonderFruit = Instantiate(wonderPrefab) as Fruit;
                wonderFruit.transform.position = position;
                return wonderFruit;
            }

            if (index == -2)
            {
                InstantiateFruit(0, position - Vector3.right * 0.15f + Vector3.up * 0.3f);
                InstantiateFruit(0, position + Vector3.right * 0.15f + Vector3.up * 0.3f);
                return InstantiateFruit(1, position);
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
