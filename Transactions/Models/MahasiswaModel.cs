using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Transactions.Models
{
    public class MahasiswaModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string nrp { get; set; }
        public string email { get; set; }
        public string jurusan { get; set; }
        public string nama_dosen { get; set; }
    }

    public class DosenModel
    {
        public string id { get; set; }
        public string nama_dosen { get; set; }
        public string nidn { get; set; }
    }
}