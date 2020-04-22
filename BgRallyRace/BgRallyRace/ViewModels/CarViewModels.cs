namespace BgRallyRace.ViewModels
{
    using BgRallyRace.Models;
    using System;

    public class CarViewModels
    {
         const int percentage =  100;
         const int price =  15;

        public int CarId { get; set; }

        public Cars Car { get; set; }

        public Cars Cars { get; set; }

        public Aerodynamics Aerodynamics { get; set; }

        public Brakes Brakes { get; set; }

        public Engines Engines { get; set; }

        public Gearboxs Gearboxs { get; set; }

        public ModelsCars ModelsCars { get; set; }

        public Mountings Mountings { get; set; }

        public Turbo? Turbo { get; set; }

        public decimal MaxSpeed { get; set; }

        public decimal CurrentSpeedAerodynamics
        {
            get
            {
                return this.Aerodynamics.Speed * this.Aerodynamics.Strength/percentage;
            }
        }

        public decimal RepairPriceAerodynamics
        {
            get
            {
                return (100- this.Aerodynamics.Strength)*price;
            }
        }

        public decimal CurrentSpeedBrakes
        {
            get
            {
                return this.Brakes.Speed * this.Brakes.Strength/percentage;
            }
        }

        public decimal RepairPriceBrakes
        {
            get
            {
                return (100 - this.Brakes.Strength) * price;
            }
        }

        public decimal CurrentSpeedEngines
        {
            get
            {
                return this.Engines.Speed * this.Engines.Strength/percentage;
            }
        }

        public decimal RepairPriceEngines
        {
            get
            {
                return (100 - this.Engines.Strength) * price;
            }
        }

        public decimal CurrentSpeedGearboxs
        {
            get
            {
                return this.Gearboxs.Speed * this.Gearboxs.Strength/ percentage;
            }
        }

        public decimal RepairPriceGearboxs
        {
            get
            {
                return (100 - this.Gearboxs.Strength) * price;
            }
        }

        public decimal CurrentSpeedModelsCars
        {
            get
            {
                return this.ModelsCars.Speed * this.ModelsCars.Strength/ percentage;
            }
        }

        public decimal RepairPriceModelsCars
        {
            get
            {
                return (100 - this.ModelsCars.Strength) * price;
            }
        }

        public decimal CurrentSpeedMountings
        {
            get
            {
                return this.Mountings.Speed * this.Mountings.Strength/ percentage;
            }
        }

        public decimal RepairPriceMountings
        {
            get
            {
                return (100 - this.Mountings.Strength) * price;
            }
        }

        public decimal CurrentSpeedTurbo
        {
            get
            {
                return this.Turbo.Speed * this.Turbo.Strength / percentage;
            }
        }

        public decimal RepairPriceTurbo
        {
            get
            {
                return (100 - this.Turbo.Strength) * price;
            }
        }

        public string Text { get; set; }

    }
}
