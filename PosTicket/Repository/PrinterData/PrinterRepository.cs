using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Printing;
using System.Drawing.Printing;
using PosTicket.Repository.Interface;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using PosTicket.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Drawing;

namespace PosTicket.Repository.PrinterData
{
    class PrinterRepository : IPrinterRepository
    {
        private ReadConfig readConfig { get; set; }
        public PrinterRepository()
        {
            readConfig = new ReadConfig();
        }
        public IEnumerable<Printer> GetPrinter()
        {
            List<Printer> printers = new List<Printer>();
            LocalPrintServer printServer = new LocalPrintServer();
            PrintQueueCollection printQueuesOnLocalServer = printServer.GetPrintQueues(new[] {
                EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections
            });
            foreach (PrintQueue printer in printQueuesOnLocalServer)
            {
                printers.Add(new Printer { Name = printer.Name });
                //Debug.WriteLine("\tThe shared printer : " + printer.Name);
            }
            return printers;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount, string dataType)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.
            try
            {
                di.pDocName = "Saloka Ticket Document";
                //di.pDataType = "XPS_PASS";
                di.pDataType = dataType;

                // Open the printer.
                if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
                {
                    // Start a document.
                    if (StartDocPrinter(hPrinter, 1, di))
                    {
                        // Start a page.
                        if (StartPagePrinter(hPrinter))
                        {
                            // Write your bytes.
                            bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                            EndPagePrinter(hPrinter);
                        }
                        EndDocPrinter(hPrinter);
                    }
                    ClosePrinter(hPrinter);
                }
                // If you did not succeed, GetLastError may give more information
                // about why not.
                if (bSuccess == false)
                {
                    dwError = Marshal.GetLastWin32Error();
                }
            }
            catch { }
            return bSuccess;
        }

        public static bool SendFileToPrinter(string szPrinterName, string szFileName, string dataType)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess = false;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength, dataType);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return bSuccess;
        }
        public static bool SendStringToPrinter(string szPrinterName, string szString, string dataType)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            bool success = SendBytesToPrinter(szPrinterName, pBytes, dwCount, dataType);
            Marshal.FreeCoTaskMem(pBytes);
            return success;
        }
        public void PrintReceipt(string printerName,List<string> data)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, args) => CetakReceipt(data, args.Graphics);
            pd.PrinterSettings.PrinterName = printerName;
            if (pd.PrinterSettings.IsValid)
            {
                pd.Print();
            }
        }
        public void CetakReceipt(List<string> data, Graphics graphics)
        {
            Font regular = new Font(FontFamily.GenericSansSerif, 10.0f, FontStyle.Regular);
            Font bold = new Font(FontFamily.GenericSansSerif, 14.0f, FontStyle.Bold);
            graphics.DrawString("SALOKA THEME PARK", bold, Brushes.Black, 30, 10);
            graphics.DrawString("Jl.Fatmawati No.154, Gumuksari, Lopait", regular, Brushes.Black, 10, 40);
            graphics.DrawString("Kec.Tuntang, Semarang", regular, Brushes.Black, 60, 60);
            graphics.DrawString("Jawa Tengah 50773, Indonesia", regular, Brushes.Black, 45, 80);
            graphics.DrawLine(Pens.Black, 10, 100, 320, 100);
            //graphics.DrawString("SALOKA BRO", bold, Brushes.Black, 110, 80);
            //graphics.DrawLine(Pens.Black, 10, 100, 320, 100);
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] == "garis")
                {
                    graphics.DrawLine(Pens.Black, 10, 120 + i * 20, 320, 120 + i * 20);
                }
                else
                {
                    graphics.DrawString(data[i], regular, Brushes.Black, 10, 120 + i * 20);
                }
            }
            regular.Dispose();
            bold.Dispose();
        }
        public bool CetakReceiptLine(string printerName, List<string> data)
        {
            StringBuilder label = new StringBuilder();

            foreach (string Isidata in data)
            {
                label.AppendLine(Isidata); ;
            }
            SendStringToPrinter(printerName, label.ToString(),"RAW");
            return true;
        }
        public async Task<bool> CetakTicket(string printerName, List<Ticket> data)
        {
            List<int> ticket_ids = new List<int>();
            ConfigList = readConfig.GetAllConfigs();
            foreach(Ticket ticket in data)
            {
                ticket_ids.Add(ticket.id);
            }
            await SendPrintingStatus(ticket_ids, ConfigList[0].api_key, ConfigList[0].server_url, "printing");
            foreach (Ticket printer in data)
            {
                StringBuilder label = new StringBuilder();
                label.AppendLine("^XA");
                label.AppendLine("^POI");
                label.AppendLine("^FO140,300^BY4^BQN,2,8^FDAM,A" + printer.barcode + "^FS ");
                label.AppendLine("^FO105,485^ADN,12,12^FD" + printer.barcode + "^FS");
                label.AppendLine("^FO140,510^ADN,12,12^FD" + printer.line1 + "^FS");
                label.AppendLine("^FO140,535^ADN,12,12^FD" + printer.line2 + "^FS");
                label.AppendLine("^FO140,560^ADN,12,12^FD" + printer.line3 + "^FS");
                label.AppendLine("^FO420,450^ABB,5,10^FD" + printer.line4 + "^FS");
                label.AppendLine("^FO10,480^ABB,5,10^FD" + printer.line5 + "^FS");
                label.AppendLine("^FO30,615^ADN,12,8^FD" + printer.line6 + "^FS");
                label.AppendLine("^XZ");
                if(SendStringToPrinter(printerName, label.ToString(), "XPS_PASS") == true)
                {
                    await UpdateStatus(printer.id, ConfigList[0].api_key, ConfigList[0].server_url, "printed");
                }
                else
                {
                    await UpdateStatus(printer.id, ConfigList[0].api_key, ConfigList[0].server_url, "draft");
                }
            }
            return true;
        }

        public async Task UpdateStatus(int ticket_id, string api_key, string server_url, string status)
        {
            var client = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, server_url + "/saloka/ticket/print_proxy/"+status+"/" + ticket_id);
            requestMessage.Headers.Add("X-Api-Key", api_key);
            requestMessage.Content = new StringContent("{}",
                                    Encoding.UTF8,
                                    "application/json");
            var a = await client.SendAsync(requestMessage);
        }
        public async Task SendPrintingStatus(List<int> ticket_ids, string api_key, string server_url, string status)
        {
            var request = new RestRequest(Method.POST);
            RestClient url = new RestClient(server_url+"/saloka/ticket/print_proxy/"+status);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-Api-Key",api_key);
            request.AddParameter("application/json", "{\"ticket_ids\":"+JsonConvert.SerializeObject(ticket_ids)+"}", ParameterType.RequestBody);
            _ = await url.ExecuteTaskAsync(request);
        }

        public List<Config> ConfigList { get; set; }
    }
}
