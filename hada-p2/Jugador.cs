using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Jugador
    {
        private int _amonestaciones;
        private int _faltas;
        private int _energia;

        public static int maxAmonestaciones { get; set; }
        public static int maxFaltas { get; set; }
        public static int minEnergia { get; set; }
        public static Random rand { private get; set; }

        public string nombre { get; private set; }
        public int puntos { get; set; }

        private int amonestaciones
        {
            get { return _amonestaciones; }
            set {
                if (value < 0)
                {
                    _amonestaciones = 0;
                }
                else _amonestaciones = value;
                if (_amonestaciones > maxAmonestaciones)
                {
                    amonestacionesMaximoExcedido(this, new AmonestacionesMaximoExcedidoArgs(_amonestaciones));
                }
            }
        }
        private int faltas
        {
            get { return _faltas; }
            set
            {
                if (value > maxFaltas)
                {
                    faltasMaximoExcedido(this, new FaltasMaximoExcedidoArgs(_faltas));
                }
            }
        }
        private int energia
        {
            get { return _energia; }
            set
            {
                if (value < 0)
                {
                    _energia = 0;
                }
                else if (value > 100)
                {
                    _energia = 100;
                } else _energia = value;
                if (_energia < minEnergia)
                {
                    energiaMinimaExcedida(this, new EnergiaMinimaExcedidaArgs(_energia));
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
            _amonestaciones = _amonestaciones + rand.Next(0, 2);
        }

        public void incFaltas()
        {
            _faltas = _faltas + rand.Next(0, 3);
        }

        public void decEnergia()
        {
            _energia = _energia - rand.Next(1, 7);
        }

        public void incPuntos()
        {
            puntos = puntos + rand.Next(0, 3);
        }

        public bool todoOk()
        {
            bool todoOkey = false;
            if (amonestaciones <= maxAmonestaciones &&
                energia >= minEnergia && faltas <= maxFaltas)
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

            salida = "[" + nombre + "]" + "Puntos: " + puntos + "; Amonestaciones: " + _amonestaciones + "; Faltas: " + _faltas + "; Energía: " + _energia + "%; Ok: " + todoOk();

            return salida;
        }
        public event EventHandler<AmonestacionesMaximoExcedidoArgs> amonestacionesMaximoExcedido;
        public event EventHandler<FaltasMaximoExcedidoArgs> faltasMaximoExcedido;
        public event EventHandler<EnergiaMinimaExcedidaArgs> energiaMinimaExcedida;
    }

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