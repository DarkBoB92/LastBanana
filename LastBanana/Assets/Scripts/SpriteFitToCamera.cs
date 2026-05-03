using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteAlways]
public class SpriteFitToCamera : MonoBehaviour
{
    public Camera targetCamera;

    void Start() => Fit();
    void OnEnable() => Fit();

    public void Fit()
    {
        if (targetCamera == null) targetCamera = Camera.main;
        if (targetCamera == null)
        {
            return;
        }

        var sr = GetComponent<SpriteRenderer>();
        if (sr.sprite == null)
        {
            return;
        }

        transform.localScale = Vector3.one;

        float worldHeight = targetCamera.orthographicSize * 2f;
        float worldWidth = worldHeight * targetCamera.aspect;

        Vector2 spriteSize = sr.sprite.bounds.size;

        float scaleX = worldWidth / spriteSize.x;
        float scaleY = worldHeight / spriteSize.y;

        float scale = Mathf.Max(scaleX, scaleY);
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}