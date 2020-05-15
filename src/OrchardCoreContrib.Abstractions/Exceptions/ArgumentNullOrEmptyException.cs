using System;

namespace OrchardCoreContrib
{
    /// <summary>
    /// The exception that is thrown when a <see langword="null" /> reference or empty value is passed to a method that does not accept it as a valid argument.
    /// </summary>
    public class ArgumentNullOrEmptyException : ArgumentNullException
    {
        private static readonly string _argumentNullOrEmptyExceptionMessage = "Value cannot be null or empty.";

        /// <summary>
        ///  Initializes a new instance of the <see cref="ArgumentNullOrEmptyException"/> class.
        /// </summary>
        public ArgumentNullOrEmptyException()
            : base(_argumentNullOrEmptyExceptionMessage, null as Exception)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNullOrEmptyException"/> class with the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        public ArgumentNullOrEmptyException(string paramName)
            : base(paramName, _argumentNullOrEmptyExceptionMessage)
        {

        }

        /// <summary>
        /// Initializes an instance of the <see cref="ArgumentNullOrEmptyException"/> class with a specified error message and the name of the parameter that causes this exception.
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        public ArgumentNullOrEmptyException(string paramName, string message)
            : base(paramName, message)
        {

        }
    }
}
