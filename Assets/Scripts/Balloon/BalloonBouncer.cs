using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BalloonBouncer : MonoBehaviour
{
    public float speed = 1f;

    private Vector2 dir;
    private Camera cam;
    private float halfW, halfH;

    void Awake()
    {
        cam = Camera.main;
        // random gentle direction
        dir = UnityEngine.Random.insideUnitCircle.normalized;
        // cache sprite half-size in world units so we don't clip edges
        var sr = GetComponent<SpriteRenderer>();
        var ext = sr.bounds.extents;
        halfW = ext.x;
        halfH = ext.y;
    }

    void Update()
    {
        transform.position += (Vector3)(dir * speed * Time.deltaTime);

        Vector3 pos = transform.position;
        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        // reflect off vertical edges
        if (pos.x - halfW < min.x || pos.x + halfW > max.x)
        {
            dir.x *= -1f;
            pos.x = Mathf.Clamp(pos.x, min.x + halfW, max.x - halfW);
        }
        // reflect off horizontal edges
        if (pos.y - halfH < min.y || pos.y + halfH > max.y)
        {
            dir.y *= -1f;
            pos.y = Mathf.Clamp(pos.y, min.y + halfH, max.y - halfH);
        }

        transform.position = pos;
    }
}
