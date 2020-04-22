﻿namespace BgRallyRace.Services
{
    using BgRallyRace.Data;
    using BgRallyRace.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class RallyNavigatorsServices : IRallyNavigatorsServices
    {
        private readonly ApplicationDbContext dbContext;
        public RallyNavigatorsServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public List<RallyNavigators> GetNavigators(string user)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            int timeId = dbContext.Teams.Where(t => t.User == user).Select(t => t.Id).First();
            var navigator = dbContext.RallyNavigators.Where(x => x.TeamId == timeId).ToList();
            return navigator;
        }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public RallyNavigators? GetNavigator(int id)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            var navigator = dbContext.RallyNavigators.Where(x => x.Id == id).FirstOrDefault();
            return navigator;
        }

        public int CreateRallyNavigatorsAsync()
        {
            Random rnd = new Random();
            int first = rnd.Next(1, 100);
            int last = rnd.Next(1, 100);
            var firstName = dbContext.FirstNames.Select(x => new { x.FirstName, x.Id })
                .FirstOrDefault(x => x.Id == first);
            var lastName = dbContext.LastNames.Select(x => new { x.LastName, x.Id })
                .FirstOrDefault(x => x.Id == last);
            var rallyNavigator = dbContext.RallyNavigators.Add(new RallyNavigators
            {
                FirstName = firstName.FirstName,
                LastName = lastName.LastName,
                Age = 18,
                Salary = 610,
                Concentration = 5,
                Experience = 5,
                Energy = 100,
                Devotion = 5,
                PhysicalTraining = 5,
                Communication = 5,
                Pounds = 80,
            });

            dbContext.SaveChanges();
            var id = rallyNavigator.Entity.Id;
            return id;
        }
        public void Fired(int id)
        {
            var person = this.GetNavigator(id);
            person.TeamId = null;
            dbContext.SaveChanges();
        }
        public void IncreaseAge(int id)
        {
            var person = this.GetNavigator(id);
            person.Age = person.Age + 1;
            dbContext.SaveChanges();
        }
        public void IncreaseCommunication(int id, int variable)
        {
            var person = this.GetNavigator(id);
            person.Concentration = person.Communication + variable;
            dbContext.SaveChanges();
        }
        public void DecreaseCommunication(int id, int variable)
        {
            var person = this.GetNavigator(id);
            person.Concentration = person.Communication - variable;
            dbContext.SaveChanges();
        }
        public void IncreaseDevotion(int id, int variable)
        {
            var person = this.GetNavigator(id);
            person.Devotion = person.Devotion + variable;
            dbContext.SaveChanges();
        }
        public void DecreaseDevotion(int id, int variable)
        {
            var person = this.GetNavigator(id);
            person.Devotion = person.Devotion - variable;
            dbContext.SaveChanges();
        }
        public void IncreaseEnergy(int id)
        {
            var person = this.GetNavigator(id);
            person.Energy = 100;
            dbContext.SaveChanges();
        }
        public void DecreaseEnergy(int id, int variable)
        {
            var person = this.GetNavigator(id);
            person.Energy = person.Energy - variable;
            dbContext.SaveChanges();
        }
        public void IncreaseExperience(int id, int variable)
        {
            var person = this.GetNavigator(id);
            person.Experience = person.Experience + variable;
            dbContext.SaveChanges();
        }
        public void DecreaseExperience(int id, int variable)
        {
            var person = this.GetNavigator(id);
            person.Experience = person.Experience - variable;
            dbContext.SaveChanges();
        }
        public void IncreasePhysicalTraining(int id, int variable)
        {
            var person = this.GetNavigator(id);
            person.PhysicalTraining = person.PhysicalTraining + variable;
            dbContext.SaveChanges();
        }
        public void DecreasePhysicalTraining(int id, int variable)
        {
            var person = this.GetNavigator(id);
            person.PhysicalTraining = person.PhysicalTraining - variable;
            dbContext.SaveChanges();
        }
        public void IncreasePounds(int id, int variable)
        {
            var person = this.GetNavigator(id);
            person.Pounds = person.Pounds + variable;
            dbContext.SaveChanges();
        }
        public void DecreasePounds(int id, int variable)
        {
            var person = this.GetNavigator(id);
            person.Pounds = person.Pounds - variable;
            dbContext.SaveChanges();
        }
        public void IncreaseSalary(int id, int variable)
        {
            var person = this.GetNavigator(id);
            person.Salary = person.Salary + variable;
            dbContext.SaveChanges();
            this.IncreaseDevotion(id, 2);
        }
        public void DecreaseSalary(int id, int variable)
        {
            var person = this.GetNavigator(id);
            person.Salary = person.Salary - variable;
            dbContext.SaveChanges();
            this.DecreaseDevotion(id, 2);
        }
        public void IncreaseConcentration(int id, int variable)
        {
            var person = this.GetNavigator(id);
            person.Concentration = person.Concentration + variable;
            dbContext.SaveChanges();
        }
        public void DecreaseConcentration(int id, int variable)
        {
            var person = this.GetNavigator(id);
            person.Concentration -= variable;
            dbContext.SaveChanges();
        }
        public bool IsItBusy(int id)
        {
            var result = dbContext.RallyNavigators.Where(x => x.Id == id).Select(x => x.IsItWorking).First();
            return result;
        }
        public void IsWorking(int id)
        {
            var pilot = dbContext.RallyPilots.Where(x => x.Id == id).First();
            pilot.IsItWorking = true;
            dbContext.SaveChanges();
        }

        public async Task AllNavigatorNoWorking()
        {
            var navigator = await dbContext.RallyNavigators.Where(x => x.IsItWorking == true).ToArrayAsync();
            for (int i = 0; i < navigator.Length; i++)
            {
                navigator[i].IsItWorking = false;
            }
            await dbContext.SaveChangesAsync();
        }

        public int EnergyNavigator(int id)
        {
            var navigator = dbContext.RallyNavigators.Where(x => x.Id == id).Select(x => x.Energy).FirstOrDefault();
            return navigator;
        }

        public RallyNavigators GetNavigatorNoTracking(int id)
        {
            var navigator = dbContext.RallyNavigators.Where(x => x.Id == id).AsNoTracking().First();
            return navigator;
        }

        public void ReduceEnergy(People people, decimal variable)
        {
            if (variable < 35)
            {
                people.Concentration -= 35;
            }
            else if (variable < 40)
            {
                people.Concentration -= 26;
            }
            else if (variable < 45)
            {
                people.Concentration -= 20;
            }
            else if (variable < 50)
            {
                people.Concentration -= 16;
            }
            else if (variable < 55)
            {
                people.Concentration -= 12;
            }
            else if (variable < 60)
            {
                people.Concentration -= 10;
            }
            else if (variable < 65)
            {
                people.Concentration -= 8;
            }
            else if (variable < 70)
            {
                people.Concentration -= 6;
            }
            else if (variable < 75)
            {
                people.Concentration -= 4;
            }
            else if (variable < 80)
            {
                people.Concentration -= 2;
            }
            else if (variable < 85)
            {
                people.Concentration -= 1;
            }
        }
    }
}

