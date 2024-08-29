using System;
using System.Collections.Generic;

namespace CarGalleryProject
{
    class Application
    {
        Gallery CarGallery = new Gallery();
        int counter = 0;

        public void Run()
        {
            ShowMenu();
            while (true)
            {
                string choice = GetChoice();
                Console.WriteLine();
                switch (choice)
                {
                    case "R":
                    case "1":
                        RentCar();
                        break;
                    case "D":
                    case "2":
                        ReturnCar();
                        break;
                    case "L":
                    case "3":
                        ListCars("On Rent");
                        break;
                    case "G":
                    case "4":
                        ListCars("In Gallery");
                        break;
                    case "A":
                    case "5":
                        ListCars("CarNotFound");
                        break;
                    case "C":
                    case "6":
                        CancelRental();
                        break;
                    case "N":
                    case "7":
                        AddNewCar();
                        break;
                    case "S":
                    case "8":
                        DeleteCar();
                        break;
                    case "I":
                    case "9":
                        ShowInformation();
                        break;
                    case "X":
                        // If "X" is pressed on the selection screen, nothing happens, and a new choice is requested.
                        break;
                    case "EXIT":
                        Exit();
                        break;
                    default:
                        Console.WriteLine("Invalid operation. Please try again.");
                        counter++;
                        break;
                }
            }
        }

        public void ShowMenu()
        {
            Console.WriteLine("Gallery Automation                    ");
            Console.WriteLine("1- Rent a Car (R)                     ");
            Console.WriteLine("2- Return a Car (D)                   ");
            Console.WriteLine("3- List Rented Cars (L)               ");
            Console.WriteLine("4- List Cars in Gallery (G)           ");
            Console.WriteLine("5- List All Cars (A)                  ");
            Console.WriteLine("6- Cancel Rental (C)                  ");
            Console.WriteLine("7- Add a Car (N)                      ");
            Console.WriteLine("8- Delete a Car (S)                   ");
            Console.WriteLine("9- Show Information (I)               ");
        }

        public string GetChoice()
        {
            if (counter != 10)
            {
                Console.WriteLine();
                Console.Write("Your choice: ");
                return Console.ReadLine().ToUpper();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Sorry, I cannot understand you. The program is terminating.");
                return "EXIT";
            }
        }

        public void Exit()
        {
            Environment.Exit(0);
        }

        public void RentCar()
        {
            Console.WriteLine("-Rent a Car-");
            Console.WriteLine();

            try
            {
                if (CarGallery.Cars.Count == 0)
                {
                    throw new Exception("There are no cars in the gallery.");
                }
                else if (CarGallery.CarsInGalleryCount == 0)
                {
                    throw new Exception("All cars are rented out.");
                }

                string licensePlate;
                while (true)
                {
                    // We check if the license plate provided by the user is valid and if the car exists using the methods we created.
                    // We also check if the user entered a number using the method SayiMi, and if a letter was entered instead, an error is thrown and a new input is requested.
                    // Finally, we pass the user input to the gallery's rental method to complete the rental process.

                    // To prevent asking for a car license plate if no cars are available for rent.
                    licensePlate = VehicleTools.GetLicensePlate("License plate of the car to be rented: ");

                    // If "X" is entered, exit the method.
                    if (licensePlate == "X")
                    {
                        return;
                    }

                    string status = CarGallery.GetStatus(licensePlate);

                    if (status == "On Rent")
                    {
                        Console.WriteLine("The car is currently rented. Please choose another car.");
                    }
                    else if (status == "CarNotFound")
                    {
                        Console.WriteLine("There is no car with this license plate in the gallery.");
                    }
                    else
                    {
                        break;
                    }
                }

                int duration = VehicleTools.GetNumber("Rental duration: ");
                CarGallery.RentCar(licensePlate, duration);
                Console.WriteLine();
                Console.WriteLine("The car with license plate " + licensePlate.ToUpper() + " has been rented for " + duration + " hours.");
            }
            catch (Exception e)
            {
                if (e.Message == "Exit")
                {
                    return;
                }
                Console.WriteLine(e.Message);
            }
        }

