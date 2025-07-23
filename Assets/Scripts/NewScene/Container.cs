using UnityEngine;

public class Container<T> where T : Component
{
    private T _component;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveCompoent(T component)
    {
        _component = component;
    }
    public T GetCompoent()
    {
        return _component;
    }
    public void PrintNameComponent(T component)
    {
        if (component == null)
        {
            Debug.LogWarning("Component is null");
            return;
        }
        else
        {
            Debug.Log($"Component Name: {_component.GetType().Name}");
        }
    }
    public void SetRigibody()
    {

    }
        
}
