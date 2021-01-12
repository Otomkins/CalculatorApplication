# Calculator Application v0.2.0-beta

[TOC]

### Release Notes:

***v0.2.0-beta*** is the second minor release of the application. The original code was refactored to become organised and efficient. More encompassing unit tests were created for the application as a whole. Previously unavailable functionality, such as the use of decimal points, was added. Several bugs that caused the application to solve equations in undesirable ways and become out of sync with the interface were tackled.

### Summary:

A WPF Calculator Application. With a simple and classic design, the user can perform common equations with positive, negative and decimal numbers. The application displays the last equation that was calculated and the user can backspace and clear entries to alter the input before this calculation.

### User Guide:

The application is designed in a way that replicates common desktop and mobile device calculators, making it familiar to most who will use it. The number keypad is set out traditionally and input from these digits is reflected in the desired equation.

Surrounding them are the buttons for further functionality:

1. **Clear Buttons ( CE, C )**: The **Clear Entry Button ( CE )** is used to clear the digits from the part of the equation that is currently being entered in, whether this be before or after the operation. The **Clear Button ( C )** is used to erase all digits that is being stored in the calculator. This resets the calculator to a neutral position. When a divide by 0 is attempted, an error message will appear on the calculator, locking the ability for further calculations. This is also reset only through the **Clear Button**.
2. **Backspace Button ( <= )**: This button allows the user to remove the previously entered digit. This works on either side of the operation and decimal points are counted as one digit that can be removed. This is so that the user has more flexibility when inputting equations and will not need to reset the application. As of ***v0.2.0-beta***, there is no functionality for the backspace to remove the second set of digits, the operation and then have the ability to start removing the first set of digits.
3. **Operation Buttons( +, -, \*, /, = )**: The standard four operations are reflected in these buttons; **Addition ( + )**, **Subtraction ( - )**, **Multiplication ( \* )** and **Division ( / )**. These follow conventional software arithmetic operators with division and multiplication being represented as such. These operations can be changed at any time during the equation without the surrounding digits being affected. As of ***v0.2.0-beta***, there is no functionality to remove the operation and then proceed to make changes to the previously inputted value. The **Equals Button ( = )** will solve the inputted equation and present it on the calculator's screen. This button cannot be used before inputting a valid equation.
4. **Positive/Negative Button  (+- )**: This button changes the currently selected value to a negative number and then back to a positive. As of ***v0.2.0-beta***, there is no functionality to declare a negative number before the input of at least one digit between 1 - 9. This means that the button must be used after the desired input.
5. **Decimal Point Button ( . )**: This button is used to add a decimal point to the end of the current value and this will be represented in the calculations. If a decimal is placed at the start of a value, it will be represented as '0.' in the calculator. If a value is inputted into the equation which ends in a decimal point, this will be ignored. The **Backspace Button** counts the decimal as a single backspace, allowing for more flexibility with the editing of the value.

At the top of the application is the calculator screen. This will display the individual buttons as they are inputted into the application. The top left of the screen will display the last equation in its entirety or the current equation as it is being inputted. Once the **Equals Button** is used, both the equation and the result will be shown. Using the **Operation Buttons** from this point will allow you to use this result in a further equation. This result can not be added onto, changed or removed partially. The calculator will reset the equation when a new digit button is pressed. The previous result will then be ignored.

### Architecture:

***v0.2.0-beta*** included refactoring and organising of the code. Three projects are now present in the `Calculator_Application` solution: `Calculator_Code` (Backend code), `Calculator_Tests` (Unit Test code) and `Calculator_WPF` (Frontend code). Initially, in previous versions of the application, the C# code for the project was coupled closely with the frontend `.xaml` code. Even though this was a small application, I decided to separate the concerns within the code by creating a service layer.

Now with this separation, the code has fewer dependencies, with higher cohesion and lower coupling. This made it easier to test the code effectively and made the project scalable and more organised. The code dependencies are as follows:

`MainWindow.xaml  >>  MainWindow.xaml.cs  >>  CalculatorCode.cs  >>  CalculatorService.cs`

Loosely created with the MVC design pattern in mind, `CalculatorCode.cs` acts as the in-between for the frontend and backend code. Various UI related exception handling is done within `MainWindow.xaml.cs` which prevents the user from using the application in undesirable ways.

The code across the project is annotated with comments regarding their nature and purpose in the application. Where applicable, these comments explain how the code interacts with other code across the project to give a more complete overview on the project.

### Future Releases:

> *"Currently, there are no plans to release major updates to this application. In the future, work may be done to expand the calculator and its functionality by adding other common calculator buttons. For the near future, work will be done to fix minor bugs and features that detract from the experience of the application. As well as this, further work will be done to the UI portion of the application to give it more of an appealing and intuitive look." - 12/01/2021*

Here is a list of minor changes that can be included in future patches:

- Ability to remove an operation and resume input/editing of the first value. Using the backspace button and the CE button(?).
- Division and Multiplication ( /, * ) can be changed on the UI to represent their common, more well-known icons.