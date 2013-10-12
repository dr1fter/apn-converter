using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using apnConverter.gui;


namespace apnConverter
{
    class Program
    {
        static bool useGUI = true;

        [STAThread]
        static void Main(string[] args)
        {
            useGUI = (args != null && args.Length == 0);
            if (useGUI)
            {
                Console.WriteLine("APN-Converter starting using graphical user interface..");
                Console.WriteLine("leave this window opened to receive status messages");
              
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Form form = new MainForm();
                System.Windows.Forms.Application.Run(form);
            }
            else
            {
                //CreateApn("d:/test.bmp", "test", "d:/test.apn");
                
                Console.Write(HEADER_COPYRIGHT);
                //ToBitmap(@"d:\dev\alpine\res\xpd008.apn",@"d:\dev\alpine\res\test09.bmp");
                parseArgs(args);
            }
        }

        public static void parseArgs(string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    DisplayHelp();
                    break;
                case 1:
                    switch (args[0].ToLower())
                    {
                        case "tobmp":
                            Console.WriteLine("Usage: APNCONV toBmp source destination");
                            break;
                        case "toapn":
                            Console.WriteLine("Usage: APNCONV toApn source name destination");
                            break;
                        default:
                            Console.WriteLine("argument not recognised: {0}\ntry APNCONV without arguments for help.",
                                args[0]);
                            break;
                    }
                    break;
                case 2:
                    switch (args[1].ToLower())
                    {
                        case "tobmp":
                            Console.WriteLine("Usage: APNCONV toBmp source destination");
                            break;
                        case "toapn":
                            Console.WriteLine("Usage: APNCONV toApn source name destination");
                            break;
                        default:
                            Console.WriteLine("argument not recognised: {0}\ntry APNCONV without arguments for help.",
                                args[0]);
                            break;
                    }
                    break;
                case 3:
                    switch (args[0].ToLower())
                    {
                        case "tobmp":
                            ToBitmap(args[1], args[2]);
                            break;
                        case "toapn":
                            Console.WriteLine("Usage: APNCONV toApn source name destination");
                            break;
                        default:
                            Console.WriteLine("argument not recognised: {0}\ntry APNCONV without arguments for help.", 
                                args[0]);
                            
                            break;
                    }
                    break;
                default:
                    {
                        switch (args[0].ToLower())
                        {
                            case "tobmp":
                                Console.WriteLine("WARNING: too many parameters specified. Ommitting surplus parameters.");
                                ToBitmap(args[1], args[2]);
                                break;
                            case "toapn":
                                CreateApn(args[1], args[2], args[3]);
                                break;
                            default:
                                Console.WriteLine("argument not recognised: {0}\ntry APNCONV without arguments for help.",
                                    args[0]);
                                break;
                        }
                        break;
                    }
            }
        }
        public static bool CreateApn(string bmpFile, string name, string outputFile)
        {
           // bool succ =  Apn.FromBitmap(@"d:\dev\alpine\res\09.bmp", "XPD009", @"d:\dev\alpine\res\test00.apn");
            bool succ = Apn.FromBitmap(bmpFile, name, outputFile);
            if (succ)
            {
                Console.WriteLine("Conversion succeeded.");
                //Console.WriteLine("Conversion succeeded. Press [ENTER] to exit.");
                //Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Errors occurred during conversion. Refer to the prior output for error details.");
            }
            return succ;
        }
        public static bool ToBitmap(string apnSourceFile, string destinationBmpFile)
        {
            Apn apn = new Apn();
            Console.WriteLine("trying to load source file (apn)..");
            if (!apn.Load(apnSourceFile))
            {
                Console.WriteLine("ERROR: could not load source file. See prior messages for details.");
                return false;
            }
            Console.WriteLine("Successfully parsed source file.");
            Console.WriteLine("Creating bitmap..");
            apn.ToBitmapFile(destinationBmpFile);
            Console.WriteLine("Done.");
            //Console.WriteLine("Done. Press [ENTER] to exit.");
            //Console.ReadLine();
            return true;
        }
        public static void DisplayHelp()
        {
            Console.Write(HELP_MESSAGE);
        }

        private static readonly string HEADER_COPYRIGHT = "APN <-> BMP - Converter\n"
           + "  Converts Alpine .apn-Files to the Windows Bitmap (.bmp) format and vice versa.\n"
           + "\n"
           + "Copyright (c) 2008 by Christian Cwienk\n"
           + "\n"
           + "Use at your own risk. The author cannot be held responsible for any damage caused by this tool.\n";
           
       private static readonly string HELP_MESSAGE =
           "Usage:\n"
           + " APNCONV toBmp  source destination\n"
           + " APNCONV toApn  source name destination\n"
           + "\n"
           + "  toBmp\tThe source (which must be a Alpine .apn-file)\n"
           + "\twill be converted to a Windows BMP-File specified by DESTINATION\n"
           + "\n"
           + "  toApn\tThe source (which must be a Windows Bitmap (.bmp)\n"
           + "\twill be converted to a Alpine APN-file specified by DESTINATION\n";


    }
}
