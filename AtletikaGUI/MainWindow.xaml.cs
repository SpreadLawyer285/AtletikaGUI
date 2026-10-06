using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography.X509Certificates;

namespace AtletikaGUI;

public partial class MainWindow : Window
{
    private string connectionString = "Filename=atletika.db";
    private SqliteConnection connection;
    private List<Versenyzo> adatok;

    public MainWindow()
    {
        InitializeComponent();

        connection = new(connectionString);
        connection.Open();

        Beolvas();
    }

    private void Beolvas()
    {
        string queryText = """
            SELECT 
                versenyszamok.Id,
                versenyszamok.VersenySzam,
                versenyszamok.VersenyzoNev,
                versenyszamok.Eredmeny,
                nemzetek.Nemzet,
                versenyszamok.Csucs,
                versenyszamok.Helyezes
            FROM versenyszamok
            INNER JOIN nemzetek
                ON versenyszamok.NemzetKod = nemzetek.NemzetId
            ORDER BY versenyszamok.Id
            """;

        SqliteCommand command = new(queryText, connection);

        SqliteDataReader reader = command.ExecuteReader();

        adatok = [];

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string versenyszam = reader.GetString(1);
            string nev = reader.GetString(2);
            string eredmeny = reader.GetString(3);
            string nemzetiseg = reader.GetString(4);
            string csucs = reader.GetString(5);
            int helyezes = reader.GetInt32(6);

            adatok.Add(
                new Versenyzo(
                    id,
                    versenyszam,
                    nev,
                    eredmeny,
                    nemzetiseg,
                    csucs,
                    helyezes
                )
            );
        }

        eredmenyTabla.ItemsSource = adatok;
        eredmenyTabla.SelectedIndex = 0;
    }

    private void helyezesButton_Click(object sender, RoutedEventArgs e)
    {
        Versenyzo kijelolt = eredmenyTabla.SelectedItem as Versenyzo;

        helyezesLabel.Content = $"Helyezés: {kijelolt.Helyezes}.";
    }

    private void csucsButton_Click(object sender, RoutedEventArgs e)
    {
        int csucsok = 0;
        foreach(var versenyzo in adatok)
        {
            if (versenyzo.Csucs == "PB")
            {
                csucsok++;
            }
        }
        csucsLabel.Content = $"Egyéni csúcsok: {csucsok}";
    }
}