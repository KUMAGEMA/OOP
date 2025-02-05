using System;
using Sirenix.OdinInspector;
using UnityEngine;

// Base class (Parent) for all types of cars, demonstrating inheritance
/* Explanation
Inheritance
1. Base Class Role:
Car is designed as the base class for all car types (e.g., gasoline, solar, or other cars).
Shared properties (BrandName, TopSpeed, Acceleration, MaxFuel, FuelConsumption) ensure that all derived classes inherit these attributes without duplicating code.
2. Shared Behavior:
Methods like Refuel(), GetCurrentFuel(), and GetCurrentSpeed() are generic and can be reused directly by derived classes.
These shared methods reduce the need for reimplementation in subclasses, making the code more maintainable.
3. Encapsulation:
Protected fields (currentSpeed and currentFuel) allow derived classes to access these attributes while hiding them from external modification.

Polymorphism
Virtual Move() Method:

The Move() method is marked as virtual, allowing derived classes to override it and provide their own specific behavior (e.g., SolarCar adds recharging logic in its overridden Move() method).
This ensures the base behavior (speed increment and fuel consumption) is available for all cars, while enabling customization.
Runtime Method Resolution:

At runtime, when a Car reference points to a derived class (e.g., SolarCar), the most specific implementation of Move() is executed, demonstrating runtime polymorphism.
*/
[Serializable]
public class Car
{
    // Common properties shared by all types of cars
    public string BrandName; // Shared attribute for car branding
    public float TopSpeed; // Shared maximum speed for all cars
    public float Acceleration; // Shared acceleration rate for all cars
    public float MaxFuel; // Shared maximum fuel capacity for all cars
    public float FuelConsumtion; // Shared fuel consumption rate for all cars

    // Protected fields allow derived classes to access and modify while encapsulating from external access
    [ShowInInspector] protected float currentSpeed; // Tracks the car's current speed
    [ShowInInspector] protected float currentFuel; // Tracks the car's current fuel level

    // Constructor to initialize shared properties for all types of cars
    public Car(string brandName, float speed, float acceleration, float maxFuel, float fuelConsumtion)
    {
        this.BrandName = brandName; // Initializes the car's brand
        this.TopSpeed = speed; // Sets the maximum speed
        this.Acceleration = acceleration; // Sets the acceleration rate
        this.MaxFuel = maxFuel; // Sets the maximum fuel capacity
        this.FuelConsumtion = fuelConsumtion; // Sets the fuel consumption rate
        currentFuel = this.MaxFuel; // Initializes the car's fuel tank to maximum capacity
    }

    // Virtual method that can be overridden by derived classes, demonstrating polymorphism
    public virtual void Move()
    {
        if (currentFuel > 0) // Executes movement logic only if there is fuel
        {
            // Increase the car's speed based on its acceleration rate
            currentSpeed += Acceleration * Time.deltaTime;

            // Clamp the car's speed to ensure it doesn't exceed the maximum speed
            if (currentSpeed > TopSpeed)
                currentSpeed = TopSpeed;

            // Consume fuel over time during movement
            currentFuel -= FuelConsumtion * Time.deltaTime;
        }
    }

    // Method for refueling the car, shared by all types of cars
    public void Refuel()
    {
        currentFuel = MaxFuel; // Refills the fuel tank to its maximum capacity
    }

    // Getter method to retrieve the current fuel level, supporting encapsulation
    public float GetCurrentFuel()
    {
        return currentFuel; // Returns the car's current fuel level
    }

    // Getter method to retrieve the current speed, supporting encapsulation
    public float GetCurrentSpeed()
    {
        return currentSpeed; // Returns the car's current speed
    }

    // Method to check if the car is out of fuel, reusable by all derived classes
    public bool IsOutOfFuel()
    {
        return currentFuel <= 0; // Returns true if the car has no fuel left
    }
}
