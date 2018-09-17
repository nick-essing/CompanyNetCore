using System;

namespace CompanyNetCore.Helper
{
    [Serializable]
    public class RepoException<T> : Exception
    {
        public T Type { get; set; }
        public RepoException(T type)
        {
            Type = type;
        }
        public RepoException(string message, T type) : base(message)
        {
            Type = type;
        }
        public RepoException(string message, Exception inner) : base(message, inner) { }
        protected RepoException(          
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public enum ResultType
    {
        SQLERROR,
        INVALIDEARGUMENT,
    }
}
