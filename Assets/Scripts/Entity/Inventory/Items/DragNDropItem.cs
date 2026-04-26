using Unity.VisualScripting;
using UnityEngine;

public class DragNDropItem : MonoBehaviour
{
    private Vector3 mouseOffset;
    private BoxCollider2D boxCollider;
    private void Awake()
    {
        boxCollider = transform.GetOrAddComponent<BoxCollider2D>();
    }
    private Vector3 GetWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnMouseDown()
    {
        Debug.Log("valami");
        mouseOffset = gameObject.transform.position - GetWorldPosition();
    }
    private void OnMouseDrag()
    {
        Debug.Log("valami mas");
        transform.position = GetWorldPosition() + mouseOffset;
    }
    private void OnMouseUp()
    {
        
    }
}