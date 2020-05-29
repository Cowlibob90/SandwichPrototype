using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAroundSide : MonoBehaviour
{
    public GameObject ingriedents;
    public float speed = 1;

    private Vector3 aroundSideRight;

    // Start is called before the first frame update
    void Start()
    {
        aroundSideRight = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.1f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OrbitAroundSide()
    {
        //transform.RotateAround(aroundSideRight, Vector3.forward, 180);
        StartCoroutine(RotationAroundSideRight());
    }

    IEnumerator RotationAroundSideRight()
    {
        while (transform.eulerAngles.z <= 180)
        {
            transform.RotateAround(aroundSideRight, Vector3.forward, speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -180);
        yield return null;
    }
}
