using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGalleryProject
{
    class VehicleTools
    {
        static public bool IsLicensePlate(string data)
        {
            int number;
            // Precondition: The entered license plate must be between 7 to 9 characters long, the first two characters must be numbers, the 6th and 7th characters (5th and 6th index) must be numbers, and the 3rd character (2nd index) must always be a letter.
            if (data.Length > 6 && data.Length < 10
                && int.TryParse(data.Substring(0, 2), out number)
                && IsLetter(data.Substring(2, 1)))
            {
                // If the conditions are met for plates in the format 11A1111, it is a valid plate.
                if (data.Length == 7 && int.TryParse(data.Substring(3), out number))
                {
                    return true;
                }
                // If the conditions are met for plates in the format 11AA111 and 11AA1111, it is a valid plate.
                else if (data.Length < 9 && IsLetter(data.Substring(3, 1)) && int.TryParse(data.Substring(4), out number))
                {
                    return true;
                }
                // If the conditions are met for plates in the format 11AAA11, 11AAA111, and 11AAA1111, it is a valid plate.
                else if (IsLetter(data.Substring(3, 2)) && int.TryParse(data.Substring(5), out number))
                {
                    return true;
                }
            }
            return false;    // If none of these conditions are met, it is not a valid plate.
        }

        // We took the data as a string type for the string methods to work, and then checked the element at the 0th index against its ASCII value to compare it as a character.
        static public bool IsLetter(string data)
        {
            data = data.ToUpper();

            for (int i = 0; i < data.Length; i++)
            {
                int code = (int)data[i]; // Gets the ASCII table value of the character.
                if ((code >= 65 && code <= 90) != true) // If the input is outside the ASCII table values for uppercase letters, the method returns false.
                {
                    return false;
                }
            }

            return true;
        }

        static public string GetText(string prompt)
        {
            int number;

            // The TryParse method produces a bool result.
            // If the entered string data can be converted to an int, an error is thrown, and text input is requested.
            // When valid input is provided, the entered input is returned.

            do
            {
                try
                {
                    Console.Write(prompt);
                    string input = Console.ReadLine().ToUpper();

                    if (int.TryParse(input, out number))
                    {
                        throw new Exception("Input not recognized. Please try again.");
                    }
                    else if (input == "X")
                    {
                        return input;
                    }
                    else
                    {
                        return input;
                    }

                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }

            } while (true);

        }

        static public int GetNumber(string message)
        {
            int number;

            // The TryParse method returns a bool variable.
            // If the method cannot convert the entered data to an int, it will throw an error and ask for input again.
            // If the condition is met, the entered data is returned.

            do
            {
                try
                {
                    Console.Write(message);
                    string input = Console.ReadLine().ToUpper();

                    if (int.TryParse(input, out number))
                    {
                        return number;
                    }
                    else if (input == "X")
                    {
                        throw new Exception("Exit");
                    }
                    else
                    {
                        throw new Exception("Input not recognized. Please try again.");
                    }
                }
                catch (Exception e)
                {
                    if (e.Message == "Exit")
                    {
                        throw new Exception("Exit");
                    }
                    else
                    {
                        Console.WriteLine(e.Message);
                    }
                }

            } while (true);

        }
        static public string GetLicensePlate(string message)
        {
            string licensePlate;
            do
            {
                try
                {
                    Console.Write(message);
                    licensePlate = Console.ReadLine().ToUpper();

                    if (licensePlate == "X")
                    {
                        return "X";
                    }

                    if (!IsLicensePlate(licensePlate))
                    {
                        throw new Exception("You cannot enter a license plate in this format. The license plate must be in the format 11AAA11, 11AAA111, or 11AAA1111. Please try again.");
                    }

                    return licensePlate;
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            } while (true);
        }

        static public string GetVehicleType()
        {
            Console.WriteLine("Vehicle type: ");
            Console.WriteLine("For SUV, press 1");
            Console.WriteLine("For Hatchback, press 2");
            Console.WriteLine("For Sedan, press 3");

            while (true)
            {

                Console.Write("Vehicle Type: ");
                string selection = Console.ReadLine().ToUpper();
                if (selection == "X")
                {
                    throw new Exception("Exit");
                }

                switch (selection)
                {
                    case "1":
                        return "SUV";

                    case "2":
                        return "Hatchback";

                    case "3":
                        return "Sedan";

                    default:
                        Console.WriteLine("Input not recognized. Please try again.");
                        break;
                }

            }
        }
    }
}
