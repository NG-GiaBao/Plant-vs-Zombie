using System.Text;
using UnityEngine;

public class GCTest : MonoBehaviour
{
    private int counter = 0;
    private StringBuilder sb = new StringBuilder(64);

    void Update()
    {
        counter++;
        sb.Clear();
        sb.Append("Counter: ").Append(counter);
        // Không Debug.Log, không ToString
    }
}
