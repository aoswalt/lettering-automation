using System;

namespace Lettering.Errors {
    internal class Error {
        internal ErrorType Type;
        internal string Message;

        internal Error(ErrorType type, string message) {
            this.Type = type;
            this.Message = message;
        }
    }
}
