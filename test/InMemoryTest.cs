using EFCore.Testes.Data;
using EFCore.Testes.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace EFCore.Testes
{
    public class InMemoryTest
    {
        [Fact]
        public void DeveInserirUmDepartamento()
        {
            // Ararnge
            var departamento = new Departamento
            {
                Descricao = "Tecnologia",
                DataCadastro = DateTime.Now
            };

            // Setup
            var context = CreateContext();
            context.Departamentos.Add(departamento);

            // Action
            var inseridos = context.SaveChanges();

            // Assert
            Assert.Equal(1, inseridos);
        }

        [Fact]
        public void NaoImplementadoFuncoesDeDatasParaProviderInMemory()
        {
            // Ararnge
            var _departamento = new Departamento
            {
                Descricao = "Tecnologia",
                DataCadastro = DateTime.Now
            };

            // Setup
            var _context = CreateContext();
            _context.Departamentos.Add(_departamento);

            // Action
            var _inseridos = _context.SaveChanges();

            // Assert
            Action _action = () => _context.Departamentos.FirstOrDefault(departamento => EF.Functions.DateDiffDay(DateTime.Now, departamento.DataCadastro) > 0);
            Assert.Throws<NotImplementedException>(_action);
        }

        private ApplicationContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase("InMemoryTest").Options;

            return new ApplicationContext(options);
        }
    }
}
