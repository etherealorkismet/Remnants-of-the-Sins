using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.UI.Extensions;

public class Drawing : MonoBehaviour
{
    public UILineRenderer line;

    public List<Vector2> points = new List<Vector2>();

    public void UpdateLine(Vector2 position)
    {
        if (points.Count == 0)
        {
            AddPoint(position);
            return;
        }

        if (Vector2.Distance(points[points.Count - 1], position) > 5f)
        {
            AddPoint(position);
        }
    }

    void AddPoint(Vector2 point)
    {
        points.Add(point);
        line.Points = points.ToArray();
    }
}
