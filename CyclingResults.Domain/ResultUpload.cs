using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyclingResults.Domain
{
    public class ResultUpload
    {
        public int Id { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// The URL for the image that is uploaded.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The identifier for the result be retrieved.
        /// </summary>
        public Guid? ResultId { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public int RaceId { get; set; }

        /// <summary>
        /// Which item it was uploaded for.
        /// </summary>
        public virtual Race Race { get; set; }
        
    }
}
