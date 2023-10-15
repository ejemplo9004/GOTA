using UnityEngine;
using UnityEditor;

public class Hexagon : MonoBehaviour
{
    [SerializeField] private bool walkable;
    [SerializeField] private bool navigable;
    [SerializeField] private bool flyable;
    [SerializeField] private bool bakeable;

    [SerializeField] private Equipo team;

    private void Start()
    {
        if (navigable || flyable)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void SetTeam(Equipo newTeam)
    {
        team = newTeam;
    }
    public bool Walkable => walkable;
    public bool Navigable => navigable;
    public bool Flyable => flyable;
    public bool Bakeable => bakeable;
    public Equipo Team => team;
}
