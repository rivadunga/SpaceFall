using UnityEngine;
using System.Collections;


/**
 *   Handle the algorithms and behavior related to the Artificial Inteligent
 *   of the player
 */

public class AI : MonoBehaviour
{
    public int  decisionLevels = 1;
    public int  decisionRadius = 100;
    public bool bfsOrDfs       = true;
    public bool showEdges      = true;
    public bool showNodes      = true;
    public bool showPath       = true;



    private Transform  playerReference;
    private PlayerData playerData;

    private int   NODES_PER_LEVEL = 18;
    private float INF             = 100000000;

    private Vector3[] nodePositions;
    private float[]   nodeValues;
    private float[, ] nodeEdges;
    private ArrayList path;


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
        return new float[] { 0, rad, -rad, rad / Mathf.Sqrt(2), -rad / Mathf.Sqrt(2) };
    }

    //Initialize nodes positions

    private Vector3[] initializeShellNodes()
    {
        Vector3[] nodes = new Vector3[(decisionLevels * NODES_PER_LEVEL) + 1];
        nodes [0] = Vector3.zero;
        int iNode = 1;

        for (int w = 0; w < decisionLevels; w++) {
            float rad = decisionRadius * (w + 1);

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
            int currentRad = decisionRadius * (j + 1);

            for (int i = 0; i < NODES_PER_LEVEL; i++) {
                int parent = (j * NODES_PER_LEVEL) + 1 + i;

                for (int w = 0; w < NODES_PER_LEVEL * 2; w++) {
                    int child = ((j - 1) * NODES_PER_LEVEL) + 1 + w;

                    if (child < 0) { continue; }
                    float distance = (nodePositions[parent] - nodePositions[child]).magnitude;

                    if ((distance <= currentRad) && (edges[child, parent] == 0) && (edges[parent, child] == 0)) {
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
            int currentRad = decisionRadius * (j + 1);

            for (int i = 0; i < NODES_PER_LEVEL; i++) {
                int     nodeInd = (j * NODES_PER_LEVEL) + 1 + i;
                Vector3 nodePos = playerPos + nodePositions[nodeInd];

                //Ponderate checkpoint
                int     currentCheck = playerData.getCheckpoint();
                Vector3 checkPos     = GameObject.Find("checkpoint_" + currentCheck).transform.position;
                float   checkValue   = (nodePos - checkPos).magnitude;
                values[nodeInd] += checkValue;


                //Ponderate meteors
                for (int w = 0; w < meteors.Length; w++) {
                    Vector3 meteorPos = meteors[w].transform.position;
                    float   radius    = meteors[w].transform.lossyScale.x * meteors[w].GetComponent<SphereCollider>().radius;


                    if ((meteorPos - nodePos).magnitude < radius + decisionLevels * (decisionRadius / 4)) {
                        values[nodeInd] += 300;
                    }
                }
            }
        }
        return values;
    }

    private void adjustPonderation()
    {
        float max = Mathf.Max(nodeValues);

        nodeValues[0] = max;                 //Origin
        float min = Mathf.Min(nodeValues);
        nodeValues[0] = min;

        //Adjust values
        for (int i = 1; i < nodeValues.Length; i++) {
            nodeValues[i] = (nodeValues[i] - min) / (max - min);
        }

        for (int j = 0; j < nodeEdges.GetLength(0); j++) {
            for (int i = 0; i < nodeEdges.GetLength(1); i++) {
                if (nodeEdges[i, j] == 0) {
                    nodeEdges[i, j] = INF;
                }
            }
        }
    }

    float interval = 0;
    void OnDrawGizmos()
    {
        initializeTransform();
        playerData    = GetComponent<PlayerData>();
        nodePositions = initializeShellNodes();
        nodeEdges     = initializeShellEdges();

        nodeValues = updateStaticValues();
        adjustPonderation();

        if (interval >= 2) {
            StartCoroutine(getPath());
            interval = 0;
        }
        else{
            interval += Time.deltaTime;
        }
        if (showNodes) { drawNodes(); }
        if (showEdges) { drawEdges(); }
        if (showPath) { drawPath(path); }
        nextDir = nodePositions[(int)path[1]];
    }

    IEnumerator getPath()
    {
        GraphPath graph = new GraphPath(nodeValues, nodeEdges, decisionLevels);

        path = bfsOrDfs ? graph.getRouteBFS() : graph.getRouteDFS();
        yield return null;
    }

    private Vector3 nextDir;
    public Vector3 getNextDir()
    {
        return nextDir;
    }

    void drawNodes()
    {
        Vector3 playerPos = playerReference.position;

        for (int i = 1; i < nodeValues.Length; i++)
        {
            float value = nodeValues[i];
            Gizmos.color = new Color(value, 1 - value, 0, 1);
            Gizmos.DrawSphere(playerPos + nodePositions[i], 5);
        }
    }

    void drawEdges()
    {
        Vector3 playerPos = playerReference.position;

        for (int j = 0; j < nodeEdges.GetLength(0); j++) {
            for (int i = 0; i < nodeEdges.GetLength(1); i++) {
                if (nodeEdges[i, j] < INF) {
                    Debug.DrawLine(
                        playerPos + nodePositions[i], playerPos + nodePositions[j], new Color(1, 1, 1, 0.07f));
                }
            }
        }
    }

    void drawPath(ArrayList path)
    {
        Vector3 playerPos = playerReference.position;

        for (int i = 0; i < path.Count - 1; i++) {
            Debug.DrawLine(
                playerPos + nodePositions[(int)path[i]], playerPos + nodePositions[(int)path[i + 1]], new Color(1, 1, 0));
        }
    }
}
