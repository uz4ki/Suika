using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class Dropper : MonoBehaviour
    {
        [SerializeField] private Transform point;
        public Vector3 Position => point.position;
        [SerializeField] private float moveLimit;
        [SerializeField] private float moveSpeed;
        
        private float _startY;

        private void Start()
        {
            _startY = transform.position.y;
        }

        public void MoveDropper(float diff)
        {
            if (Mathf.Abs(point.localPosition.x + diff) > moveLimit) return;
            
            point.localPosition += diff * moveSpeed * Vector3.right;
        }

        public async UniTask ToBottom()
        {
            await point.DOMove(new Vector3(point.position.x, -0.5f, point.position.z), 0.8f).SetEase(Ease.InQuad).ToUniTask();
        }
        public async UniTask ToTop()
        {
            await point.DOMove(new Vector3(point.position.x, _startY + 2.5f, point.position.z), 0.8f).SetEase(Ease.InQuad).ToUniTask();
        }
    }
}