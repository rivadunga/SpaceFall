using UnityEngine;
using System.Collections;


/**
 *   Handle the algorithms and behavior related to the Artificial Inteligent
 *   of the player
 */

public class AI : MonoBehaviour
{
    public int decisionLevels = 1;

    private Transform playerReference;

    private int       NODES_PER_LEVEL = 48;
    private Vector3[] nodePositions;
    private float[]   nodeValues;
    private float[, ] nodeEdges;

    void Start()
    {
        initializeTransform();
        nodePositions = initializeShellNodes();
        nodeEdges     = initializeShellEdges(nodePositions);
    }

    private void initializeTransform()
    {
        Transform[] childrens = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in childrens) {
            if (child.name == "Player_reference") {
                playerReference = child;
            }
        }
    }

    private float[] getSphereCoords(float rad)
    {
        return new float[] { 0, -0, rad, -rad, rad / Mathf.Sqrt(2), -rad / Mathf.Sqrt(2) };
    }

    private Vector3[] initializeShellNodes()
    {
        Vector3[] nodes = new Vector3[decisionLevels * NODES_PER_LEVEL + 1];
        nodes [0] = Vector3.zero;
        int iNode = 1;

        for (int w = 0; w < decisionLevels; w++) {
            float rad = 100 * (w + 1);

            //Values of the z,y and x points of the sphere
            float[] p   = getSphereCoords(rad);
            int     len = p.Length;

            //Generates permutation, example (0,rad,rad/2)
            for (int k = 0; k < len; k++) {
                for (int j = 0; j < len; j++) {
                    for (int i = 0; i < len; i++) {
                        Vector3 point;
                        point = new Vector3(p [i], p [j], p [k]);

                        //Only consider the points that are on the shell of the sphere
                        if (point.magnitude == rad) {
                            nodes [iNode] = point;
                            iNode++;
                        }
                    }
                }
            }
        }

        return nodes;
    }

    float[, ] initializeShellEdges(Vector3[] nodes)
    {
        float[, ] edges = new float[decisionLevels * NODES_PER_LEVEL + 1, decisionLevels * NODES_PER_LEVEL + 1];

        for (int j = 0; j < decisionLevels; j++) {
            int currentRad = 100 * (j + 1);

            for (int i = 0; i < NODES_PER_LEVEL; i++) {
                int parent = (j * NODES_PER_LEVEL) + 1 + i;

                for (int w = 0; w < NODES_PER_LEVEL * 2; w++) {
                    int child = ((j - 1) * NODES_PER_LEVEL) + 1 + w;

                    if (child < 0) { continue; }
                    float distance = (nodes[parent] - nodes[child]).magnitude;

                    if (distance <= currentRad) {
                        edges[parent, child] = distance;
                    }
                }
            }
        }
        return edges;
    }



    void OnDrawGizmos()
    {
        //Debug
        initializeTransform();
        nodePositions = initializeShellNodes();
        nodeEdges     = initializeShellEdges(nodePositions);
        //End debug


        Vector3 playerPos = playerReference.position;

        for (int i = 0; i < nodePositions.Length; i++)
        {
            Gizmos.color = new Color(1, 1, 0, 0.1f);
            Gizmos.DrawSphere(playerPos + nodePositions[i], 5);
        }

        for (int j = 0; j < nodeEdges.GetLength(0); j++) {
            for (int i = 0; i < nodeEdges.GetLength(0); i++) {
                if (nodeEdges[i, j] > 0) {
                    Debug.DrawLine(
                        playerPos + nodePositions[i], playerPos + nodePositions[j], new Color(1, 1, 1, 0.003f));
                }
            }
        }
    }
}
