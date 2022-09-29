using UnityEngine;

public class CuttingBoard : MonoBehaviour
{
    [SerializeField] private Transform _ingredientPosition;
    private GameObject _ingredientGO;
    private Ingredient _ingredient;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            _ingredientGO = other.gameObject;
            _ingredient = _ingredientGO.GetComponent<Ingredient>();
            _ingredient.StopMoving();
            _ingredientGO.transform.parent = _ingredientPosition.transform;
            _ingredientGO.transform.localPosition = Vector3.zero;
        }
    }
}
