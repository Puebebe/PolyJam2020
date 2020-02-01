using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClasses : MonoBehaviour
{
    Sprite mySprite;
    public SockFeature testFeature;
    public enum FeatureType 
    {
        single,
        multi,
        zigzag
    }


    public class SockFeature
    {
        public Texture2D SockFeatureTexture;
        public FeatureType SockFeatureType;
        List<Vector2> SockFeaturePointmap;
        public int SockFeaturePatternAreaW, SockFeaturePatternAreaH;
        public int DEFAULT_PATTERN_SIZE;

        SockFeature()
        {
            SockFeatureTexture = null;
            SockFeaturePointmap = null;
            SockFeaturePatternAreaW = SockFeaturePatternAreaH = 0;
            DEFAULT_PATTERN_SIZE = 50;
            SockFeatureType = FeatureType.multi;
        }

        //function populates list with random 2D points with values 0 - 1, hopefully while considering the sprite size
        public void GeneratePointmap()
        {
            switch (SockFeatureType)
            {
                case FeatureType.single:
                    {
                        //default single behaviour
                        SockFeaturePointmap.Add(new Vector2(1f, 1f));

                        break;
                    }
                case FeatureType.multi:
                    {
                        //TODO calculate and substitute the 3 value!!!
                        for (int i = 0; i < 3; i++)
                        {
                            //default random behaviour without constraints
                            SockFeaturePointmap.Add(new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)));
                            //TODO consider constraints (random rectangle population with equal-ish spacing)
                        }

                        break;
                    }
                case FeatureType.zigzag:
                    {
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
            //need to make sure default rectangle size (non zero inbetween sprite)
            if (SockFeaturePatternAreaW == 0 || SockFeaturePatternAreaH == 0)
            {
                //TODO default value 50, plz check this and adjust!!!
                SockFeaturePatternAreaW = SockFeaturePatternAreaH = DEFAULT_PATTERN_SIZE;
            }
            //TODO test if patternTexture is not empty
            Texture2D PatternTexture = new Texture2D(SockFeaturePatternAreaW + SockFeatureTexture.width, SockFeaturePatternAreaH + SockFeatureTexture.height);
            //TODO errorcheck
            for (int i = 0; i < PatternTexture.width; i++)
            {
                for (int j = 0; j < PatternTexture.height; j++)
                {
                    PatternTexture.SetPixel(i, j, new Color(0, 0, 0, 0));
                }
            }
            //what happenned here? plz check if this does not generate weird stuff :c
            //PatternSprite = Sprite.Create(PatternTexture, new Rect(new Vector2(PatternTexture.width, PatternTexture.height), new Vector2(1f, 1f)), new Vector2(0, 0));

            //DONT SCREW THIS UP!! sprite "concatenation" operation
            foreach(Vector2 featurePoint in SockFeaturePointmap)
            {
                for (int i = 0; i < PatternTexture.width; i++)
                {
                    for (int j = 0; j < PatternTexture.height; j++)
                    {
                        PatternTexture.SetPixel((int)(featurePoint.x * SockFeaturePatternAreaW) + i, (int)(featurePoint.y * SockFeaturePatternAreaH) + j, SockFeatureTexture.GetPixel(i, j));
                    }
                }
            }
            //here we should have the patterntexture ready, lets make a full texture now
            Texture2D SockTexture = new Texture2D(width, height);
            //TODO errorcheck
            //fill blank
            for (int i = 0; i < SockTexture.width; i++)
            {
                for (int j = 0; j < SockTexture.height; j++)
                {
                    PatternTexture.SetPixel(i, j, new Color(0, 0, 0, 0));
                }
            }
            //fill with featurepattern
            for (int i = 0; i < SockTexture.width; i++)
            {
                for (int j = 0; j < SockTexture.height; j++)
                {
                    SockTexture.SetPixel(i, j, PatternTexture.GetPixel(i % PatternTexture.width, j % PatternTexture.height));
                }
            }
            //here the sockfeaturetexture should be ready, lets return it
            return SockTexture;
        }
    }

    class SockShape
    {
        //here should be a list of pairs: target rectangle mask and feature name?/pointer?
    }

    void MakeSpriteFromTexture()
    {
        
    }

    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>().sprite;
    }
}
