using UnityEngine;
using UnityEngine.Events;

public class Propogate : MonoBehaviour
{
    public UnityEvent onMouseDown;
    public UnityEvent onMouseUp;

    private void OnMouseDown() => onMouseDown.Invoke();
    private void OnMouseUp() => onMouseUp.Invoke();
}
