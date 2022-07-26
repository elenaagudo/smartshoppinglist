using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Helpers
{
    public class LanguageHelper
    {
        public static void ChangeLanguage(string idiomaSeleccionado)
        {
            if (idiomaSeleccionado != null)
            {
                try
                {
                    // Cambiar el idioma en base de datos. Opcional

                    var cultureInfo = CultureInfo.CreateSpecificCulture(idiomaSeleccionado);
                    cultureInfo.NumberFormat.NegativeSign = "-";
                    cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
                    CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                    CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
                }
                catch (Exception)
                {
                }

            }
        }
    }
}
