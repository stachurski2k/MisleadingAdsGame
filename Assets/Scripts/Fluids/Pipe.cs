using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] Handle handle;
    [SerializeField] Transform pipeEnd;
    [SerializeField] float snapDistance=0.2f;
    [SerializeField] BoxCollider2D[] collidersToDeactivate;
   [SerializeField] Transform[] points;
    [SerializeField] MeshFilter filter;
    [SerializeField] PolygonCollider2D col;
    Mesh mesh;
    List<Vector3> vertices=new List<Vector3>();
    List<int> triangles=new List<int>();
    Vector2[] path;
    bool pluged=false;
    Vector3 prevHandlePos=Vector3.zero;
    private void Start()
    {
        filter.mesh=mesh=new Mesh();
        path=new Vector2[4];
        col.enabled=false;
    }
   private void Update()
   {
        if(!pluged&&NeedsUpdate()){
            CheckHandle();
            CreateMesh();
        }
   }
   bool NeedsUpdate(){
        if(prevHandlePos!=handle.transform.position){
            prevHandlePos=handle.transform.position;
            return true;
        }
        return false;
   }
   void Snap(){
        foreach (var c in collidersToDeactivate)
        {
            c.enabled=false;
        }
        pluged=true;
        handle.transform.position=pipeEnd.position;
        handle.gameObject.SetActive(false);
        CreateCollider();
   }
   void CheckHandle(){
        if((handle.transform.position-pipeEnd.position).sqrMagnitude<snapDistance*snapDistance){
            Snap();
        }
   }
   void CreatePath(int index,Vector3 p1,Vector3 p2){
        path[0]=new Vector2(p1.x,p1.y+0.25f);
        path[1]=new Vector2(p1.x,p1.y-0.25f);

        path[2]=new Vector2(p2.x,p2.y-0.25f);
        path[3]=new Vector2(p2.x,p2.y+0.25f);
        col.SetPath(index,path);
   }
   void CreateCollider(){
        col.enabled=true;   
        CreatePath(0,points[0].position,points[2].position);
        CreatePath(1,points[1].position,points[3].position);
   }
   void CreateMesh(){
        vertices.Clear();
        triangles.Clear();
        vertices.Add(points[0].position);
        vertices.Add(points[1].position);
        vertices.Add(points[2].position);
        vertices.Add(points[3].position);

        triangles.Add(2);
        triangles.Add(1);
        triangles.Add(0);

        triangles.Add(1);
        triangles.Add(2);
        triangles.Add(3);

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles,0);
   }
}
