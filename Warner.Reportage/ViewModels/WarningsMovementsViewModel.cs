using System;
using System.Collections.Generic;
using System.Linq;

namespace Warner.Reportage.ViewModels
{
    public class WarningsMovementsViewModel
    {
        private readonly IDictionary<string, int> movements;

        public WarningsMovementsViewModel(
            IDictionary<string, int> movements)
        {
            this.movements = movements;
        }

        public List<KeyValuePair<string, int>> GetMovements()
        {
            return movements.OrderByDescending(p => Math.Abs(p.Value)).ToList();
        }
    }
}
