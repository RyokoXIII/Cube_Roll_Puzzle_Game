using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    public GameObject subPlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            subPlayer.transform.parent = this.transform;
            Combine(subPlayer);
        }
    }

    void Combine(GameObject cube)
    {
        Destroy(this.gameObject.GetComponent<BoxCollider>());

        MeshFilter[] meshFilters = this.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combines = new CombineInstance[meshFilters.Length];

        int i = 0;
        while(i < meshFilters.Length)
        {
            combines[i].mesh = meshFilters[i].sharedMesh;
            combines[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }

        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combines, true);
        meshFilter.mesh.RecalculateBounds();
        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.Optimize();

        this.gameObject.AddComponent<BoxCollider>();
        this.transform.gameObject.SetActive(true);

        Destroy(cube);
    }
}
