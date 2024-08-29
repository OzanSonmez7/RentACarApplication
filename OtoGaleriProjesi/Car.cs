using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGalleryProject
{
    class Car
    {
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public float RentalPrice { get; set; }
        public string VehicleType { get; set; }
        public string Status { get; set; }

        public List<int> RentalDurations = new List<int>();
        public int RentalCount
        {
            get
            {
                return this.RentalDurations.Count;
            }
        }

        public int TotalRentalDuration
        {
            get
            {
                return this.RentalDurations.Sum();
            }
        }

        public Car(string licensePlate, string brand, float rentalPrice, string vehicleType)
        {
            // In the Car method, we create a parameterized constructor to add the data received from the parameters to the information of the car in the list.

            this.LicensePlate = licensePlate.ToUpper();
            this.Brand = brand.ToUpper();
            this.RentalPrice = rentalPrice;
            this.VehicleType = vehicleType;
            this.Status = "In Gallery";
        }
    }
}
