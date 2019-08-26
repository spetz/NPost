using System;

namespace NPost.Modules.Deliveries.Core.Exceptions
{
    internal abstract class ExceptionBase : Exception
    {
        public abstract string Code { get; }

        protected ExceptionBase(string message) : base(message)
        {
        }
    }
}