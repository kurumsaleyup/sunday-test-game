using UnityEngine;
using UnityEngine.EventSystems;

public class TubeController : MonoBehaviour
{
    [SerializeField] private float sensitivity = 0.2f;


    #region Life Cycle

    private void Start()
    {
        InputManager.Instance.onDrag += OnDrag;
    }

    #endregion


    #region User Interaction

    private void OnDrag(PointerEventData eventData)
    {
        var euler = transform.rotation.eulerAngles;
        euler.z += eventData.delta.x * sensitivity;
        transform.rotation = Quaternion.Euler(euler);
    }

    #endregion
}