        public void ReturnCar()
        {
            Console.WriteLine("-Return a Car-");
            Console.WriteLine();
            try
            {
                if (CarGallery.Cars.Count == 0)
                {
                    throw new Exception("There are no cars in the gallery.");
                }
                else if (CarGallery.CarsOnRentCount == 0)
                {
                    throw new Exception("There are no cars on rent.");
                }

                // The license plate provided by the user is checked to see if it is valid and if the car is rented. If not, an error is thrown, and a new input is requested.
                // Finally, we pass the user input to the gallery's return method to complete the return process.
                string licensePlate;

                while (true)
                {
                    licensePlate = VehicleTools.GetLicensePlate("License plate of the car to be returned: ");

                    // If "X" is entered, exit the method.
                    if (licensePlate == "X")
                    {
                        return;
                    }
                    string status = CarGallery.GetStatus(licensePlate);
                    if (status == "In Gallery")
                    {
                        Console.WriteLine("Invalid input. The car is already in the gallery.");
                    }
                    else if (status == "CarNotFound")
                    {
                        Console.WriteLine("There is no car with this license plate in the gallery.");
                    }
                    else
                    {
                        break;
                    }
                }
                CarGallery.ReceiveCar(licensePlate);
                Console.WriteLine();
                Console.WriteLine("The car has been returned to the gallery.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ListCars(string status)
        {
            if (status == "On Rent")
            {
                Console.WriteLine("-Cars on Rent-");
            }
            else if (status == "In Gallery")
            {
                Console.WriteLine("-Cars in Gallery-");
            }
            else
            {
                Console.WriteLine("-All Cars-");
            }

            Console.WriteLine();
            ListCarDetails(CarGallery.GetCarList(status));
        }

        public void ListCarDetails(List<Car> list)
        {
            // If the total number of cars is 0, show a warning that there are no cars to list.
            if (list.Count == 0)
            {
                Console.WriteLine("No cars to list.");
                return;
            }
            Console.WriteLine("License Plate".PadRight(14) + "Brand".PadRight(12) + "Rental Price".PadRight(12) + "Car Type".PadRight(12) +
                    "Rental Count".PadRight(12) + "Status");
            Console.WriteLine("".PadRight(70, '-'));

            foreach (Car item in list)
            {
                Console.WriteLine(item.LicensePlate.PadRight(14) + item.Brand.PadRight(12) + item.RentalPrice.ToString().PadRight(12) + item.VehicleType.ToString().PadRight(12) + item.RentalCount.ToString().PadRight(12) + item.Status);
            }
        }

        public void CancelRental()
        {
            Console.WriteLine("-Cancel Rental-");
            Console.WriteLine();
            try
            {
                if (CarGallery.CarsOnRentCount == 0)
                {
                    throw new Exception("There are no cars on rent.");
                }

                // The license plate provided by the user is checked to see if it is valid and if the car is rented. If not, an error is thrown, and a new input is requested.
                // Finally, we pass the user input to the gallery's cancel rental method to complete the cancellation.
                string licensePlate;

                while (true)
                {
                    licensePlate = VehicleTools.GetLicensePlate("License plate of the car to cancel rental: ");

                    // If "X" is entered, exit the method.
                    if (licensePlate == "X")
                    {
                        return;
                    }

                    string status = CarGallery.GetStatus(licensePlate);
                    if (status == "In Gallery")
                    {
                        Console.WriteLine("Invalid input. The car is already in the gallery.");
                    }
                    else if (status == "CarNotFound")
                    {
                        Console.WriteLine("There is no car with this license plate in the gallery.");
                    }
                    else
                    {
                        break;
                    }
                }

                CarGallery.CancelRental(licensePlate);
                Console.WriteLine();
                Console.WriteLine("The rental has been canceled.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AddNewCar()
        {
            Console.WriteLine("-Add a Car-");
            Console.WriteLine();
            try
            {
                string licensePlate; // We define the variable outside the loop because we cannot pass the variable from inside the loop to the method.
                while (true)
                {
                    licensePlate = VehicleTools.GetLicensePlate("License Plate: ");

                    // If "X" is entered, exit the method.
                    if (licensePlate == "X")
                    {
                        return;
                    }

                    // The license plate is checked using the method we created in VehicleTools. If it is invalid, an error is thrown, and a new input is requested.

                    if (CarGallery.GetStatus(licensePlate) == "On Rent" || CarGallery.GetStatus(licensePlate) == "In Gallery") // If the condition is met, a car with this license plate is already in the gallery, and new information is requested.
                    {
                        Console.WriteLine("A car with the same license plate already exists. Please check the license plate you entered.");
                    }
                    else
                    {
                        break; // If the condition is met, the car does not exist, and new car information is gathered.
                    }
                }
                // We pass the user input to the gallery's add car method using the methods defined in VehicleTools, and a new car is added.
                string brand = VehicleTools.GetText("Brand: ");
                // If "X" is entered, exit the method.
                if (brand == "X")
                {
                    return;
                }

                float rentalPrice = VehicleTools.GetNumber("Rental Price: ");
                string vehicleType = VehicleTools.GetVehicleType();
                CarGallery.AddCar(licensePlate, brand, rentalPrice, vehicleType);
                Console.WriteLine();
                Console.WriteLine("The car has been successfully added.");
            }
            catch (Exception e)
            {
                if (e.Message == "Exit")
                {
                    return;
                }
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteCar()
        {
            Console.WriteLine("-Delete a Car-");
            Console.WriteLine();
            string licensePlate;
            try
            {
                if (CarGallery.Cars.Count == 0)
                {
                    throw new Exception("There are no cars to delete in the gallery.");
                }

                while (true)
                {
                    licensePlate = VehicleTools.GetLicensePlate("Enter the license plate of the car you want to delete: ");

                    // If "X" is entered, exit the method.
                    if (licensePlate == "X")
                    {
                        return;
                    }

                    if (CarGallery.GetStatus(licensePlate) == "CarNotFound")
                    {
                        Console.WriteLine("There is no car with this license plate in the gallery.");
                    }
                    else if (CarGallery.GetStatus(licensePlate) == "On Rent")
                    {
                        Console.WriteLine("The car cannot be deleted because it is on rent.");
                    }
                    else
                    {
                        break;
                    }
                }
                CarGallery.DeleteCar(licensePlate);
                Console.WriteLine();
                Console.WriteLine("The car has been deleted.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ShowInformation()
        {
            // We directly access the class to return the information of the cars saved in the gallery.

            Console.WriteLine("-Gallery Information-");
            Console.WriteLine("Total number of cars: " + CarGallery.TotalCarCount);
            Console.WriteLine("Number of cars on rent: " + CarGallery.CarsOnRentCount);
            Console.WriteLine("Number of cars waiting in the gallery: " + CarGallery.CarsInGalleryCount);
            Console.WriteLine("Total car rental duration: " + CarGallery.TotalCarRentalDuration);
            Console.WriteLine("Total number of rentals: " + CarGallery.TotalCarRentalCount);
            Console.WriteLine("Revenue: " + CarGallery.Revenue);
        }
    }
}
