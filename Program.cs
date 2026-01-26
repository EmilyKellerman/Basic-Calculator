// Basic calculator demo done in class Program
using System;
using System.Diagnostics;

Console.WriteLine("Welcome to the Calculator");
Console.WriteLine("Press any key to continue to continue: ");
string operation = Console.ReadLine().ToUpper();



CCalculator calculator = new CCalculator();
while( operation != "X")
{
    Console.WriteLine("Enter an operation (+, -, *, /): ");
    Console.WriteLine("Press X to exit");
    operation = Console.ReadLine().ToUpper();

    //First number
    Console.WriteLine("Enter the first number: ");
    int input1 = int.Parse(Console.ReadLine());

    //Second number
    Console.WriteLine("Enter the second number: ");
    int input2 = int.Parse(Console.ReadLine());


    switch (operation)
    {
        case "+":
            Console.WriteLine(calculator.Add(input1, input2));
        break;

        case "-":
            Console.WriteLine(calculator.Subtract(input1, input2));
        break;

        case "*":
            Console.WriteLine(calculator.Multiply(input1, input2));
        break;

        case "/":
            Console.WriteLine(calculator.Add(input1, input2));
        break;

        default:
            Console.WriteLine("Unexpected error please try again");
        break;
    }

    CalculationRequest request = new CalculationRequest(input1, input2, CCalculator.Operations.Add );
    int result = calculator.calculate(request.a, request.b, request.Operations);

}

