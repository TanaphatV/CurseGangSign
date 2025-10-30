using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject healthBarTarget;
    public Image image;

    private void Start()
    {
        if (healthBarTarget.TryGetComponent<IHealbarTarget>(out var target))
        {
            target.GetUnitHealth().onHealthChange += HealthChangeHandler;
        }
        else
            Debug.LogError($"{healthBarTarget} is not IHealthbarTarget");
    }

    void HealthChangeHandler(float amount)
    {
        image.fillAmount = amount;
    }
}

public interface IHealbarTarget
{
    public UnitHealth GetUnitHealth();
}