using System.Diagnostics;

namespace Edu.Wisc.Forest.Flel.Util
{
    /// <summary>
    /// An exception with an multiline text message.
    /// </summary>
    public class MultiLineException
        : System.ApplicationException
    {
        public const string DefaultIndent = "  ";

        private static string indent = DefaultIndent;

        //---------------------------------------------------------------------

        /// <summary>
        /// The string to indent each line in the inner portion of an
        /// exception's message.
        /// </summary>
        public static string Indent
        {
            get {
                return indent;
            }

            set {
                Debug.Assert( value != null );
                indent = value;
            }
        }

        //---------------------------------------------------------------------

        private MultiLineText multiLineMessage;

        //---------------------------------------------------------------------

        /// <summary>
        /// The exception's message with its separate lines.
        /// </summary>
        public MultiLineText MultiLineMessage
        {
            get {
                return multiLineMessage;
            }
        }

        //---------------------------------------------------------------------

        public override string Message
        {
            get {
                return multiLineMessage.ToString();
            }
        }

        //---------------------------------------------------------------------

        public MultiLineException(MultiLineText message)
            : base(ConvertToString(message))
        {
            SetMultiLineMessage(message, null);
        }

        //---------------------------------------------------------------------

        public MultiLineException(MultiLineText message,
                                  MultiLineText innerMessage)
            : base(ConvertToString(message))
        {
            SetMultiLineMessage(message, innerMessage);
        }

        //---------------------------------------------------------------------

        public MultiLineException(MultiLineText    message,
                                  System.Exception innerException)
            : base(ConvertToString(message), innerException)
        {
            if (innerException == null)
                SetMultiLineMessage(message, null);
            else {
                MultiLineException inner = innerException as MultiLineException;
                if (inner != null)
                    SetMultiLineMessage(message, inner.MultiLineMessage);
                else
                    SetMultiLineMessage(message, innerException.Message);
            }
        }

        //---------------------------------------------------------------------

        private static string ConvertToString(MultiLineText message)
        {
            if (message == null)
                throw new System.ArgumentNullException();
            return message.ToString();
        }

        //---------------------------------------------------------------------

        private void SetMultiLineMessage(MultiLineText message,
                                         MultiLineText innerMessage)
        {
            if (message.Count == 1 && innerMessage != null)
                multiLineMessage = new MultiLineText(message.ToString() + ":");
            else
                multiLineMessage = new MultiLineText(message);
            if (innerMessage != null) {
                foreach (string line in innerMessage)
                    multiLineMessage.Add(Indent + line);
            }
        }
    }
}
