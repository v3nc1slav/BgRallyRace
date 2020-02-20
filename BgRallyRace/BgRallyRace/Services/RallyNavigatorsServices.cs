﻿using BgRallyRace.Data;
using BgRallyRace.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgRallyRace.Services
{
    public class RallyNavigatorsServices : IRallyNavigatorsServices
    {
        private readonly ApplicationDbContext dbContext;
        public RallyNavigatorsServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public ICollection<RallyNavigators>? GetNavigators(string user)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            var navigator = dbContext.Teams.Where(t => t.User == user).Select(x => x.RallyNavigator).ToList();
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
            //ToDo
            int first = rnd.Next(1, 100);
            int last = rnd.Next(1, 100);
            var  firstName =  dbContext.FirstNames.Select(x=>new { x.FirstName, x.Id })
                .FirstOrDefault(x => x.Id == first);
            var lastName =  dbContext.LastNames.Select(x => new { x.LastName, x.Id })
                .FirstOrDefault(x => x.Id == last);
           var rallyNavigator =   dbContext.RallyNavigators.Add(new RallyNavigators
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
            throw new NotImplementedException();
        }

        public void IncreaseCommunication(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public void DecreaseCommunication(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public RallyNavigators GetNavigators(int id)
        {
            throw new NotImplementedException();
        }

        public void IncreaseAge(int id)
        {
            throw new NotImplementedException();
        }

        public void IncreaseConcentration(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public void DecreaseConcentration(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public void IncreaseDevotion(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public void DecreaseDevotion(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public void IncreaseEnergy(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public void DecreaseEnergy(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public void IncreaseExperience(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public void DecreaseExperience(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public void IncreasePhysicalTraining(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public void DecreasePhysicalTraining(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public void IncreasePounds(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public void DecreasePounds(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public void IncreaseSalary(int id, int variable)
        {
            throw new NotImplementedException();
        }

        public void DecreaseSalary(int id, int variable)
        {
            throw new NotImplementedException();
        }

   }
}

