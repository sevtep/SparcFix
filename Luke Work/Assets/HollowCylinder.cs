using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HollowCylinder : MonoBehaviour {
    public float radius = 1f;
    public float height = 1f;
    public int numSegments = 32;
    public float thickness = 0.1f;

    private void Start() {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[numSegments * 4];
        int[] triangles = new int[numSegments * 12];

        for (int i = 0; i < numSegments; i++) {
            float angle = 2 * Mathf.PI * i / numSegments;
            float x = Mathf.Cos(angle);
            float z = Mathf.Sin(angle);

            // Outer cylinder vertices
            vertices[i] = new Vector3(x * (radius + thickness), height / 2f, z * (radius + thickness));
            vertices[i + numSegments] = new Vector3(x * (radius + thickness), -height / 2f, z * (radius + thickness));

            // Inner cylinder vertices
            vertices[i + numSegments * 2] = new Vector3(x * radius, height / 2f, z * radius);
            vertices[i + numSegments * 3] = new Vector3(x * radius, -height / 2f, z * radius);
        }

        // Generate triangles for the outer and inner cylinders
        int ti = 0;
        for (int i = 0; i < numSegments; i++) {
            int next = (i + 1) % numSegments;

            // Outer cylinder triangles
            triangles[ti++] = i;
            triangles[ti++] = i + numSegments;
            triangles[ti++] = next;

            triangles[ti++] = next + numSegments;
            triangles[ti++] = next;
            triangles[ti++] = i + numSegments;

            // Inner cylinder triangles
            triangles[ti++] = i + numSegments * 2;
            triangles[ti++] = next + numSegments * 2;
            triangles[ti++] = i + numSegments * 3;

            triangles[ti++] = next + numSegments * 3;
            triangles[ti++] = i + numSegments * 3;
            triangles[ti++] = next + numSegments * 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
