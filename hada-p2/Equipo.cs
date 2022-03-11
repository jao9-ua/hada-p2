using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Equipo
    {
        private List<Jugador> equip;
        private List<Jugador> ExcdLimiteAm;
        private List<Jugador> ExcdLimiteFa;
        private List<Jugador> ExcdMinimEn;
        public static int minJugadores { get; set; }

        public static int maxNumeroMovimientos { get; set; }

        public int movimientos { get; private set; }

        public string nombreEquipo { get; private set; }

        public Equipo(int nj, string nom)
        {
            nombreEquipo = nom;
            equip = new List<Jugador>();
            ExcdLimiteAm = new List<Jugador>();
            ExcdLimiteFa = new List<Jugador>();
            ExcdMinimEn = new List<Jugador>();

            for (int i = 0; i < nj; i++)
            {
                equip.Add(new Jugador("Jugador_" + i, 0, 0, 50, 0));
            }

        }

        public bool moverJugadores()
        {
            bool resultado=false;
            int incr = 0;
            for (int i=0;i<equip.Count();i++)
            {
                if (equip[i].todoOk())
                {
                    incr++;
                    equip[i].mover();
                   
                }
            }
            if (incr > minJugadores)
            {
                resultado = true;
            }
            movimientos++;

            return resultado;
        }

        public void moverJugadoresEnBucle()
        {
            for (int i = 0; i < equip.Count(); i++)
            {
                if (equip[i].todoOk())
                {
                    equip[i].mover();

                }
            }
        }

        public int sumarPuntos()
        {
            int puntos = 0;
            for(int i = 0; i < equip.Count; i++)
            {
                puntos = equip[i].puntos + puntos;
            }

            return puntos;
        }
        
        public List<Jugador> getJugadoresExcedenLimiteAmonestaciones()
        {
            return ExcdLimiteAm;
        }

        public List<Jugador> getJugadoresExcedenLimiteFaltas()
        {
            return ExcdLimiteFa;
        }

        public List<Jugador> getJugadoresExcedenMinimoEnergia()
        {
            return ExcdMinimEn;
        }

        public override string ToString()
        {
            string salida;

            salida = "[EQUIPO_" + nombreEquipo + "]" + " Puntos: " + sumarPuntos() + ";";
            salida += " Expulsados: " + getJugadoresExcedenLimiteFaltas().Count();
            salida +=   "; Retirados: " + getJugadoresExcedenMinimoEnergia().Count()+ "\n" ;

            for (int i = 0; i < equip.Count(); i++)
            {
                salida += equip[i].ToString() + "\n";
            }



            return salida;
        }

        private void cuandoAmonestacionesMaximoExcedido(object sender, AmonestacionesMaximoExcedidoArgs evento)
        {
            Jugador jugador = (Jugador)sender;

            if (ExcdLimiteAm.Contains(jugador) == false)
            {
                ExcdLimiteAm.Add(jugador);
            }
            Console.WriteLine("¡¡Número máximo excedido de amonestaciones. Jugador expulsado!!");
            Console.WriteLine("Jugador: " + jugador.nombre);
            Console.WriteLine("Equipo: " + "equipo_" + nombreEquipo );
            Console.WriteLine("Amonestaciones: " + evento.amonestaciones);
        }

        private void cuandoFaltasMaximoExcedido(object sender, FaltasMaximoExcedidoArgs evento)
        {
            Jugador jugador = (Jugador)sender;

            if (ExcdLimiteFa.Contains(jugador) == false)
            {
                ExcdLimiteFa.Add(jugador);
            }
            Console.WriteLine("¡¡Número máximo excedido de faltas recibidas. Jugador lesionado!!");
            Console.WriteLine("Jugador: " + jugador.nombre);
            Console.WriteLine("Equipo: " + "equipo_" + nombreEquipo);
            Console.WriteLine("Faltas: " + evento.faltas);
        }

        private void cuandoEnergiaMinimaExcedida(object sender, EnergiaMinimaExcedidaArgs evento)
        {
            Jugador jugador = (Jugador)sender;

            if (ExcdMinimEn.Contains(jugador) == false)
            {
                ExcdMinimEn.Add(jugador);
            }
            Console.WriteLine("¡¡Energía mínima excedida. Jugador retirado!!");
            Console.WriteLine("Jugador: " + jugador.nombre);
            Console.WriteLine("Equipo: " + "equipo_" + nombreEquipo);
            Console.WriteLine("Energía: " + evento.energia + " %");
        }
    }
}
