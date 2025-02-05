using System;
using UnityEngine;

// Derived class that inherits from the base class "Car"
/* Explanation
Inheritance
1. Base Class Constructor Call:
- The SolarCar constructor uses : base(...) to initialize shared properties (e.g., name, TopSpeed, Acceleration) defined in the Car base class.
- This ensures that common initialization logic doesn't need to be reimplemented in SolarCar.
2. Shared Properties:
currentSpeed, currentFuel, and MaxFuel are inherited from the Car class and reused in SolarCar without redefinition.
3. Shared Behavior:
The base.Move() method is called to reuse the Car class’s behavior for increasing speed and consuming fuel, ensuring that SolarCar behaves like any other car before adding its own specific functionality.

Polymorphism
1. Method Overriding:
The Move() method in SolarCar overrides the virtual Move() method in the Car class. This allows SolarCar to extend the behavior of the base class by adding the Recharge() functionality.

2. Runtime Behavior:
If a Car reference points to a SolarCar object, calling Move() will execute the SolarCar version of the method, including recharging logic. This demonstrates runtime polymorphism.

Why It Matters
Inheritance: Simplifies code reuse by allowing SolarCar to build on top of Car without duplicating its common attributes and behaviors.
Polymorphism: Enables flexible behavior, allowing the system to handle all Car objects uniformly while respecting their specific differences at runtime.
*/
[Serializable]
public class SolarCar : Car
{
    // Property specific to SolarCar, representing how quickly the battery recharges
    private float chargeRate;

    // Constructor: Uses the base constructor (from Car) and adds additional logic for chargeRate
    public SolarCar(string name, float topSpeed, float acceleration, float maxFuel, float fuelConsumption, float chargeRate)
        : base(name, topSpeed, acceleration, maxFuel, fuelConsumption) // Calls the base class constructor
    {
        this.chargeRate = chargeRate; // Assigns the solar-specific property
    }

    // Overrides the virtual Move method in the base class "Car"
    public override void Move()
    {
        // Calls the base class's Move method to handle shared behavior (e.g., accelerating and consuming fuel)
        base.Move();

        // Adds SolarCar-specific behavior (recharging the battery) to the Move method
        Recharge();

        // Logs the current speed and battery state, demonstrating the SolarCar-specific extension of behavior
        Debug.Log(BrandName + " (Solar Car) is moving at " + currentSpeed + " km/h. Battery left: " + currentFuel);
    }

    // Specific behavior for SolarCar: Recharge the battery over time
    public void Recharge()
    {
        // Increment currentFuel based on the chargeRate unique to SolarCar
        currentFuel += chargeRate * Time.deltaTime;

        // Ensures the currentFuel does not exceed the maximum fuel capacity inherited from the base class
        if (currentFuel > MaxFuel)
        {
            currentFuel = MaxFuel;
        }

        // Logs the recharge status, highlighting the SolarCar-specific behavior
        Debug.Log(BrandName + " is recharging. Battery: " + currentFuel);
    }
}
