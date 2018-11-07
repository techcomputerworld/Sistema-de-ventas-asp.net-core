using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sistem_Ventas.Models;

namespace Sistem_Ventas.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IServiceProvider serviceProvider)
        {
            CreateRoles(serviceProvider);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //metodo para crear los roles asincronico 
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            string mensaje;
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                //a la hora de crear los roles, quiero introducir SuperAdmin, Admin, Manager por zona, User
                String[] rolesName = { "Admin", "User" };
                foreach (var item in rolesName)
                {
                    //esto nos sirve para verificar que cada rol existe en la tabla de roles 
                    //es un metodo asincronico que le indicamos que mientras no acabe este metodo, no puede acabar el CreateRoles
                    var roleExist = await roleManager.RoleExistsAsync(item);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(item));
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            

        }
    }
}
