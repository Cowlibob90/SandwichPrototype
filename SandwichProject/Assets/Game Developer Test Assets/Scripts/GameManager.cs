using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    [HideInInspector] public bool ingredientIsAnimating;
    public Transform parentIngredients;
    [Header("UI")]
    public Text finishedText;

    private Stack[] allStackInScene;
    private Transform moreHeight;

    // Replay Vars
    private List<ReplaySample> replaySamples = new List<ReplaySample>();
    private bool sampleTheTransform;
    int replayIndex;
    bool doFirstReplayFrame;
    Transform recordingTarget;

    private bool doOneRetry;

    private void Awake()
    {
        singleton = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        moreHeight = transform;
        moreHeight.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ReplaySampler();
        RewindReplayPlayer();
    }

    public void FindStack()
    {
        allStackInScene = FindObjectsOfType<Stack>();

        if (allStackInScene.Length == 1)
        {
            Debug.Log("Finished");
            CheckWin(allStackInScene[0]);
        }
    }

    public void CheckWin(Stack _stack)
    {
        for (int i = 0; i < _stack.allChildrens.Length; i++)
        {
            if(_stack.allChildrens[i].position.y >= moreHeight.position.y)
            {
                moreHeight = _stack.allChildrens[i];
            }
        }
        if (moreHeight.name.Contains("Bread") && _stack.allChildrens[0].name.Contains("Bread"))
        {
            finishedText.text = "YOU WIN";
            Debug.Log("You win");
        }
        else
        {
            finishedText.text = "YOU LOSE";
            Debug.Log("You Lose");
        }
    }

    private void OnDisable()
    {
        singleton = null;
    }


    #region Replay System

    public void StartReplaySampling(Transform targetIngredient)
    {
        recordingTarget = targetIngredient;
        sampleTheTransform = true;
        //StartCoroutine(ReplaySampler(targetIngredient));
    }

    public void StopReplaySamples()
    {
        sampleTheTransform = false;
    }

    public void ResetReplaySamples()
    {
        replaySamples.Clear();
    }
    
    public void RewindReplay()
    {
        if (!doOneRetry)
        {
            //StartCoroutine(RewindReplayPlayer());
            doOneRetry = true;
        }
    }

    void ReplaySampler ()
    {
        if (sampleTheTransform)
        {
            replaySamples.Add(new ReplaySample(recordingTarget));
        }
    }

    void RewindReplayPlayer()
    {
        if (doOneRetry)
        {
            if (!doFirstReplayFrame)
            {
                replayIndex = replaySamples.Count - 1;
                doFirstReplayFrame = true;
            }
            if (replayIndex >= 0)
            {
                Stack tempStack = replaySamples[replayIndex].TargetIngredient.GetComponent<Stack>();
                if (tempStack != null)
                {
                    Destroy(tempStack);
                }

                replaySamples[replayIndex].TargetIngredient.parent = parentIngredients;

                replaySamples[replayIndex].SetIngredientToAllSampledProperties();

                replayIndex--;
            }
            else
            {
                ResetReplaySamples();

                //Resetting the stacks
                GameObject[] allIngredients = GameObject.FindGameObjectsWithTag("Ingredients");
                
                for (int i = 0; i < allIngredients.Length; i++)
                {
                    allIngredients[i].AddComponent<Stack>();
                }
                doOneRetry = false;
                doFirstReplayFrame = false;
            }
        }
    }

    //IEnumerator ReplaySampler(Transform targetIngredient)
    //{
    //    sampleTheTransform = true;

    //    while (sampleTheTransform)
    //    {
    //        replaySamples.Add(new ReplaySample(targetIngredient, targetIngredient.position, targetIngredient.rotation));
    //        //yield return new WaitForFixedUpdate();
    //        yield return new WaitForSeconds(1f / 60f);
    //    }
    //    yield return null;
    //}


    //IEnumerator RewindReplayPlayer()
    //{
    //    for (int i = replaySamples.Count - 1; i >= 0; i--)
    //    {
    //        Stack tempStack = replaySamples[i].TargetIngredient.GetComponent<Stack>();
    //        if (tempStack != null)
    //        {
    //            Destroy(tempStack);
    //        }

    //        replaySamples[i].TargetIngredient.parent = parentIngredients;

    //        replaySamples[i].SetIngredientToAllSampledProperties();
    //        //yield return new WaitForFixedUpdate();
    //        yield return new WaitForSeconds(1f / 60f);
    //    }
    //    ResetReplaySamples();

    //    //Resetting the stacks
    //    GameObject[] allIngredients = GameObject.FindGameObjectsWithTag("Ingredients");
    //    for (int i = 0; i < allIngredients.Length; i++)
    //    {
    //        allIngredients[i].AddComponent<Stack>();
    //    }
    //    doOneRetry = false;
    //    yield return null;
    //}
    #endregion
}

public class ReplaySample
{
    private Transform _targetIngredient;
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;


    public Transform TargetIngredient
    {
        get => _targetIngredient;
    }

    public Vector3 TargetPosition
    {
        get => _targetPosition;

        set
        {
            _targetPosition = value;
        }
    }

    public Quaternion TargetRotation
    {
        get => _targetRotation;

        set
        {
            _targetRotation = value;
        }
    }

    public ReplaySample (Transform targetIngredient, bool useLocals = false)
    {
        _targetIngredient = targetIngredient;
        _targetPosition = useLocals ? targetIngredient.localPosition : targetIngredient.position;
        _targetRotation = useLocals ? targetIngredient.localRotation : targetIngredient.rotation;
    }

    public ReplaySample(Transform targetIngredient, Vector3 targetPosition, Quaternion targetRotation)
    {
        _targetIngredient = targetIngredient;
        _targetPosition = targetPosition;
        _targetRotation = targetRotation;
    }

    public void SetIngredientToSampledPosition()
    {
        _targetIngredient.position = _targetPosition;
    }

    public void SetIngredientToSampledRotation()
    {
        _targetIngredient.rotation = _targetRotation;
    }

    public void SetIngredientToAllSampledProperties()
    {
        SetIngredientToSampledPosition();
        SetIngredientToSampledRotation();
    }
}
