using System.Collections.Generic;
using UnityEngine;

public class GCAllocExamples : MonoBehaviour
{
    private int counter = 0;


    private void Start()
    {
        Stack<int> s = new Stack<int>();
        s.Push(10);
        s.Push(20);
        s.Push(30);
        int x = s.Pop();
        s.Push(40);
        int y = s.Peek();
        Debug.Log($"x : {x}");
        Debug.Log($"y : {y}");
    }
    void Update()
    {
        
    }
    private void TestGC()
    {
        counter++;

        // 1. String concat
        string a = "Frame: " + counter;

        // 2. ToString()
        string b = counter.ToString();

        // 3. New Vector3
        Vector3 pos = new Vector3(counter, 0, 0); // struct không nằm trong heap

        // 4. List Add without preallocation
        List<int> list = new List<int>();
        list.Add(counter);

        // 5. Delegate tạo mỗi frame
        System.Action action = () => Debug.Log("Hi");

        // 6. Gọi GetComponent mỗi frame
        var transformRef = GetComponent<Transform>();
    }    

    private void TestDictionKey()
    {
        Dictionary<string, int> fruits = new Dictionary<string, int>();
        fruits["apple"] = 10;
        fruits["apple"] = 20;

        Debug.Log(fruits["apple"]); // In ra gì?
    }    
}
