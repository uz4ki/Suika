using Fruits;
using UnityEngine;

namespace GUI
{
    public class ViewNow : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        [SerializeField] private Sprite cherries;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            FruitManager.Instance.OnFruitUpdated.AddListener(UpdateNextFruitView);
        }
        

        private void UpdateNextFruitView()
        {
            var index =  GameManager.Instance.nowFruit;
            if (index == -1)
            {
                _renderer.transform.localScale = Vector3.one;
                _renderer.sprite = FruitManager.Instance.wonderPrefab.sprite.sprite;
            }
            else if (index == -2)
            {  
                _renderer.transform.localScale = Vector3.one * 2f * FruitManager.Instance.fruitBlueprints[0].radius;
                _renderer.sprite = cherries;
            }
            else if (index == -3)
            {  
                _renderer.transform.localScale = Vector3.one * 0.5f;
                _renderer.sprite = FruitManager.Instance.extraFruits[0].texture;
            }
            else if (index == -4)
            {  
                _renderer.transform.localScale = Vector3.one * 0.5f;
                _renderer.sprite = FruitManager.Instance.extraFruits[1].texture;
            }
            else
            {
                var bluePrint = FruitManager.Instance.fruitBlueprints[index];
                _renderer.sprite = bluePrint.texture;
                _renderer.transform.localScale = Vector3.one * bluePrint.radius;
            }
            
        }
    }
}
