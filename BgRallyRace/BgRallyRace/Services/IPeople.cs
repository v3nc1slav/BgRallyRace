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
    Task Fired(int id);
    Task IncreaseAge(int id);
    Task IncreaseConcentration(int id, int variable);
    Task DecreaseConcentration(int id, int variable);
    Task IncreaseDevotion(int id, int variable);
    Task DecreaseDevotion(int id, int variable);
    Task IncreaseEnergy(int id, int variable);
    Task DecreaseEnergy(int id, int variable);
    Task IncreaseExperience(int id, int variable);
    Task DecreaseExperience(int id, int variable);
    Task IncreasePhysicalTraining(int id, int variable);
    Task DecreasePhysicalTraining(int id, int variable);
    Task IncreasePounds(int id, int variable);
    Task DecreasePounds(int id, int variable);
    Task IncreaseSalary(int id, int variable);
    Task DecreaseSalary(int id, int variable);
    }
}
