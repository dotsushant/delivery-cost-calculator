# Guidelines

Please write code in C# and/or JavaScript. 

When writing your code, keep the following points in mind:
- Your code should be production ready
- Your code should be understandable and maintainable by other developers
- Your code should be robust and handle error situations
- Your code should be bug free, compile and work
- Source code must be submitted in ZIP format (no binaries)

# Exercise

Write a program that receives four integer inputs to indicate the weight and size of a parcel for postage, and returns the cost of delivery. 
The user inputs are:

1. Weight (kg)
2. Height (cm)
3. Width (cm)
4. Depth (cm)

The rules for calculating the cost of delivery in order of priority:

|Priority|Rule|Condition|Cost|
|-|-|-|-|
|1 (Highest)|Reject|If the weight exceeds 50kg|N/A|
|2|Heavy Parcel|If the weight exceeds 10kg|$15 x Weight (kg)|
|3|Small Parcel|If the volume is less than 1500|$0.05 x Volume|
|4|Medium Parcel|If the volume is less than 2500|$0.04 x Volume|
|5 (Lowest)|Large Parcel||$0.03 x Volume|

> Volume is calculated by Height x Width x Depth.

> The rule matched with the highest priority should be used to calculate the cost of delivery


# Examples

## Example 1
Enter Weight in kg: 10

Enter Height in cm: 20

Enter Width in cm: 5

Enter Depth: 20

Category: Medium Parcel

Cost: $80

## Example 2

Enter Weight in kg: 22

Enter Height in cm: 5

Enter Width in cm: 5

Enter Depth: 5

Category: Heavy Parcel

Cost: $330

## Example 3

Enter Weight in kg: 2

Enter Height in cm: 3

Enter Width in cm: 10

Enter Depth: 12

Category: Small Parcel

Cost: $18

## Example 4

Enter Weight in kg: 110

Enter Height in cm: 20

Enter Width in cm: 55

Enter Depth: 120

Category: Rejected

Cost: N/A