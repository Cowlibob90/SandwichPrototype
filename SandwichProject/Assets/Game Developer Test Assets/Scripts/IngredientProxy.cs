using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientProxy : MonoBehaviour
{
    [Header("Rotation Speed")]
    public float speed = 100;

    private float maxDistance;
    private Collider myCollider;
    private RaycastHit hit;
    private GameObject hitObject;

    void Start()
    {
        maxDistance = 1.0f;
        myCollider = GetComponent<Collider>();
    }

    void Update()
    {

    }

    bool CanSwipeLeft()
    {
        if(Physics.BoxCast(myCollider.bounds.center, transform.localScale / 4, transform.right, out hit, transform.rotation, maxDistance))
        {
            hitObject = hit.collider.transform.gameObject;
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CanSwipeRight()
    {
        if(Physics.BoxCast(myCollider.bounds.center, transform.localScale / 4, -transform.right, out hit, transform.rotation, maxDistance))
        {
            hitObject = hit.collider.transform.gameObject;
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CanSwipeUp()
    {
        if (Physics.BoxCast(myCollider.bounds.center, transform.localScale / 4, -transform.forward, out hit, transform.rotation, maxDistance))
        {
            hitObject = hit.collider.transform.gameObject;
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CanSwipeDown()
    {
        if (Physics.BoxCast(myCollider.bounds.center, transform.localScale / 4, transform.forward, out hit, transform.rotation, maxDistance))
        {
            hitObject = hit.collider.transform.gameObject;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OrbitAroundSideRight()
    {
        if (CanSwipeRight() && !GameManager.singleton.ingredientIsAnimating)
        {
            Stack stack = hitObject.GetComponent<Stack>();
            Stack myStack = GetComponent<Stack>();
            Vector3 sideRight = new Vector3(transform.position.x - transform.lossyScale.x / 2, 0.05f * stack.allChildrens.Length + 0.05f * myStack.allChildrens.Length, transform.position.z);
            StartCoroutine(RotationAroundSide(sideRight, hitObject, Vector3.forward));
        }
    }

    public void OrbitAroundSideLeft()
    {
        if (CanSwipeLeft() && !GameManager.singleton.ingredientIsAnimating)
        {
            Stack stack = hitObject.GetComponent<Stack>();
            Stack myStack = GetComponent<Stack>();
            Vector3 sideLeft = new Vector3(transform.position.x + transform.lossyScale.x / 2, 0.05f * stack.allChildrens.Length + 0.05f * myStack.allChildrens.Length, transform.position.z);
            StartCoroutine(RotationAroundSide(sideLeft, hitObject, Vector3.back));
        }
    }

    public void OrbitAroundSideUp()
    {
        if (CanSwipeUp() && !GameManager.singleton.ingredientIsAnimating)
        {
            Stack stack = hitObject.GetComponent<Stack>();
            Stack myStack = GetComponent<Stack>();
            Vector3 sideUp = new Vector3(transform.position.x, 0.05f * stack.allChildrens.Length + 0.05f * myStack.allChildrens.Length, transform.position.z - transform.lossyScale.z / 2);
            StartCoroutine(RotationAroundSide(sideUp, hitObject, Vector3.left));
        }
    }

    public void OrbitAroundSideDown()
    {
        if (CanSwipeDown() && !GameManager.singleton.ingredientIsAnimating)
        {
            Stack stack = hitObject.GetComponent<Stack>();
            Stack myStack = GetComponent<Stack>();
            Vector3 sideDown = new Vector3(transform.position.x, 0.05f * stack.allChildrens.Length + 0.05f * myStack.allChildrens.Length, transform.position.z + transform.lossyScale.z / 2);
            StartCoroutine(RotationAroundSide(sideDown, hitObject, Vector3.right));

        }
    }



    IEnumerator RotationAroundSide(Vector3 _point, GameObject _hitObject, Vector3 _axis)
    {
        float degree = 0;
        GameManager.singleton.ingredientIsAnimating = true;
        while (degree <= 180)
        {
            GameManager.singleton.StartReplaySampling(transform);
            transform.RotateAround(_point, _axis, speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
            degree += speed * Time.deltaTime;
        }
        transform.localEulerAngles = _axis * -180;
        GameManager.singleton.StopReplaySamples();
        if (_hitObject != null)
        {
            transform.parent = _hitObject.transform;
        }

        myCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);

        _hitObject.GetComponent<Stack>().UpdateChildrens();
        GameManager.singleton.ingredientIsAnimating = false;
        yield return new WaitForSeconds(1.0f);

        if (GameManager.singleton != null)
        {
            GameManager.singleton.FindStack();
        }

        yield return null;
    }
}
