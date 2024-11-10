using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraViewportHandler : MonoBehaviour
{
    public enum Constraint
    {
        Landscape,
        Portrait
    }

    #region FIELDS

    public Color wireColor = Color.white;
    [FormerlySerializedAs("UnitsSize")] public float unitsSize = 1; // size of your scene in unity units
    public Constraint constraint = Constraint.Portrait;
    public static CameraViewportHandler Instance;
    public new Camera camera;

    public bool executeInUpdate;

    private float width;

    private float height;

    //*** bottom screen
    private Vector3 bl;
    private Vector3 bc;

    private Vector3 br;

    //*** middle screen
    private Vector3 ml;
    private Vector3 mc;

    private Vector3 mr;

    //*** top screen
    private Vector3 tl;
    private Vector3 tc;
    private Vector3 tr;

    #endregion

    #region PROPERTIES

    public float Width
    {
        get { return width; }
    }

    public float Height
    {
        get { return height; }
    }

    // helper points:
    public Vector3 BottomLeft
    {
        get { return bl; }
    }

    public Vector3 BottomCenter
    {
        get { return bc; }
    }

    public Vector3 BottomRight
    {
        get { return br; }
    }

    public Vector3 MiddleLeft
    {
        get { return ml; }
    }

    public Vector3 MiddleCenter
    {
        get { return mc; }
    }

    public Vector3 MiddleRight
    {
        get { return mr; }
    }

    public Vector3 TopLeft
    {
        get { return tl; }
    }

    public Vector3 TopCenter
    {
        get { return tc; }
    }

    public Vector3 TopRight
    {
        get { return tr; }
    }

    #endregion

    #region METHODS

    private void Awake()
    {
        camera = GetComponent<Camera>();
        Instance = this;
        ComputeResolution();
    }

    private void ComputeResolution()
    {
        float leftX, rightX, topY, bottomY;

        if (constraint == Constraint.Landscape)
        {
            camera.orthographicSize = 1f / camera.aspect * unitsSize / 2f;
        }
        else
        {
            camera.orthographicSize = unitsSize / 2f;
        }

        height = 2f * camera.orthographicSize;
        width = height * camera.aspect;

        float cameraX, cameraY;
        cameraX = camera.transform.position.x;
        cameraY = camera.transform.position.y;

        leftX = cameraX - width / 2;
        rightX = cameraX + width / 2;
        topY = cameraY + height / 2;
        bottomY = cameraY - height / 2;

        //*** bottom
        bl = new Vector3(leftX, bottomY, 0);
        bc = new Vector3(cameraX, bottomY, 0);
        br = new Vector3(rightX, bottomY, 0);
        //*** middle
        ml = new Vector3(leftX, cameraY, 0);
        mc = new Vector3(cameraX, cameraY, 0);
        mr = new Vector3(rightX, cameraY, 0);
        //*** top
        tl = new Vector3(leftX, topY, 0);
        tc = new Vector3(cameraX, topY, 0);
        tr = new Vector3(rightX, topY, 0);
    }

    private void Update()
    {
#if UNITY_EDITOR

        if (executeInUpdate)
            ComputeResolution();

#endif
    }

    void OnDrawGizmos()
    {
        Gizmos.color = wireColor;

        Matrix4x4 temp = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        if (camera.orthographic)
        {
            float spread = camera.farClipPlane - camera.nearClipPlane;
            float center = (camera.farClipPlane + camera.nearClipPlane) * 0.5f;
            Gizmos.DrawWireCube(new Vector3(0, 0, center),
                new Vector3(camera.orthographicSize * 2 * camera.aspect, camera.orthographicSize * 2, spread));
        }
        else
        {
            Gizmos.DrawFrustum(Vector3.zero, camera.fieldOfView, camera.farClipPlane, camera.nearClipPlane,
                camera.aspect);
        }

        Gizmos.matrix = temp;
    }

    #endregion
} // class