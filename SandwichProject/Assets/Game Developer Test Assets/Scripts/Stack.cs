using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    public Transform[] allChildrens;

    // Start is called before the first frame update
    void Start()
    {
        allChildrens = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateChildrens()
    {
        allChildrens = GetComponentsInChildren<Transform>();
        for (int i = 1; i < allChildrens.Length; i++)
        {
            Destroy(allChildrens[i].GetComponent<Stack>());
        }
    }
}
