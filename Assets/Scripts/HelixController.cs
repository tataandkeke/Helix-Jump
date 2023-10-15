using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector2 lastTapPos;
    private Vector3 startRotation;

    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helixLevelPrefab;

    public List<Stage> allStages = new List<Stage>();
    private float helixDistance;
    private List<GameObject> spawnedLevels = new List<GameObject>();

    // Start is called before the first frame update
    private void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + 0.1f);
        LoadStage(0);
    }

    void Update()
    {
        // Rotating Helix function
        if (Input.GetMouseButton(0))
        {
            Vector2 curTapPos = Input.mousePosition;

            if (lastTapPos == Vector2.zero)
            {
                lastTapPos = curTapPos;
            }

            float delta = lastTapPos.x - curTapPos.x;
            lastTapPos = curTapPos;

            transform.Rotate(Vector2.up * delta);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTapPos = Vector2.zero;
        }

    }



    public void LoadStage(int stageNumber)
    {
        //this makes sure the stageNumber arguement is within 0 and the stage count
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];

        //checks if the stage is null and returns
        if (stage == null)
        {
            Debug.LogError("No Stage " + stageNumber + " found in allStages List. are all stages assinged in the List?");
            return;
        }

        //sets the Camera background color to the specific stage backgroundcolor created
        Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;
        //sets the ball color to the stage ball color
        FindObjectOfType<BallController>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;


        //reset helix rotation
        transform.localEulerAngles = startRotation;

        //Destroy old levels if there are any, before we create a stage
        foreach (GameObject go in spawnedLevels)
            Destroy(go);

        //create new level / platform
        //finds the distance between each level created
        float levelDistance = helixDistance / stage.levels.Count;
        float spawnPosY = topTransform.localPosition.y;


        //creatiing the levels with for loop
        for (int i = 0; i < stage.levels.Count; i++)
        {
            //creates a new Y - the levelDistance, evry loop
            spawnPosY -= levelDistance;
            GameObject level = Instantiate(helixLevelPrefab, transform);
            Debug.Log("Levels Spawned");
            //changes the spawns level to the correct position
            level.transform.localPosition = new Vector3(0, spawnPosY, 0);
            //add the spawned levels to a New list of SpawnedLevels to track all levels spawned
            spawnedLevels.Add(level);


            //Addind Disable Parts to the spawned level
            int partsToDisable = 12 - stage.levels[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();
            //Creating the gaps of new level
            while (disabledParts.Count < partsToDisable)
            {
                //this disables a random part of the Helix gameObject child
                GameObject randompart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
                if(!disabledParts.Contains(randompart))
                {
                    randompart.SetActive(false);
                    disabledParts.Add(randompart);
                }
            }


            //tracking the remaining parts and adding color to them based on the stage setting
            List<GameObject> leftParts = new List<GameObject>();
            foreach (Transform t in level.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[stageNumber].stageLevelPartColor;
                if(t.gameObject.activeInHierarchy)
                {
                    leftParts.Add(t.gameObject);
                }
            }


            //adding death part to remaining parts
            List<GameObject> deathParts = new List<GameObject>();
            while(deathParts.Count < stage.levels[i].deathPartCount)
            {
                GameObject randompart = leftParts[(Random.Range(0, leftParts.Count))];
                if(!deathParts.Contains(randompart))
                {
                    randompart.gameObject.AddComponent<DeathPart>();
                    deathParts.Add(randompart);
                }
            }
        }

    }



}
