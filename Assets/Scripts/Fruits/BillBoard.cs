using UnityEngine;

namespace Fruits
{
    public class BillBoard : MonoBehaviour
    {
        private Camera _camera;
        public bool isEnabled;

        private void Start()
        {
            _camera = Camera.main;
        }
        private void Update()
        {
            if (!isEnabled) return;
            transform.forward = _camera.transform.position - transform.position;
        }
    }
}
