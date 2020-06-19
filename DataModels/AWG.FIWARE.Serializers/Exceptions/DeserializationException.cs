using System;

namespace AWG.FIWARE.Serializers.Exceptions
{
  public class DeserializationException : Exception
  {
    public DeserializationException()
    {
    }

    public DeserializationException(string message) : base(message)
    {
    }

    public DeserializationException(string message, Exception inner) : base(message, inner)
    {
    }
  }
}