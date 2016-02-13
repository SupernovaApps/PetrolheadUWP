using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrolhead.Models
{
    public class DataModelUpdateFailureException : Exception
    {
        public DataModelUpdateFailureException() : base("An error occurred while updating your data.")
        {

        }

        public DataModelUpdateFailureException(string message) : base(message)
        {

        }
    }
}
