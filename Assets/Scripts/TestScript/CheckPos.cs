using UnityEngine;

public class CheckPos : MonoBehaviour
{
    public Transform a;
    public bool worldPositionStays = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.SetParent(a, worldPositionStays);
        Debug.Log($"LocalPos b  {transform.localPosition} + Position b  : {transform.position} " );
        Debug.Log($"LocalPos a {a.transform.localPosition} + Position a : {a.transform.position} " );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
