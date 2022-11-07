namespace RDSync.Core;

public class DeviceNotReadyException: Exception
{
    public DeviceNotReadyException() : base()
    { }

    public DeviceNotReadyException(Exception e): base(e.Message, e)
    { }
}