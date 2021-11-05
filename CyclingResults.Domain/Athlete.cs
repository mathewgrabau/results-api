using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyclingResults.Domain
{
    /// <summary>
    /// The base object for an athlete record.
    /// </summary>
    public class Athlete
    {
        public int Id { get; set; }

        /// <summary>
        /// The first name of the development that 
        /// </summary>
        public string FirstName { get; set; }

        public string LastName { get; set; }

        //public virtual ICollection<RaceResults> Results { get; set; }

        public virtual ICollection<Result> Results { get; set; }

    }
}
