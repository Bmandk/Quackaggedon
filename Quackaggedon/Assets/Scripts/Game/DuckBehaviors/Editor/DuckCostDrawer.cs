using DuckClicker;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(DuckFeeder.DuckCost))]
public class DuckCostDrawer : PropertyDrawer
{
    private const int k_PrecalculatedCosts = 20;
    private TextField[] m_CostFields = new TextField[k_PrecalculatedCosts];
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        VisualElement root = new VisualElement();
        PropertyField baseFoodPerDuck = new PropertyField(property.FindPropertyRelative("baseFoodPerDuck"));
        PropertyField growthRate = new PropertyField(property.FindPropertyRelative("growthRate"));
        
        Foldout foldout = new Foldout {text = "Duck Cost"};
        foldout.Add(baseFoodPerDuck);
        foldout.Add(growthRate);
        
        DuckFeeder feeder = (DuckFeeder) property.serializedObject.targetObject;
        VisualElement costs = new VisualElement();
        costs.style.flexDirection = FlexDirection.Row;
        m_CostFields = new TextField[k_PrecalculatedCosts];
        
        for (int i = 0; i < k_PrecalculatedCosts; i++)
        {
            int cost = feeder.duckCost.CalculateCost(i);
            TextField textField = new TextField();
            textField.value = cost.ToString();
            textField.SetEnabled(false);
            
            m_CostFields[i] = textField;
            costs.Add(textField);
        }
        
        foldout.Add(costs);
        
        baseFoodPerDuck.RegisterCallback<ChangeEvent<float>>(evt => Callback(evt));
        growthRate.RegisterCallback<ChangeEvent<float>>(evt => Callback(evt));
        
        root.Add(foldout);

        void Callback(ChangeEvent<float> evt)
        {
            for (int i = 0; i < k_PrecalculatedCosts; i++)
            {
                int cost = feeder.duckCost.CalculateCost(i);
                m_CostFields[i].value = cost.ToString();
            }
        }
        
        return root;
    }
}