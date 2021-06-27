using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Web.Exceptions
{
    [Serializable]
    public class InvalidProductIdException : Exception
    {
        public InvalidProductIdException()
        {

        }
        public InvalidProductIdException(int id) : base($"Invalid Product Id: {id}")
        {

        }
        public InvalidProductIdException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
