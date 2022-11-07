using RDSync.Core;



Console.WriteLine("Testing RDSync.Core");

// First test: create new EndPoint on C:\ and check if there are directories
Console.WriteLine("Test 1: Create an EndPoint on C:\\ and check if there are directories");
try
{
    var endPoint1 = new DriveEndPoint("C:\\", null);
    if (endPoint1.GetDirectories().Count() == 0)
        throw new Exception("C:\\ should have more directories, shouln't it?");
    Console.WriteLine("Pass");
} catch (Exception e)
{
    Console.WriteLine("This exception was not expected: {0}", e);
}
Console.WriteLine();

// Second test: create random EndPoint, try to access it, should throw "DeviceNotReadyException"
Console.WriteLine("Test 2: Create a random endpoint on c:\\xyz\\ -> should throw DeviceNotReadyException");
try
{
    var endPoint1 = new DriveEndPoint("C:\\cdssqcds", null);
    if (endPoint1.GetDirectories().Count() >= 0)
        throw new Exception("C:\\cdssqcds should not have any directory");
    Console.WriteLine("DeviceNotReadyException expected.");
}
catch (DeviceNotReadyException e)
{
    Console.WriteLine("Pass");
}
Console.WriteLine();

// Third test: create E:\ EndPoint
Console.WriteLine("Test 3: List endpoints, takes first");
try
{
    var endPoints = Helper.GetEndPoints().ToList();

    if (endPoints.Count == 0)
    {
        Console.WriteLine("UNCONCLUSIVE");
    } else
    {

        var endPoint = endPoints.First();
        if (endPoint.GetDirectories().Count() == 0)
            throw new Exception("E:\\ should not have some directory");
    }
    Console.WriteLine("Pass");
}
catch (Exception e)
{
    Console.WriteLine("This exception was not expected: {0}", e);
}
Console.WriteLine();


// Fourth test: copy E:\ to Temp directory
Console.WriteLine("Test 4");
void Syncer_FileCopied(object? sender, Syncer.RuleEventArgs e)
{
    Console.WriteLine($" -> FileCopied >> {e.Rule.DevicePath} {e.File?.Name}");
}
void Syncer_FileDetected(object? sender, Syncer.RuleEventArgs e)
{
    Console.WriteLine($" -> FileDetected >> {e.Rule.DevicePath} {e.File?.Name} -> {e.File?.DestinationPath}");
}

try
{
    var dir = Path.Combine(Path.GetTempPath(), "RDSyncTest");

    var syncer = new Syncer() { EndPointIdentifier = "Directory:D:\\" };
    syncer.Rules.Add(new Rule() { DevicePath = dir });
    syncer.FileCopied += Syncer_FileCopied;
    syncer.FileDetected += Syncer_FileDetected;
    syncer.Execute();

    Console.WriteLine("Pass");
}
catch (Exception e)
{
    Console.WriteLine("This exception was not expected: {0}", e);
}

Console.WriteLine();


Console.ReadLine();