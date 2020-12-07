using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;
namespace First
{
    class Program
    {
        static void Main(string[] args)
        {
            var appt_a = new Appointment
            {
                Patient = new Patient
                {
                    Name = "",
                    Email = ""
                },
                Time = DateTime.MinValue

            };

            WriteLine(new AppointmentService().Create(appt_a));
            var appt_b = new Appointment
            {
                Patient = new Patient
                {
                    Name = "Jorge",
                    Email = "jburnvel@gmail.com"
                },
                Time = new DateTime(2020, 03, 08, 15, 20, 19)

            };

            WriteLine(new AppointmentService().Create(appt_b));

            //WriteLine(new AppointmentService().Create("Rodrigo Valencia", "juanortizgmail.com", new DateTime(2019, 02, 02, 15, 20, 35 )));

            var appt = new Appointment
            {
                Patient = new Patient
                {
                    Name = "Rodrigo"
                },
                Time = new DateTime(2019, 03, 08, 15, 20, 19)

            };
            WriteLine(new AppointmentService().Create(appt));

            ReadLine();
        }
    }

    public class Patient
    {
        public string Name { get; set; }
        public string Email { get; set; } = "";
    }

    public class Appointment
    {
        public DateTime Time { get; set; }
        public Patient Patient { get; set; }
    }

    public class ValidationResult
    {
        public List<string> ErrorMessage { get; set; } = new List<string>();
        public List<string> ErrorLevel { get; set; } = new List<string>();
        public List<string> ErrorFull { get; set; } = new List<string>();
        public bool IsValid { get { return !ErrorMessage.Any(); } }
    }

    public static class AppointmentServiceValidator
    {
        public static ValidationResult  Validate(Appointment appointment)
        {
            ValidationResult validation = new ValidationResult();

            if (string.IsNullOrEmpty(appointment.Patient.Name))
            {
                validation.ErrorMessage.Add("La cita no puede ser agendada, debido a que debe proporcionar un nombre de paciente.");
                validation.ErrorLevel.Add("Warning");
                validation.ErrorFull.Add(validation.ErrorLevel.Last() + " > "+ validation.ErrorMessage.Last());
            }
            if (appointment.Time.Equals(DateTime.MinValue))
            { 
                validation.ErrorMessage.Add("La cita no puede ser agendada, debido a que debe proporcionar la hora de la cita.");
                validation.ErrorLevel.Add("Fatal");
                validation.ErrorFull.Add(validation.ErrorLevel.Last() + " > " + validation.ErrorMessage.Last());
            }
            if (!appointment.Patient.Email.Contains("@") || string.IsNullOrEmpty(appointment.Patient.Email))
            {
                validation.ErrorMessage.Add($"La cita no puede ser agendada, debido a que debe proporcionar un email valido.");
                validation.ErrorLevel.Add("Low");
                validation.ErrorFull.Add(validation.ErrorLevel.Last() + " > " + validation.ErrorMessage.Last());
            }

            return validation;
        }
    } 
    
    public class AppointmentService
    {
        // 1 Crear clase validaciones
        // 2 Crear Objeto llamador paciente
        // Crear objeto Cita que tenga un paciente y la fecha cita
        public string Create(Appointment appointment)
        {
            WriteLine("Solicitando Cita: ");
            WriteLine("Resultado de la cita solicitada: ");
            ValidationResult validation = AppointmentServiceValidator.Validate(appointment);

            return validation.IsValid ?
                $"La cita quedo agendada para el paciente {appointment.Patient.Name}, el dia {appointment.Time.Date}." + "\n"
                : string.Join(Environment.NewLine, validation.ErrorFull) + "\n";

            //validation.ErrorLevel
        }

    }
}
