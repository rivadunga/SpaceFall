using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{
	public int decisionLevels = 1;

	private Vector3   playerPos;
	private int NODES_PER_LEVEL = 48;
	
	void Start() 
	{
		//Get the player position reference in the childs (To let use diferentes AI)
		Transform[] childrens = gameObject.GetComponentsInChildren<Transform>();
		foreach (Transform child in childrens)
			if (child.name == "Player_reference")
			    playerPos = child.position;
	}

	private float[] getSphereCoords(float rad)
	{
		return new float[] {0,-0,rad,-rad,rad/Mathf.Sqrt(2), -rad/Mathf.Sqrt(2)};
	}

	private Vector3[] getNodes()
	{
		Vector3[] nodes = new Vector3[decisionLevels * NODES_PER_LEVEL+1];
		nodes [0] = Vector3.zero;
		int iNode = 1;

		for (int w = 0; w < decisionLevels; w++) {

			float rad = 100 * (w + 1);

			//Values of the z,y and x points of the sphere
			float[] p = getSphereCoords (rad);
			int len = p.Length;


			//Generates permutation, example (0,rad,rad/2)
			for (int k = 0; k < len; k++) {
				for (int j = 0; j < len; j++) {
					for (int i = 0; i < len; i++) {
						Vector3 point;
						point = new Vector3 (p [i], p [j], p [k]);

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

	float[,] getEdges(Vector3[] nodes)
	{
		float[,] edges = new float[decisionLevels * NODES_PER_LEVEL+1,decisionLevels * NODES_PER_LEVEL+1];

		for (int j = 0; j < decisionLevels; j++) {

			int currentRad = 100 * (j + 1);

			for (int i = 0; i < NODES_PER_LEVEL; i++){
			
				int parent = (j*NODES_PER_LEVEL)+1+i;

				for (int w = 0; w < NODES_PER_LEVEL*2; w++){

					int child = ((j-1)*NODES_PER_LEVEL)+1+w;

					if (child < 0) continue; 
					float distance = (nodes[parent]-nodes[child]).magnitude;

					if (distance <= currentRad)
						edges[parent,child] = distance;					
				}
			}
		}
		return edges;
	}



	void OnDrawGizmos() 
	{
		Vector3[] nodes = getNodes ();
		float[,] edges = getEdges (nodes);

		for (int i = 0; i < nodes.Length; i++)
		{
			Gizmos.color = new Color(1,1,0,0.1f);
			Gizmos.DrawSphere (playerPos + nodes[i], 5);
		}

		for (int j = 0; j < edges.GetLength(0); j++) {
			for (int i = 0; i < edges.GetLength(0); i++){

				if (edges[i,j] > 0){
					Debug.DrawLine(
						playerPos+nodes[i],playerPos+nodes[j], new Color(1,1,1,0.01f));
				}
			}
		}		
	}

}

