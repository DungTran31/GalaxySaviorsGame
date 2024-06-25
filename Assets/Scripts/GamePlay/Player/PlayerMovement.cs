using UnityEngine;

namespace DungTran31.GamePlay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 25f;
        [SerializeField] private float moveSpeed = 10f;
        private Vector2 _direction;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector2 cursorPos = _camera.ScreenToWorldPoint(Input.mousePosition);

            // Only update direction and rotation if the mouse is moving
            if (cursorPos != (Vector2)transform.position)
            {
                _direction = cursorPos - (Vector2)transform.position;
                float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            }

            // Always move towards the cursor position
            transform.position = Vector2.MoveTowards(transform.position, cursorPos, moveSpeed * Time.deltaTime);
        }
    }
}
