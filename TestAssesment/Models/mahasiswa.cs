//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestAssesment.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class mahasiswa
    {
        public int id { get; set; }
        public string name { get; set; }
        public string nrp { get; set; }
        public string email { get; set; }
        public string jurusan { get; set; }
        public Nullable<int> dosen_wali_id { get; set; }
        public string nama_dosen { get; set; }

        public virtual dosen_wali dosen_wali { get; set; }
    }
}