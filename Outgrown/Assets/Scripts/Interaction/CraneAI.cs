using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneAI : MonoBehaviour
{
    Vector3 horizontalDir = Vector3.right;
    float startHeight; // 138-540
    Vector3 left;
    Vector3 right;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {
        float scale = Random.Range(0.02573985f, 0.07189397f);
        gameObject.transform.localScale += new Vector3(scale, scale, scale);
        Vector2 size = GetComponent<SpriteRenderer>().size;
        GetComponent<SpriteRenderer>().size.Set(size.x, Random.Range(400, 1200));
        startHeight = Random.Range(138, 540);
    }

    public void InitializeValues(Vector3 dir, Vector3 left, Vector3 right)
    {
        horizontalDir = dir;
        this.left = left;
        this.right = right;
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontalDir == Vector3.right)
        {
            transform.Translate(Vector3.right * 4 * Time.deltaTime);
            if (transform.position.x >= right.x)
                Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.left * 4 * Time.deltaTime);
            if (transform.position.x <= left.x)
                Destroy(gameObject);
        }
        // transform.position = Vector3.MoveTowards(start, end, 3 * Time.deltaTime);
    }
}
