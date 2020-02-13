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
        async Task Fired(int id);
        async Task IncreaseAge(int id);
        async Task IncreaseConcentration(int id, int variable);
        async Task DecreaseConcentration(int id, int variable);
        async Task IncreaseDevotion(int id, int variable);
        async Task DecreaseDevotion(int id, int variable);
        async Task IncreaseEnergy(int id, int variable);
        async Task DecreaseEnergy(int id, int variable);
        async Task IncreaseExperience(int id, int variable);
        async Task DecreaseExperience(int id, int variable);
        async Task IncreasePhysicalTraining(int id, int variable);
        async Task DecreasePhysicalTraining(int id, int variable);
        async Task IncreasePounds(int id, int variable);
        async Task DecreasePounds(int id, int variable);
        async Task IncreaseSalary(int id, int variable);
        async Task DecreaseSalary(int id, int variable);
    }
}
