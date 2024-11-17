using CapaModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapaWeb.Utilidades
{
    public class HorarioResultado
    {
        public static HorarioResultado _instancia = null;

        private HorarioResultado()
        {

        }

        public static HorarioResultado Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new HorarioResultado();
                }
                return _instancia;
            }
        }
        public bool FormarHorario(List<Seccion> listaSeccion)
        {
            foreach (Seccion seccion1 in listaSeccion)
            {
                foreach (Seccion seccion2 in listaSeccion)
                {
                    SeccionTotal seccionT1 = FormarSeccionTotal(seccion1);
                    SeccionTotal seccionT2 = FormarSeccionTotal(seccion2);
                    if(CompararSecciones(seccionT1, seccionT2))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CompararSecciones(SeccionTotal seccion1, SeccionTotal seccion2)
        {
            if(seccion1._Seccion.Codigo == seccion2._Seccion.Codigo)
            {
                return true;
            }
            else if (seccion1._Seccion.CodigoCurso == seccion2._Seccion.CodigoCurso)
            {
                return false;
            }
            else
            {
                foreach (DiaSeccion dia1 in seccion1.listaDia)
                {
                    foreach (DiaSeccion dia2 in seccion2.listaDia)
                    {
                        if(ComparacionDiaSeccion(dia1, dia2))
                        {
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public List<string> CortarHorarioInicio(string horarioinicio)
        {
            List<string> listaCortada = new List<string>();
            for(int i = 0; i < horarioinicio.Length; i+= 2)
            {
                string segment = horarioinicio.Substring(i, 2);
                listaCortada.Add(segment);
            }
            return listaCortada;
        }

        public bool ComparacionDiaSeccion(DiaSeccion dia1, DiaSeccion dia2)
        {
            if(dia1.Dia != dia2.Dia)
            {
                return true;
            }
            else
            {
                foreach (int hora1 in dia1.listaHora)
                {
                    foreach (int hora2 in dia2.listaHora)
                    {
                        if(hora1 == hora2)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public SeccionTotal FormarSeccionTotal(Seccion seccion)
        {
            List<DiaSeccion> listaDias = new List<DiaSeccion>();
            string[] Matrizhorario = seccion.Horario.Split();
            List<string> Listahorainicio1 = CortarHorarioInicio(seccion.HoraInicio);
            for (int i = 0; i < Matrizhorario.Length; i++)
            {
                int _dia = Convert.ToInt32(Matrizhorario[i]);
                if (_dia != 0)
                {
                    int horaInicio = Convert.ToInt32(Listahorainicio1[i]);
                    List<int> listaHoras = new List<int>();
                    for (int j = 0; j < _dia; j++)
                    {
                        listaHoras.Add(horaInicio + j);
                    }
                    listaDias.Add(new DiaSeccion() 
                    {
                        Dia = i + 1,
                        listaHora = listaHoras
                    });
                }
            }
            return new SeccionTotal() 
            {
                _Seccion = seccion,
                listaDia = listaDias
            };
        }
    }

    public class DiaSeccion
    {
        public int Dia { get; set; }
        public List<int> listaHora { get; set; }
    }

    public class SeccionTotal
    {
        public Seccion _Seccion { get; set; }
        public List<DiaSeccion> listaDia { get; set; }
    }
}