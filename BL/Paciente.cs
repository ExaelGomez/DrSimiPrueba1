using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

namespace BL
{
    public class Paciente
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DrSimiEntities context = new DL_EF.DrSimiEntities())
                {
                    var listaPaciente = context.Paciente.Select(paciente => new ML.Paciente
                    {
                        IdPaciente = paciente.IdPaciente,
                        Nombre = paciente.Nombre,
                        ApellidoPaterno = paciente.ApellidoPaterno,
                        ApellidoMaterno = paciente.ApellidoMaterno,
                        FechaNacimiento = paciente.FechaNacimiento
                    }).ToList();

                    if (listaPaciente.Count > 0)
                    {
                        result.Objects = listaPaciente.Cast<object>().ToList();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetById(int id)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DrSimiEntities context = new DL_EF.DrSimiEntities())
                {
                    var paciente = context.Paciente
                        .Where(pacienteBy => pacienteBy.IdPaciente == id)
                        .Select(pacienteBy => new ML.Paciente
                        {
                            IdPaciente = pacienteBy.IdPaciente,
                            Nombre = pacienteBy.Nombre,
                            ApellidoPaterno = pacienteBy.ApellidoPaterno,
                            ApellidoMaterno = pacienteBy.ApellidoMaterno,
                            FechaNacimiento = pacienteBy.FechaNacimiento
                        }).FirstOrDefault();

                    if (paciente != null)
                    {
                        result.Object = paciente;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Paciente no encontrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Add(ML.Paciente paciente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DrSimiEntities context = new DL_EF.DrSimiEntities())
                {
                    DL_EF.Paciente nuevoPaciente = new DL_EF.Paciente
                    {
                        Nombre = paciente.Nombre,
                        ApellidoPaterno = paciente.ApellidoPaterno,
                        ApellidoMaterno = paciente.ApellidoMaterno,
                        FechaNacimiento = paciente.FechaNacimiento
                    };

                    context.Paciente.Add(nuevoPaciente);
                    context.SaveChanges();

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Update(ML.Paciente paciente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DrSimiEntities context = new DL_EF.DrSimiEntities())
                {
                    var pacienteExistente = context.Paciente.FirstOrDefault(p => p.IdPaciente == paciente.IdPaciente);

                    if (pacienteExistente != null)
                    {
                        pacienteExistente.Nombre = paciente.Nombre;
                        pacienteExistente.ApellidoPaterno = paciente.ApellidoPaterno;
                        pacienteExistente.ApellidoMaterno = paciente.ApellidoMaterno;
                        pacienteExistente.FechaNacimiento = paciente.FechaNacimiento;

                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Paciente no encontrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Delete(int id)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.DrSimiEntities context = new DL_EF.DrSimiEntities())
                {
                    var pacienteExistente = context.Paciente.FirstOrDefault(p => p.IdPaciente == id);

                    if (pacienteExistente != null)
                    {
                        context.Paciente.Remove(pacienteExistente);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Paciente no encontrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
