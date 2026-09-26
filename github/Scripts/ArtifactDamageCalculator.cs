using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtifactDataCollector : MonoBehaviour
{
    public Transform[] damageCategories; // Parent categories for damage types
    public TMP_Text overallResultText; // TMP_Text for overall result
    public TMP_Text[] categoryResultTexts; // TMP_Text boxes for individual category results

    public Image pieChartBaseImage; // Prefab for pie chart slices
    public Transform pieChartParent; // Parent transform for pie chart slices
    public Color[] pieColors; // Colors for each category

    private const int TotalDropdowns = 60; // Total number of dropdowns (across all categories)
    private const int MaxSeverityPerDropdown = 5; // Maximum severity value per dropdown
    private const int TotalMaxSeverity = TotalDropdowns * MaxSeverityPerDropdown; // Total possible severity

    public void CalculateAndDisplayPercentages()
    {
        float[] categoryPercentages = new float[damageCategories.Length];
        int totalSelectedSeverity = 0;

        // Step 1: Calculate the severity for each category
        for (int i = 0; i < damageCategories.Length; i++)
        {
            Transform category = damageCategories[i];
            TMP_Text categoryResultText = categoryResultTexts[i];

            int categorySeveritySum = 0;
            TMP_Dropdown[] dropdowns = category.GetComponentsInChildren<TMP_Dropdown>();

            // Sum the selected severities for this category
            foreach (TMP_Dropdown dropdown in dropdowns)
            {
                categorySeveritySum += dropdown.value;
            }

            // Calculate the percentage for this category
            float categoryPercentage = (float)categorySeveritySum / TotalMaxSeverity * 100f;
            categoryPercentages[i] = categoryPercentage;

            // Display the percentage in the UI
            categoryResultText.text = $"Category Damage: {categoryPercentage:F2}%";

            // Keep track of the total selected severity
            totalSelectedSeverity += categorySeveritySum;

            // Debugging log
            Debug.Log($"Category {i}: Severity Sum = {categorySeveritySum}, Percentage = {categoryPercentage:F2}%");
        }

        // Step 2: Calculate the overall percentage
        float overallPercentage = (float)totalSelectedSeverity / TotalMaxSeverity * 100f;
        overallResultText.text = $"Overall Damage: {overallPercentage:F2}%";

        // Debugging log for overall percentage
        Debug.Log($"Total Selected Severity = {totalSelectedSeverity}, Overall Percentage = {overallPercentage:F2}%");

        // Step 3: Draw the pie chart
        DrawPieChart(categoryPercentages);
    }

    private void DrawPieChart(float[] percentages)
    {
        // Clear existing pie slices
        foreach (Transform child in pieChartParent)
        {
            Destroy(child.gameObject);
        }

        float startAngle = 0f; // Starting angle for the first slice
        float totalPercentage = 0f;

        for (int i = 0; i < percentages.Length; i++)
        {
            if (percentages[i] <= 0f) continue; // Skip if the percentage is 0

            // Create a new slice
            Image slice = Instantiate(pieChartBaseImage, pieChartParent);
            slice.color = pieColors[i % pieColors.Length]; // Assign color
            slice.fillAmount = Mathf.Clamp01(percentages[i] / 100f); // Set fill amount
            slice.transform.localRotation = Quaternion.Euler(0f, 0f, -startAngle); // Rotate slice to correct position

            // Update the starting angle for the next slice
            startAngle += slice.fillAmount * 360f;

            // Add this percentage to the total
            totalPercentage += percentages[i];
        }

        // Add the remaining green slice
        float remainingPercentage = 100f - totalPercentage;
        if (remainingPercentage > 0f)
        {
            Image greenSlice = Instantiate(pieChartBaseImage, pieChartParent);
            greenSlice.color = Color.green; // Set the remaining slice to green
            greenSlice.fillAmount = Mathf.Clamp01(remainingPercentage / 100f); // Fill the remaining percentage
            greenSlice.transform.localRotation = Quaternion.Euler(0f, 0f, -startAngle); // Rotate to the correct position

            // Debugging log for the green slice
            Debug.Log($"Remaining Slice: Percentage = {remainingPercentage}, Start Angle = {startAngle}, Fill Amount = {greenSlice.fillAmount}");
        }
    }


    private string GetCondition(float damagePercentage)
    {
        if (damagePercentage > 75f) return "Unacceptable";
        else if (damagePercentage > 60f) return "Poor";
        else if (damagePercentage > 40f) return "Fair";
        else return "Good";
    }
}
