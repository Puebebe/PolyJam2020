using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class patternofskarpetka : MonoBehaviour
{
    float randr;
    float randg;
    float randb;
    SpriteRenderer skarpetkaRenderer;
    GameObject skarpetkaPattern;
    public Sprite[] patterns;
    // Start is called before the first frame update
    void Start()
    {
        randr = Random.Range(0f, 1f);
        randg = Random.Range(0f, 1f);
        randb = Random.Range(0f, 1f);
        skarpetkaRenderer = gameObject.GetComponent<SpriteRenderer>();
        skarpetkaPattern = gameObject.transform.GetChild(0).gameObject;
        var randomPattern = skarpetkaPattern.GetComponent<SpriteRenderer>();
        randomPattern.sprite = patterns[Random.Range(0,patterns.Length)];
        randomPattern.color = new Color(randb, randr, randg);
        skarpetkaRenderer.color = new Color(randr, randg, randb);
    }

    // Update is called once per frame
    void Update()
    {

    }


}
