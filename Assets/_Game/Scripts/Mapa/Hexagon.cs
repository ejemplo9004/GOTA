using UnityEngine;
using UnityEditor;

public class Hexagon : MonoBehaviour
{
    [SerializeField] private bool walkable;
    [SerializeField] private bool jumpable;
    [SerializeField] private bool flyable;
    [SerializeField] private bool bakeable;

    public bool Walkable => walkable;
    public bool Jumpable => jumpable;
    public bool Flyable => flyable;
    public bool Bakeable => bakeable;
}
