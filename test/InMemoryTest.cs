using EFCore.Testes.Data;
using EFCore.Testes.Entities;
using Microsoft.EntityFrameworkCore;
using System;
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

        private ApplicationContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase("InMemoryTest").Options;

            return new ApplicationContext(options);
        }
    }
}
