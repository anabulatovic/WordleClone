using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleClone.Services
{
    interface ISolutionGeneratorService
    {
        public Task<string> GenerateSolution();
    }
}
