using System.Linq;
using UnityEngine;

namespace Assets.Game.Presentation
{
	public class GrabTreeInput : MonoBehaviour
	{
		public LayerMask DragHandleMask;

		private Camera _camera;
		private Collider2D[] _overlap = new Collider2D[20];

		private GameObject _dragObject = null;
		private Vector2 _localDragPoint;
		private Rigidbody2D _dragRididbody;

		void Awake()
		{
			_camera = Camera.main;
		}

		void Update()
		{
			var mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);

			var overlapCount = Physics2D.OverlapPointNonAlloc(mouseWorldPos, _overlap, DragHandleMask.value);

			if (overlapCount > 0 && Input.GetMouseButton(0) && _dragObject == null)
			{
				var overlapCollider = _overlap.First();

				_dragObject = overlapCollider.gameObject;
				_dragRididbody = overlapCollider.GetComponentInParent<Rigidbody2D>();
				_localDragPoint = _dragObject.transform.InverseTransformPoint(mouseWorldPos);
			} 
			else if (!Input.GetMouseButton(0))
			{
				_dragObject = null;
			}

			if (_dragObject != null)
			{
				var dragWorldPos = _dragObject.transform.TransformPoint(_localDragPoint);
				var dragVector = dragWorldPos - mouseWorldPos;

				_dragRididbody.AddForceAtPosition(-1 * dragVector, dragWorldPos);
			}
		}
	}
}
