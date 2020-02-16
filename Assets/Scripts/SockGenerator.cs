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
    public GameObject SockParent;
    public GameObject[] GenerateSockPile(int N)
    {
        List<GameObject> result = new List<GameObject>();
        //List<>
        //number of unique patterns
        int PATTERNS = 1 + (int)N / 2;
        int FEATURES = 1 + (int)Mathf.Pow(N, 1 / 3);
        int COLORS = 1 + (int)Mathf.Pow(N, 1 / 3);
        int SHAPES = 1 + (int)Mathf.Pow(N, 1 / 3);

        for (int s = 0; s < N; s++)
        {
            //TODO select these at random
            Sprite SockShape = SockBaseList[0];

            List<Color> remaingColors = new List<Color>(ColorList);
            Color SockColor = remaingColors[Mathf.Clamp(Random.Range(0, COLORS), 0, remaingColors.Count - 1)];
            remaingColors.Remove(SockColor);
            Color FeatureColor = remaingColors[Mathf.Clamp(Random.Range(0, COLORS), 0, remaingColors.Count - 1)];

            Sprite FeatureSprite = FeatureList[Mathf.Clamp(Random.Range(0, FEATURES), 0, FeatureList.Count - 1)];
            Pattern SockPattern = PatternList[Mathf.Clamp(Random.Range(0, PATTERNS), 0, PatternList.Count - 1)];

            GameObject newSock = new GameObject();
            newSock.SetActive(false);
            newSock.transform.position = new Vector3(0, 0, 0);

            SpriteRenderer SockRenderer = newSock.AddComponent<SpriteRenderer>();
            SockRenderer.sprite = SockShape;
            SockRenderer.color = Color.white;
            SockRenderer.maskInteraction = SpriteMaskInteraction.None;
            SockRenderer.sortingOrder = 1;
          
            Sock SockSock = newSock.AddComponent<Sock>();
            
            SockFeature SockSockFeature = newSock.AddComponent<SockFeature>();
            SockSockFeature.FeatureSprite = FeatureSprite;

            newSock.AddComponent<SkarpetkaController>();

            //TODO maxW i maxH
            SockSockFeature.FEATURE_NUMBER = 1;
            //TODO kiedys to sie losuje
            SockSockFeature.SockFeatureType = FeatureType.multi;
            //TODO FeatureAreaW & H
            SockSockFeature.DEFAULT_PATTERN_SIZE = 100;
            SockSockFeature.FeatureColor = FeatureColor;

            SockSockFeature.SetSockFeaturePointmap(SockPattern.points);

            GameObject SockBase = new GameObject();
            SockBase.transform.position = new Vector3(0, 0, 0);
         
            SpriteRenderer SockBaseRenderer = SockBase.AddComponent<SpriteRenderer>();

            SockBase.transform.SetParent(newSock.transform);
            SockBaseRenderer.sprite = SockShape;
            SockBaseRenderer.color = Color.white;

            SockSock.MultiFeature = SockSockFeature;
            SockSock.SockColor = SockColor;
            SockSock.SockBaseRenderer = SockBaseRenderer;

            //GameObject newSpriteMask = new GameObject();
            //newSpriteMask.AddComponent<SpriteMask>();
            //newSpriteMask.transform.SetParent(newSock.transform);
            //newSpriteMask.GetComponent<SpriteMask>().sprite = SockShape;

            GameObject newPairedSock = Instantiate(newSock);
            GameObject betterNewSock = Instantiate(newSock);
            Destroy(newSock);
            newSock = betterNewSock;

            newSock.transform.SetParent(SockParent.transform);
            newPairedSock.transform.SetParent(SockParent.transform);

            newSock.name = "Sock" + (result.Count / 2 + 1) + "A";
            newPairedSock.name = "Sock" + (result.Count / 2 + 1) + "B";

            result.Add(newSock);
            result.Add(newPairedSock);
        }

        GameObject[] arr = new GameObject[2 * N];
        for (int i = 0; i < 2 * N; i++)
        {
            int j = Random.Range(0, 2 * N - i);
            arr[i] = result[j];
            result.RemoveAt(j);
        }
        return arr; ;
    }
}
