using Unity.VisualScripting;
using UnityEngine;

public class AntFactory : UnitFactory
{
    [SerializeField] private AntUnit antPrefab;

    public override IUnit GetProduct(Vector3 position)
    {
        // create a Prefab instance and get the product component
        GameObject instance = Instantiate(antPrefab.gameObject, position, Quaternion.identity);
        AntUnit newProduct = instance.GetComponent<AntUnit>();

        // each product contains its own logic
        newProduct.Initialize();

        return newProduct;
    }
}
