using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Jugador
    {
        private int Amonestaciones;
        private int Faltas;
        private int Energia;

        public static int maxAmonestaciones { get; set; }
        public static int maxFaltas { get; set; }
        public static int minEnergia { get; set;}
        public static Random rand { private get; set;}

        public string nombre { get; private set; }
        public int puntos { get; set; }

        private int amonestaciones
        {
            get { return Amonestaciones; }
            set { 
                if (value < 0)
                {
                    Amonestaciones = 0;
                }
                else Amonestaciones = value;
                if (Amonestaciones > maxAmonestaciones)
                {
                    amonestacionesMaximoExcedido(this, new AmonestacionesMaximoExcedidoArgs(Amonestaciones));
                }
                    }
        }
        private int faltas
        {
            get { return Faltas; }
            set
            {
                if (value > maxFaltas)
                {
                    faltasMaximoExcedido(this, new FaltasMaximoExcedidoArgs(Faltas));
                }
            }
        }
        private int energia
        {
            get { return Energia; }
            set
            {
                if (value < 0)
                {
                    Energia = 0;
                }
                else if (value > 100)
                {
                    Energia = 100;
                } else Energia = value;
                if (Energia < minEnergia)
                {
                    energiaMinimaExcedida(this, new energiaMinimaExcedidaArgs(Energia));
                }
            }
        }

        public Jugador(string nombre, int amonestaciones, int faltas, int energia, int puntos)
        {
            this.nombre = nombre;
            this.amonestaciones = amonestaciones;
            this.faltas = faltas;
            this.energia = energia;
            this.puntos = puntos;
        }

        public void incAmonestaciones()
        {
            Amonestaciones = Amonestaciones + rand.Next(0, 2); 
        }

        public void incFaltas()
        {
            Faltas = Faltas + rand.Next(0, 3);
        }

        public void decEnergia()
        {
            Energia = Energia - rand.Next(1, 7);
        }

        public void incPuntos()
        {
            puntos = puntos + rand.Next(0, 3);
        }

        public bool todoOk()
        {
            bool todoOkey=false;
            if (amonestaciones <= maxAmonestaciones &&
                energia>=minEnergia && faltas<=maxFaltas)
            {
                todoOkey = true;
            }

            return todoOkey;
        }

        public void mover()
        {
            if (todoOk())
            {
                incAmonestaciones();
                incFaltas();
                incPuntos();
                decEnergia();
            }
        }

        public override string ToString()
        {
            string salida;

            salida = "[" + nombre + "]" + "Puntos: " + puntos + "; Amonestaciones: " + Amonestaciones + "; Faltas: " + Faltas + "; Energía: " + Energia + "%; Ok: " + todoOk();

            return salida;
        }
        public event EventHandler<AmonestacionesMaximoExcedidoArgs> amonestacionesMaximoExcedido;
        public event EventHandler<FaltasMaximoExcedidoArgs> faltasMaximoExcedido;
        public event EventHandler<EnergiaMinimaExcedidaArgs> energiaMinimaExcedida;

        public class AmonestacionesMaximoExcedidoArgs: EventArgs
        {
            public int amonestaciones { get; set; }
            public AmonestacionesMaximoExcedidoArgs(int numero)
            {
                amonestaciones = numero;
            }
        }

        public class FaltasMaximoExcedidoArgs: EventArgs
        {
            public int faltas { get; set; }

            public FaltasMaximoExcedidoArgs(int faltnum)
            {
                faltas = faltnum;
            }
        }
        public class EnergiaMinimaExcedidaArgs: EventArgs
        {
            public int energia { get; set; }

            public EnergiaMinimaExcedidaArgs(int enernum)
            {
                energia = enernum;
            }
        }
    }
}