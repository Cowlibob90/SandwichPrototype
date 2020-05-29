using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    public Transform[] allChildrens;
    private Collider myCollider;

    // Start is called before the first frame update
    void Start()
    {
        allChildrens = GetComponentsInChildren<Transform>();

        Stack[] allStacksInObject = GetComponents<Stack>();
        for (int i = 0; i < allStacksInObject.Length; i++)
        {
            if (allStacksInObject[i] != this)
            {
                Destroy(allStacksInObject[i]);
            }
        }

        myCollider = GetComponent<Collider>();
        myCollider.enabled = true;
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
