using System;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// An exception that is thrown when reading an InputVariable.
    /// </summary>
    public class InputVariableException
        : MultiLineException
    {
        public readonly InputVariable Variable;

        //---------------------------------------------------------------------

        public InputVariableException(InputVariable var,
                                      string        message)
            : base(message)
        {
            Variable = var;
        }

        //---------------------------------------------------------------------

        public InputVariableException(InputVariable   var,
                                      string          message,
                                      params object[] args)
            : base(string.Format(message, args))
        {
            Variable = var;
        }

        //---------------------------------------------------------------------

        public InputVariableException(InputVariable var,
                                      string        message,
                                      Exception     exception)
            : base(message, exception)
        {
            Variable = var;
        }
    }
}
