using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace SDi_Test_Problem
{
    class Program
    {
        static void Main(string[] args)
        {
            //sort the arguments and parse the file
            Console.WriteLine("Opening File...");
            String fileLocation ="";
            String command = "";
            if (args[0] == "-file")
                fileLocation = args[1];
            String fileText = readTxtFile(@fileLocation);
            if (args[2] == "-sort")
                command = args[3];

            Console.WriteLine("File read!");
            Console.WriteLine("Parsing Data...");

            //creates a list to hold all report objects
            List<MedicalReport> reports = parseFile(fileText);
            List<MedicalReport> sortedReports = reports;
            Console.WriteLine("Data has been parsed! \n\n\n");
            
                //sort list by the given command
                switch (command)
                {
                    case "facilityID":
                        sortedReports = reports.OrderBy(o => o.facilityID).ToList(); break;
                    case "facilityName":
                        sortedReports = reports.OrderBy(o => o.facilityName).ToList(); break;
                    case "facilityLocation":
                        sortedReports = reports.OrderBy(o => o.facilityLocation).ToList(); break;
                    case "patientName":
                        sortedReports = reports.OrderBy(o => o.patientName).ToList(); break;
                    case "gender":
                        sortedReports = reports.OrderBy(o => o.gender).ToList(); break;
                    case "dateOfBirth":
                        sortedReports = reports.OrderBy(o => o.dateOfBirth).ToList(); break;
                    case "patientID":
                        sortedReports = reports.OrderBy(o => o.patientID).ToList(); break;
                    case "procedure":
                        sortedReports = reports.OrderBy(o => o.procedure).ToList(); break;
                    case "numberofFilms":
                        sortedReports = reports.OrderBy(o => o.numberOfFilms).ToList(); break;
                    case "laterality":
                        sortedReports = reports.OrderBy(o => o.laterality).ToList(); break;
                    case "contrast":
                        sortedReports = reports.OrderBy(o => o.contrast).ToList(); break;
                    case "reason":
                        sortedReports = reports.OrderBy(o => o.reason).ToList(); break;
                    case "examDate":
                        sortedReports = reports.OrderBy(o => o.examDate).ToList(); break;
                    case "radiologist":
                        sortedReports = reports.OrderBy(o => o.radiologist).ToList(); break;
                    case "orderingPH":
                        sortedReports = reports.OrderBy(o => o.orderingPH).ToList(); break;
                    case "POS":
                        sortedReports = reports.OrderBy(o => o.POS).ToList(); break;
                    case "reportStatus":
                        sortedReports = reports.OrderBy(o => o.reportStatus).ToList(); break;
                    case "attendingDoctor":
                        sortedReports = reports.OrderBy(o => o.attendingDoctor).ToList(); break;
                    case "admittingDoctor":
                        sortedReports = reports.OrderBy(o => o.admittingDoctor).ToList(); break;
                    case "directorName":
                        sortedReports = reports.OrderBy(o => o.directorName).ToList(); break;
                    default:
                        Console.WriteLine("Error: I don't recognize that.");
                        break;
                }
                    
                //output the 
                foreach (MedicalReport r in sortedReports)
                {
                    foreach (var variable in typeof(MedicalReport).GetFields())
                    {
                        Console.WriteLine("{0}: {1}", variable.Name, variable.GetValue(r));
                    }
                    Console.WriteLine("========================================================\n\n");
                }
                command = Console.ReadLine();
                Environment.Exit(0);
            

        }

        //reads the text of any file and returns it in a string
        static string readTxtFile(String fileLocation)
        {
            string fileText = System.IO.File.ReadAllText(fileLocation);
            return fileText;
        }

        //function that seperates the txt file into seperate strings to more easily parse
        static List<MedicalReport> parseFile(String fileText)
        {
            List<MedicalReport> reports = new List<MedicalReport>();
            fileText = fileText.Trim();
            //seperates each report from the others
            String[] stringSeperator = new string[] {"===================END OF RESULT==================="};
            String[] result = fileText.Split(stringSeperator, StringSplitOptions.None);


            //parses each report and adds them to the list
            foreach (string sub in result)
            {
                if (sub != "")
                    reports.Add(parseReport(sub));
            }

            //return the full list
            return reports;
        }

        //parses each individual report
        static MedicalReport parseReport(String text)
        {
            MedicalReport report = new MedicalReport();
            text = text.ToUpper();

            //seperate each report into catagories
            char[] stringSeperator = new char[] { '\n' };

            String[] splits;
            String[] result = text.Split(stringSeperator, StringSplitOptions.None);

            //seperate each string again into the variable name and value
            stringSeperator = new char[] { ':' };
            foreach(string s in result)
            {
                splits = s.Split(stringSeperator, StringSplitOptions.None);
                if (!String.IsNullOrWhiteSpace(s))
                {
                    //sorts the reports into the correct variables
                    switch (splits[0])
                    {
                        case "FACILITYID":
                        case "FACILITY ID":
                            report.facilityID = Int32.Parse(splits[1]); break;

                        case "FACILITYNAME":
                        case "FACILITY NAME":
                            report.facilityName = splits[1]; break;

                        case "FACILITYLOCATION":
                        case "FACILITY LOCATION":
                            report.facilityLocation = splits[1]; break;

                        case "PATIENT":
                            report.patientName = splits[1]; break;

                        case "GENDER":
                            report.gender = splits[1]; break;

                        case "DOB":
                            report.dateOfBirth = Convert.ToDateTime(splits[1]); break;

                        case "PATIENTID":
                        case "PATIENT ID":
                            report.patientID = Int32.Parse(splits[1]); break;

                        case "PROCEDURE":
                            report.procedure = splits[1]; break;

                        case "NUMBEROFFILMS":
                        case "NUMBER OFFILMS":
                        case "NUMBEROF FILMS":
                        case "NUMBER OF FILMS":
                            report.numberOfFilms = Int32.Parse(splits[1]); break;

                        case "LATERALITY":
                            report.laterality = splits[1]; break;

                        case "CONTRAST":
                            report.contrast = splits[1]; break;

                        case "REASON":
                            report.reason = splits[1]; break;

                        case "EXAM DATE":
                        case "EXAMDATE":
                            report.examDate = Convert.ToDateTime(splits[1] + ":" + splits[2] + ":" + splits[3]); break;

                        case "RADIOLOGIST":
                            report.radiologist = splits[1]; break;

                        case "ORDERINGPH":
                        case "ORDERING PH":
                            report.orderingPH = splits[1]; break;

                        case "POS":
                            report.POS = splits[1]; break;

                        case "REPORT STATUS":
                        case "REPORTSTATUS":
                            report.reportStatus = Int32.Parse(splits[1]); break;

                        case "ADMITTING DOCTOR":
                        case "ADMITTINGDOCTOR":
                            report.admittingDoctor = splits[1]; break;

                        case "ATTENDING DOCTOR":
                        case "ATTENDINGDOCTOR":
                            report.attendingDoctor = splits[1]; break;

                        case "DIRECTORNAME":
                        case "DIRECTOR NAME":
                            report.directorName = splits[1]; break;

                        case "":
                        case "\t":
                        case "\n":
                            break;

                        default:
                            Console.Write("Something's not quite right..."); break;
                    }
                }

            }
            return report;
        }


    }

    //class to hold all values of an individual medical report
    class MedicalReport
    {

        //private variables
        public String      facilityName, 
                            facilityLocation,
                            patientName, 
                            gender, 
                            procedure,
                            laterality,
                            contrast,
                            reason,
                            radiologist,
                            orderingPH,
                            POS, 
                            attendingDoctor,
                            admittingDoctor,
                            directorName;

        public int         facilityID,
                            patientID,
                            numberOfFilms,
                            reportStatus;

        public DateTime    dateOfBirth, 
                            examDate;
    }
}
