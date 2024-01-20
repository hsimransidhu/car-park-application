using System;
using System.Collections.Generic;

class CarParkApplication
{
   /// <summary>
   /// Car Park Application Prototype using dictionaries.
   /// 
   /// </summary>
    static void Main()
    {
        // Initialize car park with a capacity of 5
        Dictionary<int, string> carPark = InitializeCarPark(5);

        while (true)
        {
            Console.WriteLine("1. Add Vehicle");
            Console.WriteLine("2. Vacate Stall");
            Console.WriteLine("3. Leave Parkade");
            Console.WriteLine("4. Manifest");
            Console.WriteLine("5. Exit");
            Console.WriteLine("========");

            Console.Write("Select option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter license number(For example,“A1A-00V” , “789-30A” ): ");
                    string license = Console.ReadLine();
                    try
                    {
                        int stallNumber = AddVehicle(carPark, license);
                        if (stallNumber != -1)
                            Console.WriteLine($"Vehicle parked at stall {stallNumber}.");
                        else
                            Console.WriteLine("No unoccupied stalls or invalid license.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case "2":
                    Console.Write("Enter stall number for vacating: ");
                    int stallVacate = int.Parse(Console.ReadLine());
                    try
                    {
                        bool  success = VacateStall(carPark, stallVacate);
                        if (success)
                            Console.WriteLine($"Stall {stallVacate} vacated successfully.");
                        else
                            Console.WriteLine("Invalid stall or already unoccupied.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case "3":
                    Console.Write("Enter license number to leave parkade: ");
                    string licenseLeave = Console.ReadLine();
                    try
                    {
                        bool success = LeaveParkade(carPark, licenseLeave);
                        if (success)
                            Console.WriteLine($"Vehicle with license {licenseLeave} left the parkade.");
                        else
                            Console.WriteLine("Invalid license or vehicle not found.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case "4":
                    string manifest = Manifest(carPark);
                    Console.WriteLine(manifest);
                    break;

                case "5":
                    Console.WriteLine("Exiting the Car Park Application. Goodbye!");
                    return;


                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    break;
            }

            Console.WriteLine();
        }
    }
    // Initializes the car park with the specified capacity and returns the initialized dictionary.
    static Dictionary<int, string> InitializeCarPark(int capacity)
    {
        Dictionary<int, string> carPark = new Dictionary<int, string>();
        for (int i = 1; i <= capacity; i++)
        {
            carPark.Add(i, null);
        }
        return carPark;
    }

    // Adds a vehicle to the first unoccupied stall in the car park and returns the stall number.
    // If no unoccupied stalls or the license is invalid, returns -1.
    static int AddVehicle(Dictionary<int, string> carPark, string license)
    {
        foreach (var kvp in carPark)
        {
            if (string.IsNullOrEmpty(kvp.Value))
            {
                if (IsValidLicense(license))
                {
                    carPark[kvp.Key] = license;
                    return kvp.Key;
                }
                else
                {
                    throw new Exception("Invalid license format.");
                }
            }
        }
        return -1; // No unoccupied stalls
    }

    // Vacates the specified stall by setting its value to null.
    // Returns true if successful, false if the stall is unoccupied or does not exist.
    static bool VacateStall(Dictionary<int, string> carPark, int stallNumber)
    {
        if (carPark.ContainsKey(stallNumber) && !string.IsNullOrEmpty(carPark[stallNumber]))
        {
            carPark[stallNumber] = null;
            return true;
        }
        return false; // Stall is unoccupied or does not exist
    }

    static bool LeaveParkade(Dictionary<int, string> carPark, string licenseNumber)
    {
        foreach (var kvp in carPark)
        {
            if (!string.IsNullOrEmpty(kvp.Value) && kvp.Value.Equals(licenseNumber))
            {
                carPark[kvp.Key] = null;
                return true;
            }
        }
        return false; // License is invalid or vehicle not found
    }

    // Finds and removes a vehicle from the car park by its license number.
    // Returns true if successful, false if the license is invalid or the vehicle is not found.
    static string Manifest(Dictionary<int, string> carPark)
    {
        string result = "Car Park Manifest:\n";
        foreach (var kvp in carPark)
        {
            result += $"{kvp.Key}: {(string.IsNullOrEmpty(kvp.Value) ? "Unoccupied" : kvp.Value)}\n";
        }
        return result;
    }

    static bool IsValidLicense(string license)
    {
        // Validate license format (alphanumeric combination of six values, separated by a hyphen)
        return System.Text.RegularExpressions.Regex.IsMatch(license, @"^[A-Za-z0-9]{3}-[A-Za-z0-9]{3}$");
    }
}
