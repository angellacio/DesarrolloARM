using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ent = Personal.Entidades;

namespace consolaFunciones
{
    public class AdministraCatalogos
    {
        ent.configAplicacion confAplica { get; set; }

        public AdministraCatalogos() 
        {
            confAplica = new ent.configAplicacion();

            confAplica = new ent.configAplicacion()
            {
                NumSerie = Guid.NewGuid().ToString(),
                lstGeneros = new List<ent.entCatSensillo>()
                 {
                    new ent.entCatSensillo{ nId = 0, sAcronimo = "Otros", sDescripcion = "Personalizados", bEstado = true },
                    new ent.entCatSensillo{ nId = 1, sAcronimo = "Clásica", sDescripcion = "Música clásica", bEstado = true },
                    new ent.entCatSensillo{ nId = 2, sAcronimo = "Blues", sDescripcion = "Blues", bEstado = true },
                    new ent.entCatSensillo{ nId = 3, sAcronimo = "Jazz", sDescripcion = "Jazz", bEstado = true },
                    new ent.entCatSensillo{ nId = 4, sAcronimo = "R&B", sDescripcion = "Rhythm and Blues (R&B)", bEstado = false },
                    new ent.entCatSensillo{ nId = 5, sAcronimo = "Rock & Roll", sDescripcion = "Rock and Roll", bEstado = true },
                    new ent.entCatSensillo{ nId = 6, sAcronimo = "K-Pop", sDescripcion = "K-pop", bEstado = true },
                    new ent.entCatSensillo{ nId = 7, sAcronimo = "Gospel", sDescripcion = "Gospel", bEstado = false },
                    new ent.entCatSensillo{ nId = 8, sAcronimo = "Soul", sDescripcion = "Soul", bEstado = false },
                    new ent.entCatSensillo{ nId = 9, sAcronimo = "Rock", sDescripcion = "Rock", bEstado = true },
                    new ent.entCatSensillo{ nId = 10, sAcronimo = "Metal", sDescripcion = "Metal", bEstado = true },
                    new ent.entCatSensillo{ nId = 11, sAcronimo = "Punk", sDescripcion = "Hadcore punk", bEstado = false },
                    new ent.entCatSensillo{ nId = 12, sAcronimo = "Country", sDescripcion = "Country", bEstado = false },
                    new ent.entCatSensillo{ nId = 13, sAcronimo = "Funk", sDescripcion = "Funk", bEstado = false },
                    new ent.entCatSensillo{ nId = 14, sAcronimo = "Disco", sDescripcion = "Disco", bEstado = false },
                    new ent.entCatSensillo{ nId = 15, sAcronimo = "House", sDescripcion = "House", bEstado = false },
                    new ent.entCatSensillo{ nId = 16, sAcronimo = "Techno", sDescripcion = "Techno", bEstado = false },
                    new ent.entCatSensillo{ nId = 17, sAcronimo = "Pop", sDescripcion = "Pop", bEstado = true },
                    new ent.entCatSensillo{ nId = 18, sAcronimo = "Ska", sDescripcion = "Ska", bEstado = true },
                    new ent.entCatSensillo{ nId = 19, sAcronimo = "Reggae", sDescripcion = "Reggae", bEstado = true },
                    new ent.entCatSensillo{ nId = 20, sAcronimo = "Bass", sDescripcion = "Drum and Bass", bEstado = false },
                    new ent.entCatSensillo{ nId = 21, sAcronimo = "Garage", sDescripcion = "Garage", bEstado = false },
                    new ent.entCatSensillo{ nId = 22, sAcronimo = "Flamenco", sDescripcion = "Flamenco", bEstado = false },
                    new ent.entCatSensillo{ nId = 23, sAcronimo = "Salsa", sDescripcion = "Salsa", bEstado = true },
                    new ent.entCatSensillo{ nId = 24, sAcronimo = "Hip Hop", sDescripcion = "Hip Hop", bEstado = true },
                    new ent.entCatSensillo{ nId = 25, sAcronimo = "Reggaeton", sDescripcion = "Reggaeton", bEstado = true },
                    new ent.entCatSensillo{ nId = 26, sAcronimo = "Glam Rock", sDescripcion = "Glam Rock", bEstado = false },
                    new ent.entCatSensillo{ nId = 27, sAcronimo = "Grunge", sDescripcion = "Grunge", bEstado = false },
                    new ent.entCatSensillo{ nId = 28, sAcronimo = "Rock Psicodélico", sDescripcion = "Rock Psicodélico", bEstado = false },
                    new ent.entCatSensillo{ nId = 29, sAcronimo = "Britpop", sDescripcion = "Britpop", bEstado = false },
                    new ent.entCatSensillo{ nId = 30, sAcronimo = "Oi", sDescripcion = "Oi!", bEstado = false },
                    new ent.entCatSensillo{ nId = 31, sAcronimo = "Vocal o Capella", sDescripcion = "Música vocal o “a capella”", bEstado = false },
                    new ent.entCatSensillo{ nId = 32, sAcronimo = "Instrumental", sDescripcion = "Música instrumental", bEstado = false },
                    new ent.entCatSensillo{ nId = 33, sAcronimo = "Pragmática", sDescripcion = "Música pragmática", bEstado = false },
                    new ent.entCatSensillo{ nId = 34, sAcronimo = "Mobiliario", sDescripcion = "Música de mobiliario", bEstado = false },
                    new ent.entCatSensillo{ nId = 35, sAcronimo = "Sonora", sDescripcion = "Banda sonora", bEstado = false },
                    new ent.entCatSensillo{ nId = 36, sAcronimo = "Electrónica", sDescripcion = "Música electrónica", bEstado = true },
                    new ent.entCatSensillo{ nId = 37, sAcronimo = "Ópera", sDescripcion = "Ópera", bEstado = false },
                    new ent.entCatSensillo{ nId = 38, sAcronimo = "Celta", sDescripcion = "Música celta", bEstado = false },
                    new ent.entCatSensillo{ nId = 39, sAcronimo = "Experimental", sDescripcion = "Música experimental", bEstado = false },
                    new ent.entCatSensillo{ nId = 40, sAcronimo = "Industrial", sDescripcion = "Música industrial", bEstado = false },
                    new ent.entCatSensillo{ nId = 41, sAcronimo = "Mariachi", sDescripcion = "Mariachi", bEstado = true },
                    new ent.entCatSensillo{ nId = 42, sAcronimo = "Norteña", sDescripcion = "Norteña", bEstado = true },
                    new ent.entCatSensillo{ nId = 43, sAcronimo = "Corridos", sDescripcion = "Corridos", bEstado = true },
                    new ent.entCatSensillo{ nId = 44, sAcronimo = "Sinaloense", sDescripcion = "Banda Sinaloense", bEstado = true },
                    new ent.entCatSensillo{ nId = 45, sAcronimo = "Cumbia", sDescripcion = "Cumbia", bEstado = true },
                    new ent.entCatSensillo{ nId = 46, sAcronimo = "Salsa", sDescripcion = "Salsa", bEstado = true },
                    new ent.entCatSensillo{ nId = 47, sAcronimo = "Bachata", sDescripcion = "Bachata", bEstado = true }
                }
            };
        }

        public void CreaJSonAdministracion(string sRutaArchivo)
        {
            string jsonString = JsonConvert.SerializeObject(confAplica);

            if (File.Exists(sRutaArchivo)) File.Delete(sRutaArchivo);

            File.WriteAllText(sRutaArchivo, jsonString);
        }
    }
}
