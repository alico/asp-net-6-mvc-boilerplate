using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BoilerPlate.Utils
{
    public class CustomException : Exception
    {
        public List<string> Messages { get; set; }

        public CustomException(string message) : base(message)
        {

        }

        public CustomException(Exception exception) : base(exception.Message, exception)
        {

        }

        public CustomException(string message, Exception exception) : base(message, exception)
        {

        }

        public CustomException(IEnumerable<string> message) : base(string.Join(",", message.Distinct()))
        {
            Messages = message.ToList();
        }

        public CustomException(IEnumerable<string> message, Exception exception) : base(string.Join("#", message.Distinct()), exception)
        {

        }

        public override string ToString()
        {
            if (InnerException == null)
            {
                return base.ToString();
            }

            return string.Format(CultureInfo.InvariantCulture, "{0} [See nested exception: {1}]", base.ToString(), InnerException);
        }
    }
}
