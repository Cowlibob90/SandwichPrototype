using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    public new Camera camera;

    private bool doOnce;
    private bool doOnceSwipe;

    public Vector3 startPosition;
    public Vector3 endPosition;
    private Vector3 differencePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButton(0))
            {
                if (!doOnce)
                {
                    startPosition = Input.mousePosition;
                    doOnce = true;
                }
                endPosition = Input.mousePosition;

                if (endPosition != startPosition)
                {
                    if (!doOnceSwipe)
                    {
                        differencePosition = startPosition - endPosition;
                        differencePosition.Normalize();

                        if (differencePosition.y > 0 && differencePosition.x > -0.5f && differencePosition.x < 0.5f)
                        {
                            IngredientProxy boxcast = hit.transform.GetComponent<IngredientProxy>();
                            if (boxcast != null)
                            {
                                boxcast.OrbitAroundSideDown();
                            }
                            Debug.Log("Down swipe");
                        }

                        if (differencePosition.y < 0 && differencePosition.x > -0.5f && differencePosition.x < 0.5f)
                        {
                            IngredientProxy boxcast = hit.transform.GetComponent<IngredientProxy>();
                            if (boxcast != null)
                            {
                                boxcast.OrbitAroundSideUp();
                            }
                            Debug.Log("Up swipe");
                        }

                        if (differencePosition.x < 0 && differencePosition.y > -0.5f && differencePosition.y < 0.5f)
                        {
                            IngredientProxy boxcast = hit.transform.GetComponent<IngredientProxy>();
                            if (boxcast != null)
                            {
                                boxcast.OrbitAroundSideRight();
                            }
                            Debug.Log("Right swipe");
                        }

                        if (differencePosition.x > 0 && differencePosition.y > -0.5f && differencePosition.y < 0.5f)
                        {
                            IngredientProxy boxcast = hit.transform.GetComponent<IngredientProxy>();
                            if (boxcast != null)
                            {
                                boxcast.OrbitAroundSideLeft();
                            }
                            Debug.Log("Left swipe");
                        }
                        doOnceSwipe = true;
                    }
                }
            }
            else
            {
                doOnce = false;
                doOnceSwipe = false;
            }
            if (Input.GetMouseButtonUp(0))
            {
                doOnce = false;
                doOnceSwipe = false;
            }

        }
    }
}
