using AppTarefas.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTarefas.Banco
{
    internal class BancoContext : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }

        public BancoContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename ={Constantes.CaminhoDoBanco}");
        }
    }
}
