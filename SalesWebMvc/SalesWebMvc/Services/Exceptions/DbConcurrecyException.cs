using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class DbConcurrecyException : ApplicationException
    {
        public DbConcurrecyException(string message) : base(message) 
        {
        }
    }
}
