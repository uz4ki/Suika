using UnityEngine;

namespace Fruits
{
    public class BillBoard : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }
        private void Update()
        {
            var transform1 = transform;
            transform1.forward = _camera.transform.position - transform1.position;
        }
    }
}
