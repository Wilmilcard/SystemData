
var exit = false;
while (exit == false)
{
    Menu();
    var rpta = Console.ReadLine().ToString().ToUpper();
    Console.Clear();
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
        case "4":
            Processor();
            Wait();
            break;
        case "5":
            SO();
            Wait();
            break;
        case "6":
            Network();
            Wait();
            break;
        case "7":
            Sound();
            Wait();
            break;
        case "8":
            Printer();
            Wait();
            break;
        //case "9":
        //    game();
        //    break;
        case "?":
            Info();
            Wait();
            break;
        default:
            exit = rpta == "S" ? true : false;
            break;

    }
}

//Programana Main
static void Menu()
{
    Console.Title = "System Data";
    //Console.WindowHeight = 35;
    //Console.WindowWidth = 120;
    string title = @"
                     *                                                    *
      *                          *               *                               *
     ____     *               __                          ____        *     __               
    /\  _`\            *     /\ \__      *               /\  _`\           /\ \__            *
    \ \,\L\_\  __  __    ____\ \ ,_\    __    ___ ___    \ \ \/\ \     __  \ \ ,_\    __     
     \/_\__ \ /\ \/\ \  /',__\\ \ \/  /'__`\/' __` __`\   \ \ \ \ \  /'__`\ \ \ \/  /'__`\   
       /\ \L\ \ \ \_\ \/\__, `\\ \ \_/\  __//\ \/\ \/\ \   \ \ \_\ \/\ \L\.\_\ \ \_/\ \L\.\_ 
       \ `\____\/`____ \/\____/ \ \__\ \____\ \_\ \_\ \_\   \ \____/\ \__/.\_\\ \__\ \__/.\_\
        \/_____/`/___/> \/___/   \/__/\/____/\/_/\/_/\/_/    \/___/  \/__/\/_/ \/__/\/__/\/_/
       *           /\___/                                 *                                   *   
   *               \/__/                                               *                     
                *                        *                                       *              ";

    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine(title);
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.WriteLine("                                        [Edicion de consola 1.0.0]");
    Console.WriteLine("                                             por Wilmilcard\n");
    Console.WriteLine("                                    para ayuda o informacion escribe ?\n\n");
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("SELECCIONE UNA OPCION:\n");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.WriteLine(
        "1. Discos duros (Factory info)\n" +
        "2. Discos duros (Resume info)\n" +
        "3. Tarjeta de video\n" +
        "4. Procesador\n" +
        "5. Sistema Operativo\n" +
        "6. Redes\n" +
        "7. Sonido\n" +
        "8. Impresoras\n");
    Console.CursorSize = 60;
    Console.WriteLine("¿Cerrar Programa? S/N");
}

