using UnityEngine;

public class PrintImfor : MonoBehaviour
{
    void Start()
    {
        int a = 1, b = 2;
        Debug.Log($"Trước khi swap: a = {a}, b = {b}");
        Swap<int>(ref a, ref b);
        Debug.Log($"Sau khi swap: a = {a}, b = {b}");

        string x = "Hello", y = "World";
        Debug.Log($"Trước khi swap: x = {x}, y = {y}");
        Swap(ref x, ref y);
        Debug.Log($"Sau khi swap: x = {x}, y = {y}");

        Set(100, 200, out int arg1, out int arg2);
        Debug.Log($"arg1 = {arg1}, arg2 = {arg2}");
        Set("Hello", "World", out string arg3, out string arg4);
        Debug.Log($"arg3 = {arg3}, arg4 = {arg4}");
    }

    void Swap<T>(ref T arg1, ref T arg2)
    {
        (arg2, arg1) = (arg1, arg2);
    }
    void Set<T>(T value, T value1, out T arg, out T ag)
    {
        arg = value;
        ag = value1;

    }
}
