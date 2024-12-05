using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class PacienteController : Controller
    {
        // GET: Paciente/List
        public ActionResult TablaPaciente()
        {
            ML.Result result = BL.Paciente.GetAll();

            if (result.Correct)
            {
                ML.Paciente pacienteModel = new ML.Paciente
                {
                    Pacientes = result.Objects
                };
                return View(pacienteModel); // Enviamos el modelo a la vista.
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
                return View("Error");
            }
        }


        // GET: Paciente/Details/{id}
        public ActionResult Details(int id)
        {
            ML.Result result = BL.Paciente.GetById(id);

            if (result.Correct)
            {
                return View(result.Object); // Enviamos el objeto paciente a la vista.
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
                return View("Error");
            }
        }

        // GET: Paciente/Form/{id} (para editar) o Paciente/Form (para agregar)
        public ActionResult Form(int? id)
        {
            ML.Paciente paciente = new ML.Paciente();

            if (id.HasValue) // Si el ID tiene valor, es una edición.
            {
                ML.Result result = BL.Paciente.GetById(id.Value);

                if (result.Correct)
                {
                    paciente = (ML.Paciente)result.Object;
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return View("Error");
                }
            }

            return View(paciente); // Enviamos el modelo paciente a la vista.
        }

        // POST: Paciente/Form
        [HttpPost]
        public ActionResult Form(ML.Paciente paciente)
        {
            ML.Result result;

            if (paciente.IdPaciente == 0) // Si el ID es 0, es un nuevo registro.
            {
                result = BL.Paciente.Add(paciente);
                ViewBag.ActionMessage = "Paciente agregado correctamente.";
            }
            else // Caso contrario, actualizamos el registro existente.
            {
                result = BL.Paciente.Update(paciente);
                ViewBag.ActionMessage = "Paciente actualizado correctamente.";
            }

            if (result.Correct)
            {
                return RedirectToAction("TablaPaciente"); // Redirigimos a la lista de pacientes.
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
                return View("Error");
            }
        }

        // GET: Paciente/Delete/{id}
        public ActionResult Delete(int id)
        {
            ML.Result result = BL.Paciente.Delete(id);

            if (result.Correct)
            {
                ViewBag.ActionMessage = "Paciente eliminado correctamente.";
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
                return View("Error");
            }

            return RedirectToAction("TablaPaciente"); // Redirigimos a la lista de pacientes.
        }
    }
}