using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneBackground : MonoBehaviour
{
    [SerializeField] GameObject crane_sprite;
    [SerializeField] GameObject left_spawn;
    [SerializeField] GameObject right_spawn;
    List<GameObject> cranes;

    // Start is called before the first frame update
    void Start()
    {
        cranes = new List<GameObject>();
        StartCoroutine("SpawnNextCrane");
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnNextCrane()
    {
        while (true)
        {
            // cranes.Add(Instantiate<GameObject>(crane_sprite));
            int rng = Random.Range(-1, 1);
            Vector3 dir;
            Vector3 position;
            print(rng);
            if (rng >= 0)
            {
                position = (left_spawn.transform.position);
                dir = Vector3.right;
            }
            else
            {
                position = (right_spawn.transform.position);
                dir = Vector3.left;
            }
            CraneAI crane = Instantiate(crane_sprite, position, Quaternion.identity, gameObject.transform).GetComponent<CraneAI>();
            crane.InitializeValues(dir, left_spawn.transform.position, right_spawn.transform.position);
            yield return new WaitForSeconds(Random.Range(20, 30));

        }
    }
}
