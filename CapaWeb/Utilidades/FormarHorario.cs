using CapaModelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapaWeb.Utilidades
{
    public class FormarHorario
    {
        public bool formarHorario(List<Seccion> listaSeccion)
        {
            foreach (Seccion seccion1 in listaSeccion)
            {
                foreach (Seccion seccion2 in listaSeccion)
                {
                    if(CompararSecciones(seccion1, seccion2))
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

        public bool CompararSecciones(Seccion seccion1, Seccion seccion2)
        {
            if(seccion1.Codigo == seccion2.Codigo)
            {
                return true;
            }
            else if (seccion1.CodigoCurso == seccion2.CodigoCurso)
            {
                return false;
            }
            else
            {
                string[] horario1 = seccion1.Horario.Split();
                string[] horario2 = seccion2.Horario.Split();
                List<string> Listahorainicio1 = CortarHorarioInicio(seccion1.HoraInicio);
                List<string> Listahorainicio2 = CortarHorarioInicio(seccion2.HoraInicio);
                for (int i = 0; i < horario1.Length; i++)
                {
                    if (horario1[i] == "0" || horario2[i] == "0")
                    {
                        continue;
                    }
                    else
                    {
                        int horainicio1 = Convert.ToInt32(Listahorainicio1[i]);
                        int horainicio2 = Convert.ToInt32(Listahorainicio2[i]);
                        int horafin1 = horainicio1 + Convert.ToInt32(horario1[i]);
                        int horafin2 = horainicio2 + Convert.ToInt32(horario2[i]);
                        if (horainicio1 >= horafin2 || horainicio2 >= horafin1)
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

        public static List<string> CortarHorarioInicio(string horarioinicio)
        {
            List<string> listaCortada = new List<string>();
            for(int i = 0; i < horarioinicio.Length; i+= 2)
            {
                string segment = horarioinicio.Substring(i, 2);
                listaCortada.Add(segment);
            }
            return listaCortada;
        }

        public static SeccionTotal formarSeccionTotal(Seccion seccion)
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