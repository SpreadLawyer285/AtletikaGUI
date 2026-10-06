using System;
using System.Collections.Generic;
using System.Text;

namespace AtletikaGUI
{
    internal class Versenyzo
    {
        public int Id { get; set; }
        public string Versenyszam { get; set; }
        public string Nev { get; set; }
        public string Eredmeny { get; set; }
        public string Nemzetiseg { get; set; }
        public string Csucs { get; set; }
        public int Helyezes { get; set; }

        public Versenyzo(
            int id,
            string versenyszam,
            string nev,
            string eredmeny,
            string nemzetiseg,
            string csucs,
            int helyezes)
        {
            Id = id;
            Versenyszam = versenyszam;
            Nev = nev;
            Eredmeny = eredmeny;
            Nemzetiseg = nemzetiseg;
            Csucs = csucs;
            Helyezes = helyezes;
        }
    }
}
