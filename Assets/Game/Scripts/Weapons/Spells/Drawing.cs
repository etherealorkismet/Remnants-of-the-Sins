using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class Drawing : MonoBehaviour
{
    public LineRenderer linerenderer;

    List<Vector2> points;

    public void UpdateLine(Vector2 position)
    {
        if (points == null)    //if there are no points yet (the start of a drawing, it will create a list then add the first point and its position into the list)
        {
            points = new List<Vector2>();
            SetPoint(position);
            return;
        }

        if (Vector2.Distance(points.Last(), position) > .1f) //checks the distance between the two points, if its less than .1, it is not a new point yet.
        {
            SetPoint(position);
        }
    }

    void SetPoint(Vector2 point)
    {
        points.Add(point);

        linerenderer.positionCount = points.Count;
        linerenderer.SetPosition(points.Count - 1, point);
    }
}
