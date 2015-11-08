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
        nodeEdges     = initializeShellEdges();
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

    float[, ] initializeShellEdges()
    {
        float[, ] edges = new float[decisionLevels * NODES_PER_LEVEL + 1, decisionLevels * NODES_PER_LEVEL + 1];

        for (int j = 0; j < decisionLevels; j++) {
            int currentRad = 100 * (j + 1);

            for (int i = 0; i < NODES_PER_LEVEL; i++) {
                int parent = (j * NODES_PER_LEVEL) + 1 + i;

                for (int w = 0; w < NODES_PER_LEVEL * 2; w++) {
                    int child = ((j - 1) * NODES_PER_LEVEL) + 1 + w;

                    if (child < 0) { continue; }
                    float distance = (nodePositions[parent] - nodePositions[child]).magnitude;

                    if (distance <= currentRad) {
                        edges[parent, child] = distance;
                    }
                }
            }
        }
        return edges;
    }


    float[] updateStaticValues()
    {
        float[]      values      = new float[decisionLevels * NODES_PER_LEVEL + 1];
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("checkpoint");
        GameObject[] meteors     = GameObject.FindGameObjectsWithTag("meteor");
        Vector3      playerPos   = playerReference.position;

        for (int j = 0; j < decisionLevels; j++) {
            int currentRad = 100 * (j + 1);

            for (int i = 0; i < NODES_PER_LEVEL; i++) {
                int     nodeInd = (j * NODES_PER_LEVEL) + 1 + i;
                Vector3 nodePos = playerPos + nodePositions[nodeInd];

                //Ponderate checkpoint
                for (int w = 0; w < checkpoints.Length; w++) {
                    Vector3 checkPos   = checkpoints[w].transform.position;
                    float   checkValue = (nodePos - checkPos).magnitude;
                    values[nodeInd] += checkValue;
                }

                //Ponderate meteors
                for (int w = 0; w < meteors.Length; w++) {
                    Vector3 meteorPos   = meteors[w].transform.position;
                    float   meteorValue = (nodePos - meteorPos).magnitude;
                    values[nodeInd] -= meteorValue;
                }
            }
        }
        return values;
    }

    void OnDrawGizmos()
    {
        //Debug
        initializeTransform();
        nodePositions = initializeShellNodes();
        nodeEdges     = initializeShellEdges();
        //End debug

        Vector3 playerPos = playerReference.position;

        nodeValues = updateStaticValues();
        float max = Mathf.Max(nodeValues);
        nodeValues[0] = max;         //Origin
        float min = Mathf.Min(nodeValues);
        nodeValues[0] = min;         //Origin
        //Debug.Log(max + " " + min + " = " + (playerPos-GameObject.FindGameObjectsWithTag("checkpoint")[0].transform.position).magnitude);

        for (int i = 1; i < nodePositions.Length; i++)
        {
            float alpha = (nodeValues[i] - min) / (max - min);
            Gizmos.color = new Color(alpha, 1 - alpha, 0, 1);
            Gizmos.DrawSphere(playerPos + nodePositions[i], 5);
        }


        for (int j = 0; j < nodeEdges.GetLength(0); j++) {
            for (int i = 0; i < nodeEdges.GetLength(1); i++) {
                if (nodeEdges[i, j] > 0) {
                    Color color;
                    Debug.DrawLine(
                        playerPos + nodePositions[i], playerPos + nodePositions[j], new Color(1, 1, 1, 0.003f));
                }
            }
        }
    }
}
