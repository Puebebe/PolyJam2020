using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SockFeature : MonoBehaviour
{
    public Sprite FeatureSprite;
    Texture2D SockFeatureTexture;
    public int FEATURE_NUMBER;
    public Texture2D debugz;
    public FeatureType SockFeatureType;
    List<Vector2> SockFeaturePointmap;
    public int SockFeaturePatternAreaW, SockFeaturePatternAreaH;
    public int DEFAULT_PATTERN_SIZE;

    private void Start()
    {
        /*
        DEFAULT_PATTERN_SIZE = 100;
        FEATURE_NUMBER = 3;
        */
    }

    void Awake()
    {
        if (FeatureSprite != null)
        {
            SockFeatureTexture = FeatureSprite.texture;
        }
        else SockFeatureTexture = null;
        SockFeaturePointmap = new List<Vector2>();
        GeneratePointmap();
        SockFeaturePatternAreaW = SockFeaturePatternAreaH = 0;

        //this is default, should add a way to change this ;)
        //SockFeatureType = FeatureType.multi;
    }

    //function populates list with random 2D points with values 0 - 1, hopefully while considering the sprite size
    public void GeneratePointmap()
    {
        switch (SockFeatureType)
        {
            case FeatureType.single:
                {
                    Debug.Log("Single FeatureType");
                    //default single behaviour
                    SockFeaturePointmap.Add(new Vector2(1f, 1f));
                    break;
                }
            case FeatureType.multi:
                {
                    //TODO calculate and substitute the 3 value!!!
                    for (int i = 0; i < FEATURE_NUMBER; i++)
                    {
                        //Debug.Log("Multi FeatureType");
                        //default random behaviour without constraints
                        float dx = Random.Range(0f, 1f), dy = Random.Range(0f, 1f);
                        SockFeaturePointmap.Add(new Vector2(dx, dy));
                        Debug.Log("Random point:" + dx + " " + dy);
                        //TODO consider constraints (random rectangle population with equal-ish spacing)
                    }

                    break;
                }
            case FeatureType.zigzag:
                {
                    Debug.Log("Zigzag FeatureType");
                    //TODO make a zigzag pattern script here
                    SockFeaturePointmap.Add(new Vector2(1f, 1f));

                    break;
                }
            default:
                {
                    Debug.Log("GeneratePointmap encountered unknown FeatureType");
                    break;
                }
        }
    }
    //function returns concatenated texture of size (w x h) from list of randomised points describing where it inserts a preloaded sprite
    public Texture2D GenerateSockTexture(int width, int height)
    {
        Debug.Log("Generating Texture" + width + " x " + height + " with " + SockFeatureTexture.width + " x " + SockFeatureTexture.height);
        //need to make sure default rectangle size (non zero inbetween sprite)
        if (SockFeaturePatternAreaW == 0 || SockFeaturePatternAreaH == 0)
        {
            //TODO default value 50, plz check this and adjust!!!
            SockFeaturePatternAreaW = SockFeaturePatternAreaH = DEFAULT_PATTERN_SIZE;
            Debug.Log("Default pattern area set: " + DEFAULT_PATTERN_SIZE);
        }
        //TODO test if patternTexture is not empty
        Texture2D PatternTexture = new Texture2D(SockFeaturePatternAreaW + SockFeatureTexture.width, SockFeaturePatternAreaH + SockFeatureTexture.height);
        //TODO errorcheck
        for (int i = 0; i < PatternTexture.width; i++)
        {
            for (int j = 0; j < PatternTexture.height; j++)
            {
                PatternTexture.SetPixel(i, j, Color.clear);
            }
        }
        //what happenned here? plz check if this does not generate weird stuff :c
        //PatternSprite = Sprite.Create(PatternTexture, new Rect(new Vector2(PatternTexture.width, PatternTexture.height), new Vector2(1f, 1f)), new Vector2(0, 0));

        //DONT SCREW THIS UP!! sprite "concatenation" operation
        //Debug.Log("Random points made: " + SockFeaturePointmap.Count);

        /*
        #region DebugLoop
        for (int i = 0; i < SockFeatureTexture.width; i++)
        {
            for (int j = 0; j < SockFeatureTexture.height; j++)
            {
                if (SockFeatureTexture.GetPixel(i, j).a == 1)
                {
                    Debug.Log(SockFeatureTexture.GetPixel(i, j).r + " " + SockFeatureTexture.GetPixel(i,j).g + " " + SockFeatureTexture.GetPixel(i,j).b);
                }
            }
        }
        #endregion
        */

        foreach (Vector2 featurePoint in SockFeaturePointmap)
        {
            //Debug.Log("Creating Pattern Texture from next point");
            for (int i = 0; i < SockFeatureTexture.width; i++)
            {
                for (int j = 0; j < SockFeatureTexture.height; j++)
                {
                    //Debug.Log("[" + ((int)(featurePoint.x * SockFeaturePatternAreaW) + i) + "][" + ((int)(featurePoint.y * SockFeaturePatternAreaH) + j) + "] <- [" + i + "][" + j + "](" + SockFeatureTexture.GetPixel(i, j).r + ", " + SockFeatureTexture.GetPixel(i, j).g + ", " + SockFeatureTexture.GetPixel(i, j).b + ", " + SockFeatureTexture.GetPixel(i, j).a + ")");
                    PatternTexture.SetPixel(Mathf.RoundToInt((featurePoint.x * SockFeaturePatternAreaW) + i), Mathf.RoundToInt((featurePoint.y * SockFeaturePatternAreaH) + j),
                      new Color(SockFeatureTexture.GetPixel(i, j).r, SockFeatureTexture.GetPixel(i, j).g, SockFeatureTexture.GetPixel(i, j).b, SockFeatureTexture.GetPixel(i,j).a));
                }
            }
        }

        PatternTexture.Apply(true);



        //here we should have the patterntexture ready, lets make a full texture now
        Texture2D SockTexture = new Texture2D(width, height);
        //TODO errorcheck
        //fill blank
        for (int i = 0; i < SockTexture.width; i++)
        {
            for (int j = 0; j < SockTexture.height; j++)
            {
                SockTexture.SetPixel(i, j, Color.clear);
            }
        }
        //fill with featurepattern
        for (int i = 0; i < SockTexture.width; i++)
        {
            for (int j = 0; j < SockTexture.height; j++)
            {
                if (PatternTexture.GetPixel(i % PatternTexture.width, j % PatternTexture.height).a > 0)
                {
                    Color col = PatternTexture.GetPixel(i % PatternTexture.width, j % PatternTexture.height);
                    SockTexture.SetPixel(i, j, col);
                    //Debug.Log("col at " + i + " - " + j +" : " + col);
                }

            }
        }
        SockTexture.Apply();
        //here the sockfeaturetexture should be ready, lets return it
        return SockTexture;
    }
}
