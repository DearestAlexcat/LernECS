using UnityEngine;

public abstract class WindowBase : MonoBehaviour
{
    public void SetActiveWindow(bool value)
    {
        gameObject.SetActive(value);
    }
}
