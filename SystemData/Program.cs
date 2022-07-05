﻿var exit = false;
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
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine(@"
 __v_
(____\/{  Oprima cualquier tecla para volver atras");
    Console.ForegroundColor = ConsoleColor.Gray;

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
                Console.WriteLine(ld.Properties["Name"].Value);

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

                Console.WriteLine("   PhysicalName: {0}", physicalName);
                Console.WriteLine("   DiskName: {0}", diskName);
                Console.WriteLine("   DiskModel: {0}", diskModel);
                Console.WriteLine("   DiskInterface: {0}", diskInterface);
                Console.WriteLine("   Capabilities: {0}", capabilities);
                Console.WriteLine("   MediaLoaded: {0}", mediaLoaded);
                Console.WriteLine("   MediaType: {0}", mediaType);
                Console.WriteLine("   MediaSignature: {0}", mediaSignature);
                Console.WriteLine("   MediaStatus: {0}", mediaStatus);

                Console.WriteLine("   DriveName: {0}", driveName);
                Console.WriteLine("   DriveId: {0}", driveId);
                Console.WriteLine("   DriveCompressed: {0}", driveCompressed);
                Console.WriteLine("   DriveType: {0}", driveType);
                Console.WriteLine("   FileSystem: {0}", fileSystem);
                Console.WriteLine("   FreeSpace: {0} bytes", freeSpace);
                Console.WriteLine("   TotalSpace: {0} bytes", totalSpace);
                Console.WriteLine("   FreeSpace: {0} GB", freeSpace);
                Console.WriteLine("   TotalSpace: {0} GB", totalSpace);
                Console.WriteLine("   DriveMediaType: {0}", driveMediaType);
                Console.WriteLine("   VolumeName: {0}", volumeName);
                Console.WriteLine("   VolumeSerial: {0}", volumeSerial);

                Console.WriteLine(new string('-', 79));
            }
        }
    }
}

static void HardDisks()
{
    var KbToGbFactor = 1d / 1024 / 1024;
    DriveInfo[] allDrives = DriveInfo.GetDrives();
    ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
    int[] color_index = new int[] { 14, 13, 12, 11, 10, 9, 6, 5, 4, 3, 2, 1 };
    var count = 0;

    foreach (DriveInfo d in allDrives)
    {
        Console.WriteLine("Drive {0}", d.Name);
        if (d.IsReady == true)
        {
            Console.ForegroundColor = colors[color_index[count]];
            Console.Write("  Drive type: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(d.DriveType);
            Console.ForegroundColor = colors[color_index[count]];
            Console.Write("  Volume label: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(d.VolumeLabel);
            Console.ForegroundColor = colors[color_index[count]];
            Console.Write("  File system: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(d.DriveFormat);
            Console.ForegroundColor = colors[color_index[count]];
            Console.Write("  Available space to current user: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0, 15} bytes", d.AvailableFreeSpace);
            Console.ForegroundColor = colors[color_index[count]];
            Console.Write("  Available space to current user: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0} GB", d.AvailableFreeSpace * KbToGbFactor);
            Console.ForegroundColor = colors[color_index[count]];
            Console.Write("  Total available space: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0, 15} bytes", d.TotalFreeSpace);
            Console.ForegroundColor = colors[color_index[count]];
            Console.Write("  Total available space: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0} GB", d.TotalFreeSpace * KbToGbFactor);
            Console.ForegroundColor = colors[color_index[count]];
            Console.Write("  Total size of drive: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0, 15} bytes ", d.TotalSize);
            Console.ForegroundColor = colors[color_index[count]];
            Console.Write("  Total size of drive: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("{0} GB ", d.TotalSize * KbToGbFactor);
        }
        Console.WriteLine(new string('-', 79));
        count = count <= 11 ? count++ : 0;
        count++;
    }
}

static void Video()
{
    var myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");

    foreach (ManagementObject obj in myVideoObject.Get())
    {
        Console.WriteLine(" Name: {0}", obj["Name"]);
        Console.WriteLine(" Status: {0}", obj["Status"]);
        Console.WriteLine(" Caption: {0}", obj["Caption"]);
        Console.WriteLine(" DeviceID: {0}", obj["DeviceID"]);
        Console.WriteLine(" AdapterRAM: {0}", obj["AdapterRAM"]);
        Console.WriteLine(" AdapterDACType: {0}", obj["AdapterDACType"]);
        Console.WriteLine(" Monochrome: {0}", obj["Monochrome"]);
        Console.WriteLine(" InstalledDisplayDrivers: {0}", obj["InstalledDisplayDrivers"]);
        Console.WriteLine(" DriverVersion: {0}", obj["DriverVersion"]);
        Console.WriteLine(" VideoProcessor: {0}", obj["VideoProcessor"]);
        Console.WriteLine(" VideoArchitecture: {0}", obj["VideoArchitecture"]);
        Console.WriteLine(" VideoMemoryType: {0}", obj["VideoMemoryType"]);
        Console.WriteLine(new string('-', 79));
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
