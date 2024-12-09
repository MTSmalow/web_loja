using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using WebLoja.Models;

namespace WebLoja.Data
{
    public class WebLojaContext : DbContext
    {
        public WebLojaContext (DbContextOptions<WebLojaContext> options)
            : base(options)
        {
        }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>().HasData(
                new Produto { Id = 1, Categoria = "", Nome = "SNES", Descricao = "", Preco = 15000, Foto = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQY6giYFcAsV5wX9z0E5sudLWKwATbkO3IoUQ&s" },
                new Produto { Id = 2, Categoria = "", Nome = "PS5 Controller", Descricao = "", Preco = 1500, Foto = "https://lazamodz.com/cdn/shop/files/PS1Classic_800x.png?v=1698695300" },
                new Produto { Id = 3,Categoria = "", Nome = "Turbo Express", Descricao = "", Preco = 15000, Foto = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/NEC-TurboExpress-Upright-FL.jpg/220px-NEC-TurboExpress-Upright-FL.jpg" }
                ) ;
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { Id = 1,Email = "cliente@cliente.com",Autenticacao= "SKmH48EJHaCuj+528OWOzr9r17D/qwOxxzRplydB+Nc=", Endereco= "Rua Ribeirão Preto, 75 Mauá-SP", Nome="Cliente" },
                new Cliente { Id = 2, Email = "admin@admin", Autenticacao = "r2wwX8N1W1w7pocd+OMMPVwkEh3JkZ+1gW/momxDnq0=", Endereco = "Rua Ribeirão Preto, 75 Mauá-SP", Nome = "Administrador" } 
                );

        }

        public DbSet<WebLoja.Models.Produto> Produto { get; set; } = default!;



        public DbSet<WebLoja.Models.Cliente> Cliente { get; set; }

    }
}
