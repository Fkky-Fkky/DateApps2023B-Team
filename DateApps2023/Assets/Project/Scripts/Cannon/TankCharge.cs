using UnityEngine;

public class TankCharge : MonoBehaviour
{
    [SerializeField]
    private Material[] materials = new Material[3];

    private GameObject inner = null;
    private Material material = null;
    // Start is called before the first frame update
    void Start()
    {
        inner = transform.GetChild(0).gameObject;
        inner.SetActive(false);
    }

    public void Charge(int energyType)
    {
        material = materials[energyType];
        inner.GetComponent<MeshRenderer>().material = material;
        inner.SetActive(true);
    }

    public void DisCharge()
    {
        inner.SetActive(false);
    }
}
