using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGameObject : MonoBehaviour
{
    public int gridWidht = 4;
    public int gridHeight = 4;

    public GameObject emptyObject;
    public Transform parentObject;

    private GameObject objectInstantiated;
    public List<GameObject> ingredientsToInstatiate = new List<GameObject>();
    public List<Vector3> positions = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        if(gridWidht%2 != 0 && gridHeight%2 != 0)
        {
            return;
        }

        //for (float i = -1.5f; i <= 1.5f; i += 1.0f)
        //{
        //    for (float j = -1.5f; j <= 1.5f; j += 1.0f)
        //    {
        //        objectInstantiated = Instantiate(emptyObject, Vector3.zero, Quaternion.identity, parentObject) as GameObject;
        //        objectInstantiated.transform.position = new Vector3(i, 0.0f, j);
        //    }
        //}

        for (float i = -(gridWidht/2 - 0.5f); i <= gridWidht/2 - 0.5f; i += 1.0f)
        {
            for (float j = -(gridHeight/2 - 0.5f); j <= gridHeight/2 - 0.5f; j += 1.0f)
            {
                //objectInstantiated = Instantiate(ingredientsToInstatiate[Random.Range(0,ingredientsToInstatiate.Count)], Vector3.zero, Quaternion.identity, parentObject) as GameObject;
                //objectInstantiated.transform.position = new Vector3(i, 0.0f, j);
                positions.Add(new Vector3(i, 0, j));

            }
        }

        int randomNum = Random.Range(0, ingredientsToInstatiate.Count);
        objectInstantiated = Instantiate(ingredientsToInstatiate[randomNum], Vector3.zero, Quaternion.identity, parentObject) as GameObject;
        objectInstantiated.transform.position = positions[Random.Range(0, positions.Count)];


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
