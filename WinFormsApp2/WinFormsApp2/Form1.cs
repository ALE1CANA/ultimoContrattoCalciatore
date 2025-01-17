using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public class Contratto
    {
        public string Squadra { get; set; }
        public int Stipendio { get; set; }
        public int DurataContratto { get; set; }
        public int Bonus { get; set; }
        public string Sponsor { get; set; }
        public string Clausola { get; set; }
        public string ObiettivoSquadra { get; set; }
        public int BonusObiettivo { get; set; }

        public override string ToString()
        {
            return $"Squadra: {Squadra}\n" +
                   $"Stipendio: {Stipendio}\n" +
                   $"Durata: {DurataContratto} ANNI\n" +
                   $"Bonus: {Bonus}\n" +
                   $"Sponsor: {Sponsor}\n" +
                   $"Clausola: {Clausola}\n" +
                   $"Obiettivo Squadra: {ObiettivoSquadra}\n" +
                   $"Bonus Obiettivo: {BonusObiettivo}";
        }
    }

    public partial class Form1 : Form
    {
        private List<Contratto> contratti;
        private int contrattoAttuale;
        private List<Contratto> inAttesa;
        private ListBox lstContratto;
        private Button btnAccetta;
        private Button btnRifiuta;
        private Button btnAspetta;
        private Random random;
        private List<string> squadre_SerieA;
        private List<string> squadre_PremierLeague;
        private List<string> squadre_LaLiga;
        private List<string> squadre_Ligue1;
        private List<string> squadre_Bundesliga;

        // Liste delle competizioni per ogni campionato
        private List<string> competizioniEuropee;
        private List<string> competizioniItaliane;
        private List<string> competizioniTedesche;
        private List<string> competizioniFrancesi;
        private List<string> competizioniSpagnole;
        private List<string> competizioniInglesi;

        public Form1()
        {
            contratti = new List<Contratto>();
            contrattoAttuale = 0;
            inAttesa = new List<Contratto>();
            random = new Random();
            squadre_SerieA = new List<string> { "ROMA", "INTER", "JUVENTUS" };
            squadre_PremierLeague = new List<string> { "MANCHESTER UNITED", "LIVERPOOL", "CHELSEA" };
            squadre_LaLiga = new List<string> { "BARCELONA", "REAL MADRID", "ATLETICO MADRID" };
            squadre_Ligue1 = new List<string> { "PSG", "LYON", "MARSEILLE" };
            squadre_Bundesliga = new List<string> { "BAYERN MUNICH", "DORTMUND", "LEIPZIG" };

            // Inizializzazione delle competizioni
            competizioniEuropee = new List<string> { "CHAMPIONS LEAGUE", "EUROPA LEAGUE", "CONFERENCE LEAGUE" };
            competizioniItaliane = new List<string> { "SERIE A", "COPPA ITALIA", "SUPERCOPPA ITALIANA" };
            competizioniTedesche = new List<string> { "BUNDESLIGA", "DFB-POKAL", "SUPERCOPPA DI GERMANIA" };
            competizioniFrancesi = new List<string> { "LIGUE 1", "COPPA DI FRANCIA", "TROFEO DEI CAMPIONI" };
            competizioniSpagnole = new List<string> { "LA LIGA", "COPA DEL REY", "SUPERCOPA DE ESPAÑA" };
            competizioniInglesi = new List<string> { "PREMIER LEAGUE", "FA CUP", "CARABAO CUP", "SUPERCOPA INGLESE" };

            lstContratto = new ListBox { Location = new Point(10, 10), Size = new Size(360, 200) };
            btnAccetta = new Button { Text = "Accetta", Location = new Point(10, 220), Size = new Size(100, 30) };
            btnRifiuta = new Button { Text = "Rifiuta", Location = new Point(120, 220), Size = new Size(100, 30) };
            btnAspetta = new Button { Text = "Aspetta", Location = new Point(230, 220), Size = new Size(100, 30) };

            btnAccetta.Click += BtnAccetta_Click;
            btnRifiuta.Click += BtnRifiuta_Click;
            btnAspetta.Click += BtnAspetta_Click;

            Controls.Add(lstContratto);
            Controls.Add(btnAccetta);
            Controls.Add(btnRifiuta);
            Controls.Add(btnAspetta);

            GeneraContratti();
        }

        private void VisualizzaContratto()
        {
            lstContratto.Items.Clear();
            if (contrattoAttuale < contratti.Count)
            {
                string[] dettagliContratto = contratti[contrattoAttuale].ToString().Split('\n');
                foreach (string dettaglio in dettagliContratto)
                {
                    lstContratto.Items.Add(dettaglio);
                }
            }
        }

        private void BtnAccetta_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hai accettato il seguente contratto:\n" + contratti[contrattoAttuale]);
            Application.Exit();
        }

        private void BtnRifiuta_Click(object sender, EventArgs e)
        {
            contratti.RemoveAt(contrattoAttuale);
            if (contratti.Count == 0 && inAttesa.Count == 0)
            {
                MessageBox.Show("Non ci sono più contratti disponibili.");
                Application.Exit();
            }
            else
            {
                if (contrattoAttuale >= contratti.Count && inAttesa.Count > 0)
                {
                    contratti.AddRange(inAttesa);
                    inAttesa.Clear();
                    contrattoAttuale = 0;
                }
                VisualizzaContratto();
            }
        }

        private void BtnAspetta_Click(object sender, EventArgs e)
        {
            inAttesa.Add(contratti[contrattoAttuale]);
            contratti.RemoveAt(contrattoAttuale);
            if (contratti.Count == 0 && inAttesa.Count > 0)
            {
                contratti.AddRange(inAttesa);
                inAttesa.Clear();
                contrattoAttuale = 0;
            }
            VisualizzaContratto();
        }

        private string GeneraObiettivo(string squadra)
        {
            List<string> competizioniSquadra = new List<string>();

            if (squadra == "ROMA" || squadra == "JUVENTUS" || squadra == "INTER")
            {
                competizioniSquadra.AddRange(competizioniItaliane);
            }
            else if (squadra == "MANCHESTER UNITED" || squadra == "LIVERPOOL" || squadra == "CHELSEA")
            {
                competizioniSquadra.AddRange(competizioniInglesi);
            }
            else if (squadra == "BARCELONA" || squadra == "REAL MADRID" || squadra == "ATLETICO MADRID")
            {
                competizioniSquadra.AddRange(competizioniSpagnole);
            }
            else if (squadra == "PSG" || squadra == "LYON" || squadra == "MARSEILLE")
            {
                competizioniSquadra.AddRange(competizioniFrancesi);
            }
            else if (squadra == "BAYERN MUNICH" || squadra == "DORTMUND" || squadra == "LEIPZIG")
            {
                competizioniSquadra.AddRange(competizioniTedesche);
            }

            // Aggiungiamo sempre le competizioni europee
            competizioniSquadra.AddRange(competizioniEuropee);

            // Selezioniamo un obiettivo casuale tra le competizioni disponibili
            return competizioniSquadra[random.Next(competizioniSquadra.Count)];
        }

        private void GeneraContratti()
        {
            List<string> sponsor = new List<string> { "NIKE", "ADIDAS", "PUMA" };
            List<string> clausole = new List<string> { "NESSUNA CLAUSOLA", "CLAUSOLA DI 50 MILIONI", "CLAUSOLA DI 150 MILIONI", "CLAUSOLA DI 200 MILIONI", "CLAUSOLA DI 250 MILIONI" };
            List<string> tutteLeSquadre = new List<string>();
            tutteLeSquadre.AddRange(squadre_SerieA);
            tutteLeSquadre.AddRange(squadre_PremierLeague);
            tutteLeSquadre.AddRange(squadre_LaLiga);
            tutteLeSquadre.AddRange(squadre_Ligue1);
            tutteLeSquadre.AddRange(squadre_Bundesliga);

            List<string> squadreUtilizzate = new List<string>();

            for (int i = 0; i < 3; i++)
            {
                string squadra;
                do
                {
                    squadra = tutteLeSquadre[random.Next(tutteLeSquadre.Count)];
                } while (squadreUtilizzate.Contains(squadra));
                squadreUtilizzate.Add(squadra);

                string sponsorContratto = sponsor[random.Next(sponsor.Count)];
                string clausola = clausole[random.Next(clausole.Count)];
                string obiettivo = GeneraObiettivo(squadra);  // Usa la funzione per generare l'obiettivo basato sulla squadra

                Contratto contratto = new Contratto
                {
                    Squadra = squadra,
                    Stipendio = random.Next(50000, 1000000),
                    DurataContratto = random.Next(1, 5),
                    Bonus = random.Next(25000, 500000),
                    Sponsor = sponsorContratto,
                    Clausola = clausola,
                    ObiettivoSquadra = obiettivo,
                    BonusObiettivo = random.Next(35000, 750000)
                };

                contratti.Add(contratto);
            }

            VisualizzaContratto();
        }
    }
}
