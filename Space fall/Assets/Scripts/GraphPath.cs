using System;
using System.Collections;



/**
 *   Handle the algorithms and behavior related to the Artificial Inteligent
 *   of the player
 */

public class GraphPath
{
    private float[] nodeValues;
    private float[, ] nodeEdges;
    private int   LEVELS;
    private int   NUM_NODES;
    private float INF = 100000000;


    public GraphPath(float[] nodeValues, float[, ] nodeEdges, int levels)
    {
        this.nodeValues = nodeValues;
        this.nodeEdges  = nodeEdges;
        NUM_NODES       = nodeValues.Length;
        LEVELS          = levels;
    }

    public ArrayList getRouteBFS()
    {
        ArrayList path = new ArrayList();

        Boolean[] visited = new Boolean[NUM_NODES];
        for (int i = 0; i < NUM_NODES; i++) { visited[i] = false; }

        Queue queue = new Queue();
        queue.Enqueue(0);         //Add origin

        int it = 0;
        while (queue.Count != 0 && it <= LEVELS)
        {
            int parent = (int)queue.Dequeue();
            path.Add(parent);
            visited[parent] = true;


            ArrayList childs = new ArrayList();
            for (int i = 0; i < NUM_NODES; i++) {
                int   childPos   = i;
                float childValue = nodeValues[childPos];
                if (((nodeEdges[parent, childPos] == INF) && (nodeEdges[childPos, parent] == INF)) || //No edges
                    visited[childPos]) {                                                              //If the node isn't visited
                    childValue = INF;
                }
                childs.Add(childValue);
            }
            ArrayList values = new ArrayList(childs);
            values.Sort();
            float minValue = (float)values[0];
            int   indexMax = childs.IndexOf(minValue);

            if ((indexMax != -1) && (minValue < INF)) {
                queue.Enqueue(indexMax);
            }

            it++;
        }

        return path;
    }

    private float     minSum = 100000000;
    private ArrayList dfsPath;

    public ArrayList getRouteDFS()
    {
        Boolean[] visited = new Boolean[NUM_NODES];
        for (int i = 0; i < NUM_NODES; i++) { visited[i] = false; }
        executeDFS(0, 0, visited, 0, new ArrayList());
        return dfsPath;
    }

    private void executeDFS(int parent, int level, Boolean[] lastVisited, float sum, ArrayList lastPath)
    {
        if (level <= LEVELS) {
            ArrayList path    = new ArrayList(lastPath);
            Boolean[] visited = new Boolean[NUM_NODES];
            for (int j = 0; j < NUM_NODES; j++) { visited[j] = lastVisited[j]; }
            visited[parent] = true;
            path.Add(parent);


            ArrayList childs = new ArrayList();
            for (int i = 0; i < NUM_NODES; i++) {
                int   childPos   = i;
                float childValue = nodeValues[childPos];

                if (!(((nodeEdges[parent, childPos] == INF) && (nodeEdges[childPos, parent] == INF)) ||
                      visited[childPos])) {
                    executeDFS(childPos, (level + 1), visited, (sum + childValue), path);
                }
            }
        }
        else{
            if (level == LEVELS + 1) {
                if (sum < minSum) {
                    minSum  = sum;
                    dfsPath = lastPath;
                }
            }
        }
    }
}
