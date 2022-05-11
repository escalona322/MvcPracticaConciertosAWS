using Microsoft.AspNetCore.Mvc;
using MvcPracticaConciertosAWS.Models;
using MvcPracticaConciertosAWS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPracticaConciertosAWS.Controllers
{
    public class ConciertosController : Controller
    {
        private ServiceApiConciertos service;

        public ConciertosController(ServiceApiConciertos service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Evento> eventos = await this.service.GetEventosAsync();
            return View(eventos);
        }

        public async Task<IActionResult> Details(int idevento)
        {
            Evento eventos = await this.service.FindEvento(idevento);
            return View(eventos);
        }

        public async Task<IActionResult> EventosCategoria()
        {
            List<CategoriaEvento> categorias = await this.service.GetCategorias();
            ViewData["CATEGORIAS"] = categorias;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EventosCategoria(int IdCategoria)
        {
            List<CategoriaEvento> categorias = await this.service.GetCategorias();
            ViewData["CATEGORIAS"] = categorias;
            List<Evento> eventos = await this.service.GetEventosCategoriaAsync(IdCategoria);
            return View(eventos);
        }

        public async Task<IActionResult> InsertEventoAsync()
        {
            List<CategoriaEvento> categorias = await this.service.GetCategorias();
            ViewData["CATEGORIAS"] = categorias;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertEvento(Evento ev)
        {
            await this.service.InsertarEvento(ev);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateCategoriaEvento()
        {
            List<Evento> eventos = await this.service.GetEventosAsync();
            List<CategoriaEvento> categorias = await this.service.GetCategorias();

            ViewData["CATEGORIAS"] = categorias;
            return View(eventos);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategoriaEvento(int idevento, int idcategoria)
        {
            await this.service.UpdateEvento(idevento, idcategoria);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteEvento(int idevento)
        {
            await this.service.DeleteEventoAsync(idevento);
            return RedirectToAction("Index");
        }
    }
}
