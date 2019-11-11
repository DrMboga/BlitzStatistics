using BlitzStatistics.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzStatistics.Messages
{
	public class ApplicationErrorMessage : IApplicationMessage
	{
		public ApplicationErrorMessage(string message, Exception exception)
		{
			Message = message;
			Exception = exception;
			ErrorMessage = exception == null
				? message
				: $"{message}. Exception: '{typeof(Exception)}' - {exception.Message}";
		}

		public string ErrorMessage { get; }
		public string Message { get; }
		public Exception Exception { get; }
	}
}
