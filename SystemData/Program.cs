var exit = false;
while (exit == false)
{
    Console.Clear();
    Console.WriteLine("Seleccione una opcion:\n\n" +
        "1. Discos duros (Factory info)\n" +
        "2. Discos duros\n" +
        "3. Tarjeta de video\n" +
        "¿Cerrar Programa? S/N");

    var rpta = Console.ReadLine().ToString().ToUpper();
    switch (rpta)
    {
        case "1":
            HardDisksFactory();
            Wait();
            break;
        case "2":
            HardDisks();
            Wait();
            break;
        case "3":
            Video();
            Wait();
            break;
        default:
            exit = rpta == "S" ? true : false;
            break;

    }
}

static void Wait()
{
    Console.WriteLine("Oprima cualquier tecla para volver atras");
    Console.ReadLine();
}

static void HardDisksFactory()
{

    var driveQuery = new ManagementObjectSearcher("select * from Win32_DiskDrive");
    foreach (ManagementObject d in driveQuery.Get())
    {
        var deviceId = d.Properties["DeviceId"].Value;
        //Console.WriteLine("Device");
        //Console.WriteLine(d);
        var partitionQueryText = string.Format("associators of {{{0}}} where AssocClass = Win32_DiskDriveToDiskPartition", d.Path.RelativePath);
        var partitionQuery = new ManagementObjectSearcher(partitionQueryText);
        foreach (ManagementObject p in partitionQuery.Get())
        {
            //Console.WriteLine("Partition");
            //Console.WriteLine(p);
            var logicalDriveQueryText = string.Format("associators of {{{0}}} where AssocClass = Win32_LogicalDiskToPartition", p.Path.RelativePath);
            var logicalDriveQuery = new ManagementObjectSearcher(logicalDriveQueryText);
            foreach (ManagementObject ld in logicalDriveQuery.Get())
            {
                //Console.WriteLine("Logical drive");
                //Console.WriteLine(ld);

                var physicalName = Convert.ToString(d.Properties["Name"].Value); // \\.\PHYSICALDRIVE2
                var diskName = Convert.ToString(d.Properties["Caption"].Value); // WDC WD5001AALS-xxxxxx
                var diskModel = Convert.ToString(d.Properties["Model"].Value); // WDC WD5001AALS-xxxxxx
                var diskInterface = Convert.ToString(d.Properties["InterfaceType"].Value); // IDE
                var capabilities = (UInt16[])d.Properties["Capabilities"].Value; // 3,4 - random access, supports writing
                var mediaLoaded = Convert.ToBoolean(d.Properties["MediaLoaded"].Value); // bool
                var mediaType = Convert.ToString(d.Properties["MediaType"].Value); // Fixed hard disk media
                var mediaSignature = Convert.ToUInt32(d.Properties["Signature"].Value); // int32
                var mediaStatus = Convert.ToString(d.Properties["Status"].Value); // OK

                var driveName = Convert.ToString(ld.Properties["Name"].Value); // C:
                var driveId = Convert.ToString(ld.Properties["DeviceId"].Value); // C:
                var driveCompressed = Convert.ToBoolean(ld.Properties["Compressed"].Value);
                var driveType = Convert.ToUInt32(ld.Properties["DriveType"].Value); // C: - 3
                var fileSystem = Convert.ToString(ld.Properties["FileSystem"].Value); // NTFS
                var freeSpace = Convert.ToUInt64(ld.Properties["FreeSpace"].Value); // in bytes
                var totalSpace = Convert.ToUInt64(ld.Properties["Size"].Value); // in bytes
                var driveMediaType = Convert.ToUInt32(ld.Properties["MediaType"].Value); // c: 12
                var volumeName = Convert.ToString(ld.Properties["VolumeName"].Value); // System
                var volumeSerial = Convert.ToString(ld.Properties["VolumeSerialNumber"].Value); // 12345678

                Console.WriteLine("PhysicalName: {0}", physicalName);
                Console.WriteLine("DiskName: {0}", diskName);
                Console.WriteLine("DiskModel: {0}", diskModel);
                Console.WriteLine("DiskInterface: {0}", diskInterface);
                // Console.WriteLine("Capabilities: {0}", capabilities);
                Console.WriteLine("MediaLoaded: {0}", mediaLoaded);
                Console.WriteLine("MediaType: {0}", mediaType);
                Console.WriteLine("MediaSignature: {0}", mediaSignature);
                Console.WriteLine("MediaStatus: {0}", mediaStatus);

                Console.WriteLine("DriveName: {0}", driveName);
                Console.WriteLine("DriveId: {0}", driveId);
                Console.WriteLine("DriveCompressed: {0}", driveCompressed);
                Console.WriteLine("DriveType: {0}", driveType);
                Console.WriteLine("FileSystem: {0}", fileSystem);
                Console.WriteLine("FreeSpace: {0}", freeSpace);
                Console.WriteLine("TotalSpace: {0}", totalSpace);
                Console.WriteLine("DriveMediaType: {0}", driveMediaType);
                Console.WriteLine("VolumeName: {0}", volumeName);
                Console.WriteLine("VolumeSerial: {0}", volumeSerial);

                Console.WriteLine(new string('-', 79));
            }
        }
    }
}

static void HardDisks()
{
    double KbToGbFactor = 1d / 1024 / 1024;

    DriveInfo[] allDrives = DriveInfo.GetDrives();

    foreach (DriveInfo d in allDrives)
    {
        Console.WriteLine("Drive {0}", d.Name);
        Console.WriteLine("  Drive type: {0}", d.DriveType);
        if (d.IsReady == true)
        {
            Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
            Console.WriteLine("  File system: {0}", d.DriveFormat);
            Console.WriteLine("  Available space to current user:{0, 15} bytes", d.AvailableFreeSpace);
            Console.WriteLine("  Total available space:          {0, 15} bytes", d.TotalFreeSpace);
            Console.WriteLine("  Total size of drive:            {0, 15} bytes ", d.TotalSize);
            Console.WriteLine("  Total size of drive:            {0} GB ", d.TotalSize * KbToGbFactor);
        }
    }
}

static void Video()
{
    var myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");

    foreach (ManagementObject obj in myVideoObject.Get())
    {
        Console.WriteLine("Name  -  " + obj["Name"]);
        Console.WriteLine("Status  -  " + obj["Status"]);
        Console.WriteLine("Caption  -  " + obj["Caption"]);
        Console.WriteLine("DeviceID  -  " + obj["DeviceID"]);
        Console.WriteLine("AdapterRAM  -  " + obj["AdapterRAM"]);
        Console.WriteLine("AdapterDACType  -  " + obj["AdapterDACType"]);
        Console.WriteLine("Monochrome  -  " + obj["Monochrome"]);
        Console.WriteLine("InstalledDisplayDrivers  -  " + obj["InstalledDisplayDrivers"]);
        Console.WriteLine("DriverVersion  -  " + obj["DriverVersion"]);
        Console.WriteLine("VideoProcessor  -  " + obj["VideoProcessor"]);
        Console.WriteLine("VideoArchitecture  -  " + obj["VideoArchitecture"]);
        Console.WriteLine("VideoMemoryType  -  " + obj["VideoMemoryType"]);
    }
}