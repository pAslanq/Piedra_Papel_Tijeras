using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Response;

namespace Piedra_Papel_Tijeras
{
    public class BD
    {

        private IFirebaseClient db;

        public BD()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "RozpBgS5S6wnxwulWpaWRXbrx7Qn5b0DjZpOGYC4",
                BasePath = "https://piedrapapeltijeraia-default-rtdb.firebaseio.com/"
            };

            db = new FireSharp.FirebaseClient(config);
        }

        //guardar contadores
        public async Task ContadoresAsync(Contadores datos)
        {
            if (db != null)
            {
                SetResponse response = await db.SetAsync("aprendizaje/global", datos);
            }
        }

        //obtener contadores acumulados
        public async Task<Contadores> ObtenerContadoresAsync()
        {
            if (db != null)
            {
                FirebaseResponse response = await db.GetAsync("aprendizaje/global");
                if (response != null && response.Body != "null")
                {
                    return response.ResultAs<Contadores>();
                }
            }
            return new Contadores();
        }
    }
}
