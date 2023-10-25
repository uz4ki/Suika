using Fruits;
using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class ViewNext : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            FruitManager.Instance.OnFruitUpdated.AddListener(UpdateNextFruitView);
        }
        
        private void OnDisable()
        {
            FruitManager.Instance.OnFruitUpdated.RemoveListener(UpdateNextFruitView);
        }

        private void UpdateNextFruitView()
        {
            var index =  GameManager.Instance.nextFruit;
            _image.sprite = index == -1 ? null : FruitManager.Instance.fruitBlueprints[index].texture;
        }
    }
}
