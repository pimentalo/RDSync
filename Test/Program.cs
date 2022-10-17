using RDSync.Core;



Console.WriteLine("Testing RDSync.Core");

// First test: create new EndPoint on C:\ and check if there are directories
try {
    var endPoint1 = new DriveEndPoint("C:\\");
    if (endPoint1.GetDirectories().Count() == 0)
        throw new Exception("C:\\ should have more directories, shouln't it?");
} catch (Exception e)
{
    throw new Exception("This exception was not expected: ", e);
}

// Second test: create random EndPoint, try to access it, should throw "DeviceNotAvailableException"
try {
    var endPoint1 = new DriveEndPoint("C:\\cdssqcds");
    if (endPoint1.GetDirectories().Count() >= 0)
        throw new Exception("C:\\cdssqcds should not have any directory");
} catch (DeviceNotReadyException e)
{
    throw new Exception("This exception was not expected: ", e);
}

var syncer = new Syncer();