using Fruits;
using UnityEngine;

namespace GUI
{
    public class ViewNow : MonoBehaviour
    {
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            FruitManager.Instance.OnFruitUpdated.AddListener(UpdateNextFruitView);
        }
        
        private void OnDisable()
        {
            FruitManager.Instance.OnFruitUpdated.RemoveListener(UpdateNextFruitView);
        }

        private void UpdateNextFruitView()
        {
            var index =  GameManager.Instance.nowFruit;
            if (index == -1)
            {
                _renderer.transform.localScale = Vector3.one;
                _renderer.sprite = null;
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
