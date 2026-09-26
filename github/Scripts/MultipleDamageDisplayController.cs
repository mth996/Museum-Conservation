using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class MultipleDamageDisplayController : MonoBehaviour
{
    public Canvas damageCanvas; // Canvas to display damages
    public TMP_Text damageText; // TMP_Text element to show damages

    // Toggle groups for each side of the object
    public Toggle[] frontSideToggles; // Toggles for front side damages
    public Toggle[] backSideToggles; // Toggles for back side damages
    public Toggle[] leftSideToggles; // Toggles for left side damages
    public Toggle[] rightSideToggles; // Toggles for right side damages

    // Dictionary to link damage names to toggles from all sides
    private Dictionary<string, List<Toggle>> damageToggles;

    private void Start()
    {
        // Hide the damage canvas initially
        damageCanvas.gameObject.SetActive(true);

        // Initialize the dictionary with unique damage type names and their corresponding toggles from all sides
        damageToggles = new Dictionary<string, List<Toggle>>
        {
            { "fracture", new List<Toggle> { frontSideToggles[0], backSideToggles[0], leftSideToggles[0], rightSideToggles[0] } },
            { "corrosion", new List<Toggle> { frontSideToggles[1], backSideToggles[1], leftSideToggles[1], rightSideToggles[1] } },
            { "decay", new List<Toggle> { frontSideToggles[2], backSideToggles[2], leftSideToggles[2], rightSideToggles[2] } },
            { "scratches", new List<Toggle> { frontSideToggles[3], backSideToggles[3], leftSideToggles[3], rightSideToggles[3] } }
        };

        // Add listener to each toggle in all groups
        foreach (var toggleList in damageToggles.Values)
        {
            foreach (var toggle in toggleList)
            {
                toggle.onValueChanged.AddListener(delegate { UpdateDamageDisplay(); });
            }
        }
    }

    private void UpdateDamageDisplay()
    {
        // Check if any toggle is on across all sides
        bool anyToggleOn = damageToggles.Values.Any(toggleList => toggleList.Any(toggle => toggle.isOn));
        //damageCanvas.gameObject.SetActive(anyToggleOn);

        if (anyToggleOn)
        {
            DisplaySelectedDamages();
        }
    }

    private void DisplaySelectedDamages()
    {
        // Collect unique damage types where at least one toggle is on
        var selectedDamages = damageToggles.Where(pair => pair.Value.Any(toggle => toggle.isOn)).Select(pair => pair.Key);
        string damageList = "Damages: " + string.Join(", ", selectedDamages);

        // Set the TMP_Text element to display unique selected damages
        damageText.text = damageList;
    }
}
