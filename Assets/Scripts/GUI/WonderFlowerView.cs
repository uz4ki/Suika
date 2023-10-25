using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class WonderFlowerView : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public async UniTask Animation()
        {
            _image.color = Color.white;
            transform.localScale = Vector3.one * 2f;
            transform.DOScale(Vector3.one * 10f, 0.5f);
            await _image.DOFade(0f, 0.5f).SetEase(Ease.OutQuad).ToUniTask();
        }
    }
}
