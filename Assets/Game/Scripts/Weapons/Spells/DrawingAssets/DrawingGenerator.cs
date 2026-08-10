using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

public class DrawingGenerator : MonoBehaviour , Weapon //spell cs
{
    public GameObject Line;
    Drawing activeLine;
    public TextAsset SpellsText;

    void Awake()
    {
        GameObject spellLine = GameObject.Find("SpellLine");
        activeLine = spellLine.GetComponent<Drawing>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            
            RectTransform canvasRect = activeLine.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

            Vector2 localPoint;
            //make the points relative to the screen size
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                Input.mousePosition,
                null,
                out localPoint
            );

            activeLine.UpdateLine(localPoint);
        }
    }
    public bool Use()
    {
        return false;
    }
    public bool HoldToUseMD()
    {
        if (activeLine == null)
        {
            return false;
        }

        if (activeLine.line == null)
        {
            return false;
        }

        activeLine.points.Clear();
        activeLine.line.Points = System.Array.Empty<Vector2>();

        return true;
    }

    public bool HoldToUseMU()
    {
        Debug.Log(DetectSpell());

        activeLine.points.Clear();
        activeLine.line.Points = System.Array.Empty<Vector2>();

        return true;
    }
    string DetectSpell()
    {
        List<Vector2> points = activeLine.points;
        List<Vector2> newpoints = CleanUp(points);
        Spells SpellL = ReadAllSpell();
        bool ifpass = true;
        newpoints.Add(new Vector2());
        newpoints.Add(Vector2.up);
        foreach(Spell s in SpellL.spells)
        {
            if(newpoints.Count - 2 == s.points)
            {
                foreach(test t in s.tests)
                {
                    if(t.func == "angle")
                    {
                        if(AngleCheck(newpoints[t.p1],newpoints[t.p2],newpoints[t.p3]) >= t.v + 0.35 || AngleCheck(newpoints[t.p1],newpoints[t.p2],newpoints[t.p3]) <= t.v - 0.35) // 0.1 is changable
                        {
                            ifpass = false;
                            break;
                        }
                    }
                    if(t.func == "direction")
                    {
                        if(DirectionCheck(newpoints[t.p1],newpoints[t.p2],newpoints[t.p3]) != t.v)
                        {
                            ifpass = false;
                            break;
                        }
                    }
                    if(t.func == "parellel")
                    {
                        if(ParallelCheck(newpoints[t.p1],newpoints[t.p2],newpoints[t.p3],newpoints[t.p4]) >= t.v + 0.35 || ParallelCheck(newpoints[t.p1],newpoints[t.p2],newpoints[t.p3],newpoints[t.p4]) <= t.v - 0.35) // 0.1 is changable
                        {
                            ifpass = false;
                            break;
                        }
                    }
                    if(t.func == "distance")
                    {
                        if(DistanceCheck(newpoints[t.p1],newpoints[t.p2],newpoints[t.p3]) >= t.v + 0.6 || DistanceCheck(newpoints[t.p1],newpoints[t.p2],newpoints[t.p3]) <= t.v - 0.6) // 0.1 is changable
                        {
                            ifpass = false;
                            break;
                        }
                    }
                }
                if (ifpass)
                {
                    return s.name;
                }
            }
            
        }
        return null;
    }
    List<Vector2> CleanUp(List<Vector2> p)
    {
        List<Vector2> ret = new List<Vector2>();
        foreach(Vector2 point in p)
        {
            if(ret.Count <= 1) //get first two points
            {
                ret.Add(point);
            }
            else
            {
                Vector2 point2 = ret[(ret.Count-1)];
                Vector2 point3 = ret[(ret.Count-2)];   
                
                Vector2 v1 = point2 - point3; //vector number 1
                Vector2 v2 = point - point2;//vector number 2

                /*
                
                .--------------.-------------.------>
                point3         poin2       point1
                
                */

                float dotResult = Vector2.Dot(v1.normalized, v2.normalized);
                if (dotResult > 0.8)
                {
                    ret.RemoveAt((ret.Count-1));
                }
                ret.Add(point);
            }
        }
        return ret;
    }

    float AngleCheck(Vector2 p3, Vector2 p2, Vector2 p1)
    {
        Vector2 v1 = p2 - p3; //vector number 1        
        Vector2 v2 = p1 - p2;//vector number 2
        /*
        .--------------.-------------.------>
        point3         poin2       point1
        */
        return Vector2.Dot(v1.normalized, v2.normalized);
    }

    float DirectionCheck(Vector2 p3, Vector2 p2, Vector2 p1)
    {
        Vector2 v1 = p2 - p3; //vector number 1        
        Vector2 v2 = p1 - p2;//vector number 2
        /*
        .--------------.-------------.------>
        point3         poin2       point1
        */
        return Vector3.Cross((Vector3)v1.normalized,(Vector3)v2.normalized).normalized.z;
        //clockwise is -1
        //anticlockwise is 1
    }
    float ParallelCheck(Vector2 p4, Vector2 p3, Vector2 p2, Vector2 p1)
    {
        Vector2 v1 = p3 - p4; //vector number 1        
        Vector2 v2 = p1 - p2;//vector number 2
        /*
        .--------------.-------------.------>
        point3         poin2       point1
        */
        return Vector2.Dot(v1.normalized, v2.normalized);
    }

    float DistanceCheck(Vector2 p3, Vector2 p2, Vector2 p1)
    {
        Vector2 v1 = p2 - p3; //vector number 1        
        Vector2 v2 = p1 - p2;//vector number 2
        /*
        .--------------.-------------.------>
        point3         poin2       point1
        */
        Debug.Log(v1.magnitude / v2.magnitude);
        return Math.Abs((v1.magnitude / v2.magnitude)-1);
    }



    Spells ReadAllSpell()
    {

        return JsonUtility.FromJson<Spells>(SpellsText.text);
    }
}

[System.Serializable]
public class test
{
    public int p1;
    public int p2;
    public int p3;
    public int p4;
    public string func;
    public float v;
}
[System.Serializable]
public class Spell
{
    public int points;
    public string name;
    public test[] tests;
}
[System.Serializable]
public class Spells
{
    public string[] a;
    public Spell[] spells;
}


