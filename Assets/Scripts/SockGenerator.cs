//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SockGenerator : MonoBehaviour
{
    [System.Serializable]
    public class Pattern
    {
        public int width, height, maxW, maxH;
        public List<Vector2> points;
    }

    //[SerializeField] List<GameObject> Patterns;
    public List<Pattern> PatternList = new List<Pattern>();
    public List<Sprite> FeatureList = new List<Sprite>();
    //TODO make sure alpha levels are solid
    public List<Color> ColorList = new List<Color>();
    public List<Sprite> SockBaseList = new List<Sprite>();
    public GameObject[] GenerateSockPile(int N)
    {
        List<GameObject> result = new List<GameObject>();
        //List<>
        //number of unique patterns
        int PATTERNS = 1 + (int)N / 2;
        int FEATURES = 1 + (int)Mathf.Pow(N,1 / 3);
        int COLORS = 1 + (int)Mathf.Pow(N, 1 / 3);
        int SHAPES = 1 + (int)Mathf.Pow(N, 1 / 3);

        for (int s = 0; s < N; s++)
        {
            //TODO select these at random
            Sprite SockShape = SockBaseList[0];
            Color SockColor = ColorList[Random.Range(0, Mathf.Max(COLORS, ColorList.Count))];
            Sprite FeatureSprite = FeatureList[Random.Range(0, Mathf.Max(FEATURES, FeatureList.Count))];
            Pattern SockPattern = PatternList[Random.Range(0, Mathf.Max(PATTERNS, PatternList.Count))];
            //TODO make sure not equal to sockcolor
            Color FeatureColor = ColorList[Random.Range(0, Mathf.Max(COLORS, ColorList.Count))];

            GameObject newSock = new GameObject();
            newSock.transform.position = new Vector3(0, 0, 0);

            newSock.AddComponent<SpriteRenderer>();
            SpriteRenderer SockRenderer = newSock.GetComponent<SpriteRenderer>();
            SockRenderer.sprite = SockShape;
            SockRenderer.color = Color.white;

            newSock.AddComponent<Sock>();
            Sock SockSock = newSock.GetComponent<Sock>();

            newSock.AddComponent<SockFeature>();
            SockFeature SockSockFeature = newSock.GetComponent<SockFeature>();
            SockSockFeature.FeatureSprite = FeatureSprite;

            //TODO maxW i maxH
            SockSockFeature.FEATURE_NUMBER = 1;
            //TODO kiedys to sie losuje
            SockSockFeature.SockFeatureType = FeatureType.multi;
            //TODO FeatureAreaW & H
            SockSockFeature.DEFAULT_PATTERN_SIZE = 100;
            SockSockFeature.FeatureColor = FeatureColor;

            SockSockFeature.SetSockFeaturePointmap(SockPattern.points);
            //PatternList[Random.Range(0, Mathf.Max(PatternList.Count, PATTERNS))];

            GameObject SockBase = new GameObject();
            SockBase.transform.position = new Vector3(0, 0, 0);

            SockBase.AddComponent<SpriteRenderer>();
            SpriteRenderer SockBaseRenderer = SockBase.GetComponent<SpriteRenderer>();

            SockBase.transform.SetParent(newSock.transform);
            SockBaseRenderer.sprite = SockShape;
            SockBaseRenderer.color = Color.white;
 
            SockSock.MultiFeature = SockSockFeature;
            SockSock.SockColor = SockColor;
            SockSock.SockBaseRenderer = SockBaseRenderer;
            result.Add(newSock);
        }
        result.AddRange(result);
        GameObject[] arr = new GameObject[2 * N];
        for (int i = 0; i < 2* N ; i++)
        {
            int j = Random.Range(0, 2*N - i);
            arr[i] = result[j];
            result.RemoveAt(j);
        }
        return arr; ;
    }
}
