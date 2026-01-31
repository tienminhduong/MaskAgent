using UnityEngine;

public class TestLureable : MonoBehaviour, ILureable
{
    [SerializeField] private HumanInfo humanInfo;
    public HumanInfo HumanInfo => humanInfo;

    public int Direction;

    void Update()
    {
        transform.Translate(Direction * Time.deltaTime * Vector2.right);
    }

    public void OnLured(Role lurerRole)
    {
        Debug.Log($"Attempting to lure {humanInfo.Name} as {lurerRole}");
        if (!((ILureable)this).IsLureable(lurerRole)) return;

        Debug.Log("Lured " + humanInfo.Name);
        Direction *= -1;
    }
}
