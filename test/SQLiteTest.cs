using EFCore.Testes.Data;
using EFCore.Testes.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EFCore.Testes
{
    public class SQLiteTest
    {
        [Theory]
        [InlineData("Tecnologia")]
        [InlineData("Financeiro")]
        [InlineData("Departamento Pessoal")]
        public void DeveInserirEConsultarUmDepartamento(string descricao)
        {
            // Ararnge
            var _departamento = new Departamento
            {
                Descricao = descricao,
                DataCadastro = DateTime.Now
            };

            // Setup
            var _context = CreateContext();
            _context.Database.EnsureCreated();
            _context.Departamentos.Add(_departamento);

            // Action
            var _inseridos = _context.SaveChanges();
            _departamento = _context.Departamentos.FirstOrDefault(departamento => departamento.Descricao.Equals(descricao));

            // Assert
            Assert.Equal(1, _inseridos);
            Assert.Equal(descricao, _departamento.Descricao);
        }

        private ApplicationContext CreateContext()
        {
            var conexao = new SqliteConnection("Datasource=:memory:");
            conexao.Open();

            var options = new DbContextOptionsBuilder<ApplicationContext>().UseSqlite(conexao).Options;

            return new ApplicationContext(options);
        }
    }
}
