using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGalleryProject
{
    class Gallery
    {
        public List<Car> Cars = new List<Car>();

        public Gallery()
        {
            AddFakeData();
        }

        public int CarsInGalleryCount
        {
            get
            {
                return this.Cars.Where(c => c.Status == "In Gallery").ToList().Count;
            }
        }

        public int CarsOnRentCount
        {
            get
            {
                return this.Cars.Where(c => c.Status == "On Rent").ToList().Count;
            }
        }

        public int TotalCarCount
        {
            get
            {
                return this.Cars.Count; // Using Count, we find the total number of cars in the gallery.
            }
        }

        public int TotalCarRentalDuration
        {
            get
            {
                return this.Cars.Sum(c => c.RentalDurations.Sum()); // The sum of the total rental duration of all cars in the Cars list.
            }
        }

        public int TotalCarRentalCount
        {
            get
            {
                return this.Cars.Sum(c => c.RentalCount); // Using Sum, we find the total number of rentals for all rented cars.
            }
        }

        public float Revenue
        {
            get
            {
                return this.Cars.Sum(c => c.TotalRentalDuration * c.RentalPrice); // Using Sum, we find the total revenue from rented cars.
            }
        }

        public void AddCar(string licensePlate, string brand, float rentalPrice, string vehicleType)
        {
            // We create a new car list with the information received from the parameters and send this information to the Car class to save the data.
            // Using the Add method, we save the car we added to the gallery.

            Car c = new Car(licensePlate, brand, rentalPrice, vehicleType);
            this.Cars.Add(c);
        }

        public void AddFakeData()
        {
            // Using the AddCar method we created, we manually enter information to create fake data.

            AddCar("34ARB3434", "FIAT", 70, "Sedan");
            AddCar("35ARB3535", "KIA", 60, "SUV");
            AddCar("34US2342", "OPEL", 50, "Hatchback");
        }

        public string GetStatus(string licensePlate)
        {
            // We find the car we are looking for with the license plate information received from the parameter.
            // The FirstOrDefault method takes the first value in the list we call.
            // If there is such a car, it returns the current status of the found car; if the car does not exist, it returns "CarNotFound".

            Car c = this.Cars.Where(c => c.LicensePlate == licensePlate.ToUpper()).FirstOrDefault();
            if (c != null)
            {
                return c.Status;
            }
            return "CarNotFound";
        }

        public void RentCar(string licensePlate, int duration)
        {
            // We find the car we are looking for from the car list with the license plate information received from the parameter.
            // The FirstOrDefault method takes the first value in the list we call.
            // If such a car exists and its status is "In Gallery," we update its status to "On Rent" and add the rental duration.

            Car c = this.Cars.Where(c => c.LicensePlate == licensePlate.ToUpper()).FirstOrDefault();
            if (c != null && c.Status == "In Gallery")
            {
                c.Status = "On Rent";
                c.RentalDurations.Add(duration);
            }
        }

        public List<Car> GetCarList(string status)
        {
            // We perform listing based on the car status in the gallery with the data type received as a parameter.

            List<Car> list = this.Cars;
            if (status == "On Rent" || status == "In Gallery")
            {
                list = this.Cars.Where(c => c.Status == status).ToList();
            }
            return list;
        }

        public void ReceiveCar(string licensePlate)
        {
            // We find the car we are looking for with the license plate information received from the parameter.
            // The FirstOrDefault method takes the first value in the list we call.
            // If there is such a car, we update its status to "In Gallery" as we will be receiving the car.

            Car c = this.Cars.Where(c => c.LicensePlate == licensePlate.ToUpper()).FirstOrDefault();

            if (c != null)
            {
                if (c.Status == "In Gallery")
                {
                    throw new Exception("Already in gallery");
                }

                c.Status = "In Gallery";
            }
            else
            {
                throw new Exception("No car with this license plate.");
            }
        }

        public void CancelRental(string licensePlate)
        {
            // We find the car we are looking for with the license plate information received from the parameter.
            // The FirstOrDefault method takes the first value in the list we call.
            // If such a car exists, we update its status to "In Gallery" and remove the rental duration as we will cancel the rental.

            Car c = this.Cars.Where(c => c.LicensePlate == licensePlate.ToUpper()).FirstOrDefault();

            if (c != null)
            {
                c.Status = "In Gallery";
                c.RentalDurations.RemoveAt(c.RentalDurations.Count - 1);
            }
        }

        public void DeleteCar(string licensePlate)
        {
            // We find the car we are looking for with the license plate information received from the parameter.
            // The FirstOrDefault method takes the first value in the list we call.
            // Using the remove method, if such a car exists and is in the gallery, we ensure that the car is deleted from the list.

            Car c = this.Cars.Where(x => x.LicensePlate == licensePlate.ToUpper()).FirstOrDefault();

            if (c != null && c.Status == "In Gallery")
            {
                this.Cars.Remove(c);
            }
        }
    }
}