static void Wait()
{
    Console.BackgroundColor = ConsoleColor.DarkCyan;
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine(@"
 __v_                                                                 
(____\/{  Oprima cualquier tecla para volver atras                    ");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.BackgroundColor = ConsoleColor.Black;

    Console.ReadLine();
    Console.Clear();
}

static void Info()
{
    var text = $@"
 ______________________________________________________________________
|[] information.exe                                             |X]|! -|
| -------------------------------------------------------------------|-|
| Autor: Wilmilcard                                                  | |
| Respositorio: https://github.com/Wilmilcard/SystemData             | |
| Version: 1.0.0                                                     | |
| Desarrollado en .net 6 - C#                                        | |
|                                                                    |_|
| ___________________________________________________________________|/|
";
    Console.WriteLine(text);
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("<3");
    Console.ForegroundColor = ConsoleColor.Gray;
}

//Opciones de menu
static void HardDisksFactory()
{
    var utils = new Utils();
    var count = utils.RandomNumber();
    var KbToGbFactor = 1d / 1024 / 1024;
    var driveQuery = new ManagementObjectSearcher("select * from Win32_DiskDrive");

    foreach (ManagementObject d in driveQuery.Get())
    {
        var deviceId = d.Properties["DeviceId"].Value;
        var partitionQueryText = string.Format("associators of {{{0}}} where AssocClass = Win32_DiskDriveToDiskPartition", d.Path.RelativePath);
        var partitionQuery = new ManagementObjectSearcher(partitionQueryText);
        
        foreach (ManagementObject p in partitionQuery.Get())
        {
            //Console.WriteLine("Partition");
            var logicalDriveQueryText = string.Format("associators of {{{0}}} where AssocClass = Win32_LogicalDiskToPartition", p.Path.RelativePath);
            var logicalDriveQuery = new ManagementObjectSearcher(logicalDriveQueryText);
            foreach (ManagementObject ld in logicalDriveQuery.Get())
            {
                Console.WriteLine(@"Drive {0}\", ld.Properties["Name"].Value);

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

                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   PhysicalName: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(physicalName);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   PhysicalName: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(physicalName);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   DiskName: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(diskName);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   DiskModel: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(diskModel);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   DiskInterface: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(diskInterface);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   Capabilities: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(capabilities);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   MediaLoaded: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(mediaLoaded);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   MediaType: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(mediaType);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   MediaSignature: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(mediaSignature);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   MediaStatus: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(mediaStatus);

                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   DriveName: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(driveName);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   DriveId: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(driveId);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   DriveCompressed: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(driveCompressed);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   DriveType: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(driveType);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   FileSystem: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(fileSystem);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   FreeSpace: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("{0} bytes", freeSpace);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   TotalSpace: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("{0} bytes", totalSpace);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   FreeSpace: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("{0} GB", freeSpace * KbToGbFactor);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   TotalSpace: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("{0} GB", totalSpace * KbToGbFactor);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   DriveMediaType: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(driveMediaType);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   VolumeName: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(volumeName);
                Console.ForegroundColor = utils.ColorSelect(count);
                Console.Write("   VolumeSerial: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(volumeSerial);

                Console.WriteLine(new string('-', 79));
            }
            if (count <= 11)
                count++;
            else
                count = 0;
        }
    }
}

static void HardDisks()
{
    var utils = new Utils();
    var KbToGbFactor = 1d / 1024 / 1024;
    DriveInfo[] allDrives = DriveInfo.GetDrives();
    var count = utils.RandomNumber();

    foreach (DriveInfo d in allDrives)
    {
        Console.WriteLine("Drive {0}", d.Name);
        if (d.IsReady == true)
        {
            Console.ForegroundColor = utils.ColorSelect(count);
            Console.Write("  Drive type: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(d.DriveType);
            Console.ForegroundColor = utils.ColorSelect(count);
            Console.Write("  Volume label: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(d.VolumeLabel);
            Console.ForegroundColor = utils.ColorSelect(count);
            Console.Write("  File system: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(d.DriveFormat);
            Console.ForegroundColor = utils.ColorSelect(count);
            Console.Write("  Available space to current user: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0, 15} bytes", d.AvailableFreeSpace);
            Console.ForegroundColor = utils.ColorSelect(count);
            Console.Write("  Available space to current user: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0} GB", d.AvailableFreeSpace * KbToGbFactor);
            Console.ForegroundColor = utils.ColorSelect(count);
            Console.Write("  Total available space: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0, 15} bytes", d.TotalFreeSpace);
            Console.ForegroundColor = utils.ColorSelect(count);
            Console.Write("  Total available space: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0} GB", d.TotalFreeSpace * KbToGbFactor);
            Console.ForegroundColor = utils.ColorSelect(count);
            Console.Write("  Total size of drive: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0, 15} bytes ", d.TotalSize);
            Console.ForegroundColor = utils.ColorSelect(count);
            Console.Write("  Total size of drive: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0} GB ", d.TotalSize * KbToGbFactor);
        }
        Console.WriteLine(new string('-', 79));
        if (count <= 11)
            count++;
        else
            count = 0;
    }
}

static void Video()
{
    var myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");
    var utils = new Utils();
    var KbToGbFactor = 1d / 1024 / 1024;
    var count = utils.RandomNumber();

    foreach (ManagementObject obj in myVideoObject.Get())
    {
        Console.ForegroundColor = utils.ColorSelect(count);
        Console.Write(" Name: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(obj["Name"]);
        Console.ForegroundColor = utils.ColorSelect(count);
        Console.Write(" Status: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(obj["Status"]);
        Console.ForegroundColor = utils.ColorSelect(count);
        Console.Write(" Caption: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(obj["Caption"]);
        Console.ForegroundColor = utils.ColorSelect(count);
        Console.Write(" DeviceID: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(obj["DeviceID"]);
        Console.ForegroundColor = utils.ColorSelect(count);
        Console.Write(" AdapterRAM: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(obj["AdapterRAM"]);
        Console.ForegroundColor = utils.ColorSelect(count);
        Console.Write(" AdapterDACType: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(obj["AdapterDACType"]);
        Console.ForegroundColor = utils.ColorSelect(count);
        Console.Write(" Monochrome: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(obj["Monochrome"]);
        Console.ForegroundColor = utils.ColorSelect(count);
        Console.Write(" InstalledDisplayDrivers: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(obj["InstalledDisplayDrivers"]);
        Console.ForegroundColor = utils.ColorSelect(count);
        Console.Write(" DriverVersion: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(obj["DriverVersion"]);
        Console.ForegroundColor = utils.ColorSelect(count);
        Console.Write(" VideoProcessor: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(obj["VideoProcessor"]);
        Console.ForegroundColor = utils.ColorSelect(count);
        Console.Write(" VideoArchitecture: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(obj["VideoArchitecture"]);
        Console.ForegroundColor = utils.ColorSelect(count);
        Console.Write(" VideoMemoryType: ");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(obj["VideoMemoryType"]);

        Console.WriteLine(new string('-', 79));
        if (count <= 11)
            count++;
        else
            count = 0;
    }
}

static void Processor()
{
    var myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");

    foreach (ManagementObject obj in myProcessorObject.Get())
    {
        Console.WriteLine("Name: {0}", obj["Name"]);
        Console.WriteLine("DeviceID: {0}", obj["DeviceID"]);
        Console.WriteLine("Manufacturer: {0}", obj["Manufacturer"]);
        Console.WriteLine("CurrentClockSpeed: {0}", obj["CurrentClockSpeed"]);
        Console.WriteLine("Caption  -  " + obj["Caption"]);
        Console.WriteLine("NumberOfCores  -  " + obj["NumberOfCores"]);
        Console.WriteLine("NumberOfEnabledCore  -  " + obj["NumberOfEnabledCore"]);
        Console.WriteLine("NumberOfLogicalProcessors  -  " + obj["NumberOfLogicalProcessors"]);
        Console.WriteLine("Architecture  -  " + obj["Architecture"]);
        Console.WriteLine("Family  -  " + obj["Family"]);
        Console.WriteLine("ProcessorType  -  " + obj["ProcessorType"]);
        Console.WriteLine("Characteristics  -  " + obj["Characteristics"]);
        Console.WriteLine("AddressWidth  -  " + obj["AddressWidth"]);
    }
}

static void SO()
{
    var myOperativeSystemObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

    foreach (ManagementObject obj in myOperativeSystemObject.Get())
    {
        Console.WriteLine("Caption  -  " + obj["Caption"]);
        Console.WriteLine("WindowsDirectory  -  " + obj["WindowsDirectory"]);
        Console.WriteLine("ProductType  -  " + obj["ProductType"]);
        Console.WriteLine("SerialNumber  -  " + obj["SerialNumber"]);
        Console.WriteLine("SystemDirectory  -  " + obj["SystemDirectory"]);
        Console.WriteLine("CountryCode  -  " + obj["CountryCode"]);
        Console.WriteLine("CurrentTimeZone  -  " + obj["CurrentTimeZone"]);
        Console.WriteLine("EncryptionLevel  -  " + obj["EncryptionLevel"]);
        Console.WriteLine("OSType  -  " + obj["OSType"]);
        Console.WriteLine("Version  -  " + obj["Version"]);
    }
}

static void Network()
{
    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

    if (nics == null || nics.Length < 1)
    {
        Console.WriteLine("  No network interfaces found.");
    }
    else
    {
        foreach (NetworkInterface adapter in nics)
        {
            IPInterfaceProperties properties = adapter.GetIPProperties();
            Console.WriteLine();
            Console.WriteLine(adapter.Description);
            Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
            Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
            Console.WriteLine("  Physical Address ........................ : {0}", adapter.GetPhysicalAddress().ToString());
            Console.WriteLine("  Operational status ...................... : {0}", adapter.OperationalStatus);
        }
    }
}

static void Sound()
{
    var myAudioObject = new ManagementObjectSearcher("select * from Win32_SoundDevice");

    foreach (ManagementObject obj in myAudioObject.Get())
    {
        Console.WriteLine("Name  -  " + obj["Name"]);
        Console.WriteLine("ProductName  -  " + obj["ProductName"]);
        Console.WriteLine("Availability  -  " + obj["Availability"]);
        Console.WriteLine("DeviceID  -  " + obj["DeviceID"]);
        Console.WriteLine("PowerManagementSupported  -  " + obj["PowerManagementSupported"]);
        Console.WriteLine("Status  -  " + obj["Status"]);
        Console.WriteLine("StatusInfo  -  " + obj["StatusInfo"]);
        Console.WriteLine(String.Empty.PadLeft(obj["ProductName"].ToString().Length, '='));
    }
}

static void Printer()
{
    var myPrinterObject = new ManagementObjectSearcher("select * from Win32_Printer");

    foreach (ManagementObject obj in myPrinterObject.Get())
    {
        Console.WriteLine("Name  -  " + obj["Name"]);
        Console.WriteLine("Network  -  " + obj["Network"]);
        Console.WriteLine("Availability  -  " + obj["Availability"]);
        Console.WriteLine("Is default printer  -  " + obj["Default"]);
        Console.WriteLine("DeviceID  -  " + obj["DeviceID"]);
        Console.WriteLine("Status  -  " + obj["Status"]);

        Console.WriteLine(String.Empty.PadLeft(obj["Name"].ToString().Length, '='));
    }
}

//GAME
//static void game()
//{
//    while (true)
//    {
//        Console.WriteLine("-->");
//        if (Console.ReadKey(true).Key == ConsoleKey.DownArrow)
//        {
//            while (true)
//            {
//                Console.WriteLine("|");
//                Console.WriteLine("|");
//                Console.WriteLine("v");
//            }
//        }
//    }
//}