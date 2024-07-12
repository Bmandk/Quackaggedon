using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DuckClicker
{
    public class ExpSlider : MonoBehaviour
    {
        public Slider expSlider;
        public DuckFeeder duckFeeder;
        public Animator sliderAnimator;
        public TextMeshProUGUI expText;

        private double currentExp = 0f; // Current experience points
        private double targetExp = 0f; // Target experience points to reach
        private double currentLevelExp = 0f; // Experience points in the current level
        private double levelUpExp = 0f; // Total experience points needed to level up
        private int level = 0; // Current level
        public float baseAnimationDuration = 0.8f; // Base duration for animation in seconds

        void Start()
        {
            if (expSlider == null)
            {
                Debug.LogError("Slider is not assigned.");
                return;
            }

            expSlider.minValue = 0;
            expSlider.maxValue = 1;
            expSlider.value = 0;
            levelUpExp = GetExpNeededToFinishLevel(level);
            UpdateExpText();
        }

        void Update()
        {
            if (currentExp < targetExp)
            {
                if (currentExp / targetExp >= 0.99)
                {
                    double remainingExp = targetExp - currentExp;

                    currentExp += remainingExp;
                    currentLevelExp += remainingExp;
                }
                else
                {
                    // Calculate the required animation speed to finish in baseAnimationDuration
                    double remainingExp = targetExp - currentExp;
                    double expPerSecond = remainingExp / baseAnimationDuration;

                    // Calculate the experience to be added this frame
                    double expToAdd = expPerSecond * Time.deltaTime;

                    currentExp += expToAdd;
                    currentLevelExp += expToAdd;
                }

                // Check if current level experience exceeds the level-up threshold
                DoLevelUpSequenceIfProper();

                expSlider.value = (float)(currentLevelExp / levelUpExp);
                UpdateExpText();
            }
            else if (currentLevelExp / levelUpExp >= 0.999)
            {
                DoLevelUpSequenceIfProper();
            }
        }

        private void DoLevelUpSequenceIfProper()
        {
            while (currentLevelExp / levelUpExp >= 0.999)
            {
                currentLevelExp -= levelUpExp;
                level++;
                sliderAnimator.SetTrigger("Fill");
                levelUpExp = GetExpNeededToFinishLevel(level);
                expSlider.value = 0;
            }
        }

        // This method can be called to add experience points
        public void AddExperience(double exp)
        {
            sliderAnimator.SetTrigger("Pulse");
            targetExp += exp;
            UpdateExpText();
        }

        private void UpdateExpText()
        {
            expText.text = $"{NumberUtility.FormatNumber(duckFeeder.FoodThrown)}/{NumberUtility.FormatNumber(duckFeeder.NextDuckCost)}";
        }

        // Placeholder for the actual implementation of experience needed per level
        double GetExpNeededToFinishLevel(int level)
        {
            return duckFeeder.DuckFeederStats.CalculateCost(level);
        }
    }
}
