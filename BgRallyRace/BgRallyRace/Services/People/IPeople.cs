using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BgRallyRace.Data;
using BgRallyRace.Models;
using Microsoft.EntityFrameworkCore;

namespace BgRallyRace.Services
{
    public interface IPeople
    {
        void Fired(int id);
        void IncreaseAge(int id);
        void IncreaseConcentration(int id, int variable);
        void DecreaseConcentration(int id, int variable);
        void IncreaseDevotion(int id, int variable);
        void DecreaseDevotion(int id, int variable);
        void IncreaseEnergy(int id);
        void DecreaseEnergy(int id, int variable);
        void IncreaseExperience(int id, int variable);
        void DecreaseExperience(int id, int variable);
        void IncreasePhysicalTraining(int id, int variable);
        void DecreasePhysicalTraining(int id, int variable);
        void IncreasePounds(int id, int variable);
        void DecreasePounds(int id, int variable);
        void IncreaseSalary(int id, int variable);
        void DecreaseSalary(int id, int variable);
        bool IsItBusy(int id);
        void IsWorking(int id);
        void ReduceEnergy(People people, decimal variable);
    }
}
