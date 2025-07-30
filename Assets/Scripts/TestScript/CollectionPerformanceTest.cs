using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CollectionPerformanceTest : MonoBehaviour
{
    private const int elementCount = 100000;
    private int[] array;
    private List<int> list;
    private Dictionary<int, int> dictionary;
    private const int searchTarget = elementCount - 10;
    private Stopwatch timer;

    void Start()
    {
        //TestArray();
        //TestList();
        //TestDictionary();
        TestWithoutCapacity();
        TestSearchSpeed();
        
    }

    void TestArray()
    {
        array = new int[elementCount];
        for (int i = 0; i < elementCount; i++) array[i] = i;

        long sum = 0;
        for (int i = 0; i < elementCount; i++) sum += array[i];
     
        int find = Array.IndexOf(array, elementCount - 1);
       
    }

    void TestList()
    {
        list = new List<int>(elementCount);
        for (int i = 0; i < elementCount; i++) list.Add(i);
      
        long sum = 0;
        for (int i = 0; i < list.Count; i++) sum += list[i];
      
        int find = list.IndexOf(elementCount - 1);
       
    }

    void TestDictionary()
    {
        dictionary = new Dictionary<int, int>(elementCount);
        for (int i = 0; i < elementCount; i++) dictionary.Add(i, i);
     
        long sum = 0;
        foreach (var kv in dictionary) sum += kv.Value;
     
        int value = dictionary[elementCount - 1];
       
    }
    private void TestSearchSpeed()
    {
        bool foundlist = list.Contains(searchTarget);
        bool found = dictionary.ContainsKey(searchTarget);
    }
    private void TestWithoutCapacity()
    {
        list = new List<int>();
        dictionary = new Dictionary<int, int>();
        for(int i = 0;i < elementCount;i++) list.Add(i);
        for(int i = 0;i < elementCount;i++) dictionary.Add(i, i);
    }
}
