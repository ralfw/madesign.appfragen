using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using jsonserialization;

namespace af.application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);

            var befragung = new af.contracts.Befragung();

            var ui = new af.ui.Ui();
            var befragen = new af.modul.befragen.Befragen(befragung);
            var auswerten = new af.modul.auswerten.Auswerten(befragung);

            ui.Json_output += befragen.Process;
            ui.Json_output += auswerten.Process;
            befragen.Json_output += ui.Process;
            auswerten.Json_output += ui.Process;

            dynamic cmdStarten = new ExpandoObject();
            cmdStarten.cmd = "Starten";

            ui.Process(JsonExtensions.ToJson(cmdStarten));
        }
    }
}
