using DuckClicker;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(DuckFeederStats))]
public class DuckCostDrawer : PropertyDrawer
{
    private TextField[] m_CostFields;
    private const int PRE_CALCULATED_COSTS = 20;
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        VisualElement root = new VisualElement();
        SerializedProperty baseFoodSerializedProperty = property.FindPropertyRelative("baseFoodPerDuck");
        PropertyField baseFoodPerDuck = new PropertyField(baseFoodSerializedProperty);
        SerializedProperty growthRateSerializedProperty = property.FindPropertyRelative("growthRate");
        PropertyField growthRate = new PropertyField(growthRateSerializedProperty);
        PropertyField foodCost = new PropertyField(property.FindPropertyRelative("foodCost"));

        Foldout foldout = new Foldout {text = "Duck Cost"};
        foldout.Add(baseFoodPerDuck);
        foldout.Add(growthRate);
        foldout.Add(foodCost);

        VisualElement costs = new VisualElement();
        costs.style.flexDirection = FlexDirection.Row;
        m_CostFields = new TextField[PRE_CALCULATED_COSTS];

        for (int i = 0; i < PRE_CALCULATED_COSTS; i++)
        {
            int cost = CalculateCost(i, baseFoodSerializedProperty.floatValue, growthRateSerializedProperty.floatValue);
            TextField textField = new TextField();
            textField.value = cost.ToString();
            textField.SetEnabled(false);

            m_CostFields[i] = textField;
            costs.Add(textField);
        }

        foldout.Add(costs);
        root.Add(foldout);
        
        baseFoodPerDuck.RegisterCallback<ChangeEvent<float>>(evt => Callback(evt));
        growthRate.RegisterCallback<ChangeEvent<float>>(evt => Callback(evt));
        
        void Callback(ChangeEvent<float> evt)
        {
            float baseFood = baseFoodSerializedProperty.floatValue;
            float growthRate = growthRateSerializedProperty.floatValue;
            for (int i = 0; i < PRE_CALCULATED_COSTS; i++)
            {
                m_CostFields[i].value = CalculateCost(i, baseFood, growthRate).ToString();
            }
        }
        
        return root;
    }
    
    public int CalculateCost(int ducksSpawned, float baseFoodPerDuck, float growthRate)
    {
        return Mathf.RoundToInt(baseFoodPerDuck * Mathf.Pow(growthRate, ducksSpawned));
    }
}