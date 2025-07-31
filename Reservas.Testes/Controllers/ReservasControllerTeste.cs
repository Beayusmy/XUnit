using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Reservas.Api.Controllers;
using Reservas.Api.Interfaces;
using Reservas.Api.Models;
using Reservas.Api.Repositories;
using Reservas.Testes.MockData;

namespace Reservas.Testes.Controllers
{
    public class ReservasControllerTeste
    {
        [Fact]
        public void GetTodasReservas_DeveRetornar200Status()
        {
            //Arrange - Organizar
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i.Reservas).Returns(ReservasMockDatas.GetReservas());
            var sut = new ReservasController(reservaService.Object);
            //Act - Agir
            var result = (OkObjectResult)sut.Get();
            //Assert - Afirmar
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetReservaPorId_DeveRetornar200_QuandoEncontrar()
        {
            //Arrange - Organizar
            var reservaMock = ReservasMockDatas.GetReservas()[0];
            var reservaService = new Mock<IReservaRepository>(); reservaService.Setup(i => i[reservaMock.ReservaId]).Returns(reservaMock);
            var sut = new ReservasController(reservaService.Object);
            //Act - Agir
            var result = sut.Get(reservaMock.ReservaId);
            //Assert - Afirmar
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetReservaPorId_DeveRetornar404_QuandoNaoEncontrar()
        {
            //Arrange - organizar os dados
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i[999]).Returns((Reserva)null);
            var sut = new ReservasController(reservaService.Object);
            //Act - Agir
            var result = sut.Get(999);
            //Assert - Afirmar
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]


        public void PostReserva_DeveRetornar2015Status()
        {
            //Arrange - organizar os dados
            var novaResposta = new Reserva { Nome = "teste" };
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i.AddReserva(It.IsAny<Reserva>())).Returns(novaResposta);
            var sut = new ReservasController(reservaService.Object);
            //Act - Agir
            var result = sut.Post(novaResposta);
            //Assert - Afirmar
            var createdResult = result.Result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            createdResult.StatusCode.Should().Be(201);
        }

        [Fact]

        public void PutReserva_DeveRetornar200_QuandoAtualizarComSucesso()
        {
            //Arrange - organizar os dados
            var reservaAtualizada = new Reserva { ReservaId = 1, Nome = "Atualizado" };
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i.UpdateReserva(It.IsAny<Reserva>())).Returns(reservaAtualizada);
            var sut = new ReservasController(reservaService.Object);

            //Act - executar a ação
            var result = sut.Put(reservaAtualizada);

            //Assert - verificar o resultado 
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull(); // Deve nao serr nulo!!!
            okResult.StatusCode.Should().Be(200);
        }

        [Fact]

        public void PatchReserva_DeveRetornar200_QuandoAplicarPatchComSucesso()
        {
            //Atualizacoes

            //Arrange - organizar os dados
            var reservaOriginal = new Reserva { ReservaId = 1, Nome = "Teste" };
            var patch = new JsonPatchDocument<Reserva>();
            patch.Replace(r => r.Nome, "Atualizado");

            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i[1]).Returns(reservaOriginal);
            var sut = new ReservasController(reservaService.Object);

            //Act - executar a ação
            var result = sut.Patch(1, patch);

            //Assert - verificar o resultado 
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void PatchReserva_DeveRetornar404_QuandoReservaNaoEncontrada()
        {
            //Arrange - organizar os dados
            var patch = new JsonPatchDocument<Reserva>();
            var reservaService = new Mock<IReservaRepository>();
            reservaService.Setup(i => i[999]).Returns((Reserva)null);
            var sut = new ReservasController(reservaService.Object);
            //Act - executar a ação
            var result = sut.Patch(999, patch);
            //Assert - verificar o resultado 
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]

        public void DeleteReserva_DeveRetornar204()
        {
            //Arrange - organizar os dados
            var reservaService = new Mock<IReservaRepository>();
            var sut = new ReservasController(reservaService.Object);
            //Act - executar a ação
            var result = sut.Delete(1);
            //Assert - verificar o resultado 
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
