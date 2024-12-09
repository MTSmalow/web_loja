using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebLoja.Data;
using WebLoja.Models;

namespace WebLoja.Controllers
{
    public class ClientesController : Controller
    {
        private readonly WebLojaContext _context;

        public ClientesController(WebLojaContext context)
        {
            _context = context;
        }

        

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Autenticacao,Endereco")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Autenticacao = BibliotecaCriptografia.Criptografia.HashRonny( cliente.Autenticacao);
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        [Authorize]

        public async Task<IActionResult> Edit()
        {
            string email = User.Claims.Where(c=>c.Type == ClaimTypes.Name).FirstOrDefault().Value;
            if (email != null)
            {
                Cliente cliente = _context.Cliente.FirstOrDefault(c=>c.Email==email);
               
                return View(cliente);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Autenticacao,Endereco")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cliente == null)
            {
                return Problem("Entity set 'WebLojaContext.Cliente'  is null.");
            }
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Cliente?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<IActionResult> Login([Bind("Email, Senha")]  ClienteAutDt cliente)
        {
            if (cliente != null) {

                Cliente? clientDb = await _context.Cliente.FirstOrDefaultAsync(c => c.Email == cliente.Email);


                if (clientDb != null)
                {

                    if (cliente.Email == clientDb.Email &&
                        clientDb.Autenticacao == BibliotecaCriptografia.Criptografia.HashRonny(cliente.Senha))
                    {
                        string perfil = clientDb.Email == "admin@admin" ? "admin" : "cliente";

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, cliente.Email),
                            new Claim("FullName", clientDb.Nome),
                            new Claim(ClaimTypes.Role, perfil),
                        };

                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties();

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity), authProperties);
                        return RedirectToAction("Index", "Home");

                    }
                }

            }

            return RedirectToAction("Login", "Clientes");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
