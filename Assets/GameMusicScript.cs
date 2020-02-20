using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicScript : MonoBehaviour
{
    public GameObject BackgroundMusicPrefab;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        GameObject BackgroundMusic = GameObject.Find("BackgroundMusic");
        if (BackgroundMusic == null)
        {
            Instantiate(BackgroundMusicPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            DontDestroyOnLoad(GameObject.Find("BackgroundMusic"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
