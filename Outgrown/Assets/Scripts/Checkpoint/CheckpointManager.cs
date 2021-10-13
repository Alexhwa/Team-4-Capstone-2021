using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] GameObject[] checkpoints;
    [SerializeField] GameObject player;

    private Preloaded preloaded;
    //private int checkpointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Title")
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        //preloaded = GameObject.FindGameObjectWithTag("Preloader").GetComponent<Preloaded>();
        Preloaded.Instance.lastCheckpointPos = transform.position;
    }

    public void loadCheckpoint()
    {
        player.transform.position = preloaded.lastCheckpointPos;
    }

    public void setCheckpoint(Checkpoint cpt)
    {
        Preloaded.Instance.lastCheckpointPos = cpt.transform.position;
    }

    public void setCheckpoint(int index)
    {
        if (index >= checkpoints.Length)
            preloaded.lastCheckpointPos = checkpoints[0].transform.position;
        else
            preloaded.lastCheckpointPos = checkpoints[index].transform.position;
    }

    public void sceneChanged()
    {
         
    }
}
