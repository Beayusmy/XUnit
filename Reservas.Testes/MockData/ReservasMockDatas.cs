using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservas.Api.Models;

namespace Reservas.Testes.MockData
{
    public class ReservasMockDatas
    {
        public static List<Reserva> GetReservas()
        {
            return new List<Reserva>(){
                new Reserva {ReservaId = 1, Nome="Bia", InicioLocacao = "Sao Paulo", FimLocacao="Rio de Janeiro"},
                new Reserva {ReservaId = 2, Nome="Bia", InicioLocacao = "Sao Paulo", FimLocacao="Rio de Janeiro" },
                new Reserva {ReservaId = 3, Nome="Bia", InicioLocacao = "Sao Paulo", FimLocacao="Rio de Janeiro" }

        };
        }
    }
}