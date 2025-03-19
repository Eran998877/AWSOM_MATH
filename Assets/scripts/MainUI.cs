using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;


public class MainUI : MonoBehaviour
{
    private VisualElement root;
    public GameData_script page_data;//scriptable object refrense

    private void OnEnable()
    {
        // Get the root element of the UI
        var uiDocumentInstance = GetComponent<UIDocument>();
        root = uiDocumentInstance.rootVisualElement;



        // Create the ScrollView
        CreateScrollView();
    }

    private void CreateScrollView()
    {
        // Create the ScrollView
        ScrollView scrollView = new ScrollView(ScrollViewMode.Vertical);

        // Set the ScrollView to fill the entire screen
        scrollView.style.position = Position.Absolute;
        scrollView.style.top = 0;
        scrollView.style.left = 0;
        scrollView.style.right = 0;
        scrollView.style.bottom = 0;

        

        // Set padding for the scroll view 
        scrollView.style.paddingTop = 20;
        scrollView.style.paddingBottom = 20;

        // Create and add buttons to the scroll view acording to scriptableobject
        for (int i = 1; i < page_data.levels.Length; i++)
        {
            // Create a container VisualElement for each button
            VisualElement buttonContainer = new VisualElement();
            buttonContainer.style.flexDirection = FlexDirection.Row;  // Arrange the content vertically


            // Create the button
            Button button = CreateButton(i);

            int buttonIndex = i;  // Capture the index
            button.clicked += ButtonClicked;

            // Add the button to the container
            buttonContainer.Add(button);

            // Every second element
            if (i % 2 == 0)
            {
                button.style.flexBasis = new Length(25, LengthUnit.Percent);  // setting size of buttons  
                buttonContainer.style.flexDirection = FlexDirection.RowReverse; // right end bouttons

                AddHorizontalLine(buttonContainer);

                AddVerticalLine(buttonContainer, 2);
            }

            else
            {
                button.style.flexBasis = new Length(25, LengthUnit.Percent);
                AddHorizontalLine(buttonContainer);
                AddVerticalLine(buttonContainer, -2);

            }

            // Set some height for the container 
            buttonContainer.style.height = 150;

            // Add the container to the ScrollView
            scrollView.Add(buttonContainer);
        }

        // Add the ScrollView to the root element of the UI
        root.Add(scrollView);
    }





    //creating the buttons and the labal within
    private Button CreateButton(int i)
    {
        Button button = new Button();
        button.text = page_data.levels[i].title + i.ToString() + "\n xp : " + page_data.levels[i].xp; // title of button acording to scriptableobject
        button.style.backgroundColor = new Color(0, 0, 0, 0); //transparent

        Texture2D button_defult = page_data.sprites[1].texture;// seting defualt texture for button
        button.style.backgroundImage = new StyleBackground (button_defult);
        var textElement = button.Q<TextElement>();

        // Change the text color
        textElement.style.color = new Color(0, 0, 0);  // black

        
        textElement.style.paddingTop = 50;  // lower the text ?
        

        // Change the font size
        textElement.style.fontSize = 20;

        //creating the labal within the button
        Label button_label = new Label();
        button_label.text = "start"; //could make it static
        button_label.style.backgroundColor = new Color(i, 0, 0); //trying to see what cokor happens with i
        button_label.style.display = DisplayStyle.None;

        button.Add(button_label);//adding labal


        return button;
    }

    private static void AddHorizontalLine(VisualElement buttonContainer)
    {
        // to do : make into a style?
        VisualElement horizontal_line = new VisualElement();
        horizontal_line.style.flexBasis = new Length(25, LengthUnit.Percent);
        horizontal_line.style.height = new Length(10, LengthUnit.Percent);
        horizontal_line.style.width = new Length(50, LengthUnit.Percent);
        horizontal_line.style.backgroundColor = new Color(0, 0, 0); //black
        horizontal_line.style.alignSelf = Align.Center;
        buttonContainer.Add(horizontal_line);
    }

    private static void AddVerticalLine(VisualElement buttonContainer, int pos)
    {
        // to do : doesnot fully connect --make into a style? 
        VisualElement vertical_line = new VisualElement();

        vertical_line.style.height = new Length(100, LengthUnit.Percent);
        vertical_line.style.width = new Length(10, LengthUnit.Percent);
        vertical_line.style.backgroundColor = new Color(0, 0, 0); //black
        vertical_line.style.maxWidth = new Length(2, LengthUnit.Percent);
        vertical_line.style.left = new Length(pos, LengthUnit.Percent); //adjusting the vertical line to connect with the others
        buttonContainer.Add(vertical_line);
    }

    // Handle the button click event
    private void ButtonClicked()
    {
        //animate labal
        // Access the button's internal text element (Label)
        var label = root.Q<Label>();

        // Start an animation to scale the label when the button is pressed
        AnimateLabel(label);

        // Optionally, you can also use the MouseLeave or Button.Release to reset the animation
        root.RegisterCallback<MouseLeaveEvent>((evt) => ResetLabelAnimation(label));



        Debug.Log("Button clicked!");
    }

    private void AnimateLabel(Label label)
    {
        label.style.display = DisplayStyle.Flex; // to do :add transition by scaling

    }

    private void ResetLabelAnimation(Label label)
    {
        label.style.display = DisplayStyle.None;
    }







}