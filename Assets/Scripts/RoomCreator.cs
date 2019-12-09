using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RoomCreator : MonoBehaviour
{
    public MeshDivider walls;
    public GameObject bottomWall;
    public MeshDivider floor;
    public Vector2 setSize;
    Vector2 size;
    public Vector2 doorUp, doorDown, doorLeft, doorRight;
    Vector2 up, down, left, right;
    // Start is called before the first frame update
    void Start()
    {
        CreateRoom();
    }

    // update is called once per frame
    void Update()
    {
        if (size != setSize || doorUp != up || doorDown != down || doorLeft != left || doorRight != right)
        {
            up = doorUp;
            left = doorLeft;
            right = doorRight;
            down = doorDown;
            size = setSize;
            CreateRoom();
        }
        if (StripForBuild.Strip)
        {
            CreateRoom();
        }
        else
        {
            if (transform.childCount == 0)
            {
                CreateRoom();
            }
            else
            {
                if (!transform.GetChild(transform.childCount - 1).name.Contains("Clone"))
                {
                    CreateRoom();
                }
            }
        }
    }

    void CreateRoom()
    {
        if (size.x == 0 || size.y == 0)
        {
            return;
        }
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (transform.GetChild(i).name.Contains("Clone"))
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
        if (StripForBuild.Strip)
        {
            return;
        }
        MeshDivider newFloor = Instantiate(floor, transform.position, Quaternion.identity, transform);
        newFloor.transform.position = transform.position;
        newFloor.newScale = new Vector3(size.x, size.y, newFloor.newScale.z);
        newFloor.transform.eulerAngles = new Vector3(90, 0, 0);
        if (up.y == 0)
        {
            CreateWall(size.x, Vector3.forward * size.y / 2, 0);
        }
        else if (up.x == 0 || up.x + up.y == size.x)
        {
            if (up.y != size.x)
            {
                CreateWall(size.x - up.y, (up.x == 0 ? 1 : -1) * Vector3.right * up.y / 2 + Vector3.forward * size.y / 2, 0);
            }
        }
        else
        {
            CreateWall(up.x, -Vector3.right * (size.x / 2 - up.x / 2) + Vector3.forward * size.y / 2, 0);
            CreateWall(size.x - up.x - up.y, -Vector3.right * (size.x / 2 - (up.x + up.y) - (size.x - up.x - up.y) / 2) + Vector3.forward * size.y / 2, 0);
        }
        if (down.y == 0)
        {
            CreateWall(size.x, -Vector3.forward * size.y / 2, 180);
        }
        else if (down.x == 0 || down.x + down.y == size.x)
        {
            if (down.y != size.x)
            {
                CreateWall(size.x - down.y, (down.x == 0 ? 1 : -1) * Vector3.right * down.y / 2 - Vector3.forward * size.y / 2, 180);
            }
        }
        else
        {
            CreateWall(down.x, -Vector3.right * (size.x / 2 - down.x / 2) - Vector3.forward * size.y / 2, 180);
            CreateWall(size.x - down.x - down.y, -Vector3.right * (size.x / 2 - (down.x + down.y) - (size.x - down.x - down.y) / 2) - Vector3.forward * size.y / 2, 180);
        }
        if (right.y == 0)
        {
            CreateWall(size.y, Vector3.right * size.x / 2, 90);
        }
        else if (right.x == 0 || right.x + right.y == size.y)
        {
            if (right.y != size.y)
            {
                CreateWall(size.y - right.y, (right.x == 0 ? 1 : -1) * Vector3.forward * right.y / 2 + Vector3.right * size.x / 2, 90);
            }
        }
        else
        {
            CreateWall(right.x, -Vector3.forward * (size.y / 2 - right.x / 2) + Vector3.right * size.x / 2, 90);
            CreateWall(size.y - right.x - right.y, -Vector3.forward * (size.y / 2 - (right.x + right.y) - (size.y - right.x - right.y) / 2) + Vector3.right * size.x / 2, 90);
        }
        if (left.y == 0)
        {
            CreateWall(size.y, -Vector3.right * size.x / 2, -90);
        }
        else if (left.x == 0 || left.x + left.y == size.y)
        {
            if (left.y != size.y)
            {
                CreateWall(size.y - left.y, (left.x == 0 ? 1 : -1) * Vector3.forward * left.y / 2 - Vector3.right * size.x / 2, -90);
            }
        }
        else
        {
            CreateWall(left.x, -Vector3.forward * (size.y / 2 - left.x / 2) - Vector3.right * size.x / 2, -90);
            CreateWall(size.y - left.x - left.y, -Vector3.forward * (size.y / 2 - (left.x + left.y) - (size.y - left.x - left.y) / 2) - Vector3.right * size.x / 2, -90);
        }
        RoomSpawns rs = GetComponent<RoomSpawns>();
        if (rs != null)
        {
            rs.Setup(this);
        }
    }

    void CreateWall(float length, Vector3 pos, float rot)
    {
        MeshDivider newWall = Instantiate(walls, transform.position, Quaternion.identity, transform);
        newWall.newScale = new Vector3(length, walls.newScale.y, walls.newScale.z);
        newWall.transform.eulerAngles = new Vector3(0, rot, 0);
        newWall.transform.position = transform.position + Vector3.up * walls.newScale.y / 2 + pos;
    }

    void CreateBottomWall(float length, Vector3 pos, float rot)
    {
        GameObject newWall = Instantiate(bottomWall, transform.position, Quaternion.identity, transform);
        newWall.GetComponent<BoxCollider>().size = new Vector3(length, walls.newScale.y, walls.newScale.z + 1);
        newWall.GetComponent<BoxCollider>().center -= Vector3.forward * 0.4f;
        newWall.transform.position = transform.position + Vector3.up * walls.newScale.y / 2 + pos;
    }
}