using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : Singleton<CheckpointManager>
{
    [SerializeField] List<Vector3> checkpoints;
    [SerializeField] GameObject player;

    public Vector3 lastCheckpointPos;
    private int checkpointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        print("Checkpoint manager start");
        checkpoints = new List<Vector3>();
        if (SceneManager.GetActiveScene().name != "Title")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            findCheckPoints();
        }
    }

    public void findCheckPoints()
    {
        GameObject levelCheckpoints = GameObject.FindGameObjectWithTag("Checkpoint");
        if (levelCheckpoints.transform.childCount == 0)
        {
            print("There are no checkpoints");
        }
        for (int i=0; i< levelCheckpoints.transform.childCount; i++)
        {
            checkpoints.Add(levelCheckpoints.transform.GetChild(i).transform.position);
        }
    }

    public void loadCheckpoint()
    {
        player.transform.position = lastCheckpointPos;
    }

    public void setCheckpoint(Checkpoint cpt)
    {
        lastCheckpointPos = cpt.transform.position;
    }

    public void setCheckpoint(int index)
    {
        if (index >= checkpoints.Count)
        {
            checkpointIndex = 0;
            Debug.LogError("Set checkpoint index to invalid index");
        }
        else
        {
            checkpointIndex = index;
        }
        lastCheckpointPos = checkpoints[checkpointIndex];
    }

    public void sceneChanged()
    {
        findCheckPoints();
        checkpointIndex = 0;
    }
}
