using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeGesture : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public Vector3 differencePosition;

    private bool doOnceSwipe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out hit))
            {

                if (touch.phase == TouchPhase.Began)
                {
                    startPosition = touch.position;
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    endPosition = touch.position;
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

                if (touch.phase == TouchPhase.Ended)
                {
                    doOnceSwipe = false;
                }
            }
        }
    }
}
